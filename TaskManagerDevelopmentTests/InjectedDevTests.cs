using System;
using TaskManagerCommon.Components;
using TaskManagerModel;
using TaskManagerModel.Components;
using TaskManagerPresenter;
using TaskManagerPresenter.Components;
using TaskManagerView.Components;

namespace TaskManagerDevelopmentTests
{
    public static class InjectedDevTests
    {
        public static void PresenterAdd10TasksTest(IPresenter taskManagerPresenter)
        {
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                taskManagerPresenter.AddTask(new UserTaskView
                {
                    Name = rnd.Next(0, 1000).ToString(),
                    Description = rnd.Next(0, 1000).ToString(),
                    IsNotified = false,
                    Priority = (TaskPriority)Enum.Parse(typeof(TaskPriority), rnd.Next(1, 3).ToString()),
                    TaskDate = DateTime.Today,
                    NotifyDate = DateTime.Today
                });
            }
        }
    }
}
