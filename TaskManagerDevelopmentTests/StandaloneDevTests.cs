using System;
using TaskManagerModel;
using TaskManagerModel.Components;
using TaskManagerNotifier;

namespace TaskManagerDevelopmentTests
{
    static class StandaloneDevTests
    {
        static void Main(string[] args)
        {
            NotifierTest();
        }

        private static void NotifierTest()
        {
            ITaskFilter taskFilter = new TasksFilter();

            IDataModel dataModel = new DataModel(new UserTasksDbFactory(), taskFilter);
            INotifier notifier = new Notifier(dataModel);

            ((IDisposable)notifier).Dispose();
        }
    }
}
