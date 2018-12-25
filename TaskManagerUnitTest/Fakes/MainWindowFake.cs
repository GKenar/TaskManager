using System;
using System.Collections.Generic;
using TaskManagerCommon.Components;
using TaskManagerView;
using TaskManagerView.Components;

namespace TaskManagerUnitTest.Fakes
{
    class MainWindowFake : IMainWindow
    {
        public void Initialize()
        {
            
        }

        public void Show()
        {
            
        }

        public bool TaskSelected { get; }
        public DateInterval DateIntervalSelected { get; }
        public ITaskListSettings TaskListSettings { get; }

        public void ShowMessageBox(string message)
        {
            
        }

        public void SetUserTasksToTasksList(List<UserTaskView> tasks)
        {
            
        }

        public void SetHighlightDates(List<DateTime> dates)
        {
            
        }

        public void EnableEditRemoveControls(bool enable)
        {
            
        }

        public event EventHandler<UserTaskEventArgs> UserTaskUpdated;
        public event EventHandler<UserTaskEventArgs> UserTaskAdded;
        public event EventHandler<UserTaskEventArgs> UserTaskDeleted;
        public event EventHandler<TaskDateIntervalEventArg> CurrentCalendarDateChanged;
        public event EventHandler<FilterEventArgs> FilterTypeChanged;
        public event EventHandler<SortEventArgs> SortTypeChanged;
        public event EventHandler TasksControlButtonPressed;
        public event EventHandler SelectionListUpdated;
        public event EventHandler TasksListNeedUpdate;
        public event EventHandler HighlightListNeedUpdate;
    }
}
