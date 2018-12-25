using System.Linq;
using TaskManagerCommon.Components;

namespace TaskManagerModel.Components
{
    public static class TasksSorter
    {
        public static IQueryable<UserTask> Sort(this IQueryable<UserTask> tasks, SortType sortType)
        {
            IQueryable<UserTask> sorted;
            switch (sortType)
            {
                case SortType.AscendingPriority:
                    sorted = tasks.OrderBy(t => t.Priority);
                    break;
                case  SortType.DescendingPriority:
                    sorted = tasks.OrderByDescending(t => t.Priority);
                    break;
                default:
                    sorted = tasks;
                    break;
            }
            return sorted;
        } 
    }
}
