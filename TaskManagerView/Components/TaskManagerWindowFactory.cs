namespace TaskManagerView.Components
{
    public class TaskManagerWindowFactory : ITaskManagerWindowFactory
    {
        public ITasksManagerWindow ShowTaskManagerWindow()
        {
            return new TasksManagerWindow();
        }
    }
}
