using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskManagerModel;

namespace TaskManagerNotifier
{
    public class Notifier : INotifier, IDisposable
    {
        public event Action ShowMainWindow = delegate { };

        private readonly IDataModel _dataModel;

        private readonly NotifyIcon _notifyIcon;

        private DateTime _appCurrentTime;

        private const int TickInterval = 5000;

        private const int BalloontipShowTime = 3000;

        private Task _task;

        private CancellationTokenSource _taskCancellationTokenSource;

        private CancellationToken _taskCancellationToken;

        public Notifier(IDataModel dataModel)
        {
            _dataModel = dataModel;

            _notifyIcon = new NotifyIcon
            {
                Text = "TaskManager",
                Icon = new Icon("appicon.ico"),
                Visible = true
            };
            _notifyIcon.DoubleClick += NotifyIconOnDoubleClick;

            _notifyIcon.BalloonTipTitle = "TaskManager";
            _notifyIcon.BalloonTipText = "Есть задачи, назначенные на сегодня!";
        }

        public void Run()
        {
            if (_task != null)
                return;

            _taskCancellationTokenSource = new CancellationTokenSource();
            _taskCancellationToken = _taskCancellationTokenSource.Token;
            _task = Task.Run((Action) Tick, _taskCancellationToken);
        }

        private void CheckForNotifyDates()
        {
            var notifyDates = _dataModel.GetTaskNotifyDates(_appCurrentTime); //Здесь можно отловить исключение

            if(!notifyDates.Any())
                return;

            _notifyIcon.ShowBalloonTip(BalloontipShowTime);
        }

        private void Tick()
        {
            while (!_taskCancellationToken.IsCancellationRequested)
            {
                if (_appCurrentTime != DateTime.Today)
                {
                    _appCurrentTime = DateTime.Today;
                    CheckForNotifyDates();
                }

                Thread.Sleep(TickInterval);
            }
        }

        private void NotifyIconOnDoubleClick(object sender, EventArgs eventArgs)
        {
            ShowMainWindow();
        }

        public void Dispose()
        {
            if(_task != null && !_task.IsCompleted)
                _taskCancellationTokenSource.Cancel();

            _notifyIcon.Dispose();
        }
    }
}
