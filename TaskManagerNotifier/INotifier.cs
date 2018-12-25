using System;

namespace TaskManagerNotifier
{
    public interface INotifier
    {
        event Action ShowMainWindow;
        void Run();
    }
}
