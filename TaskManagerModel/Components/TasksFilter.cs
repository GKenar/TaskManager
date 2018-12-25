using System;
using System.Linq;
using TaskManagerCommon.Components;

namespace TaskManagerModel.Components
{
    public class TasksFilter : ITaskFilter
    {
        public IQueryable<UserTask> Filter(IQueryable<UserTask> query, FilterType filter)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            IQueryable<UserTask> filterResult;

            switch (filter)
            {
                case FilterType.All:
                    filterResult = query;
                    break;
                case FilterType.HighPriority:
                    filterResult = query.Where(t => t.Priority == (long)TaskPriority.High);
                    break;
                case FilterType.MediumPriority:
                    filterResult = query.Where(t => t.Priority == (long)TaskPriority.Medium);
                    break;
                case FilterType.LowPriority:
                    filterResult = query.Where(t => t.Priority == (long)TaskPriority.Low);
                    break;
                default:
                    throw new ArgumentException(nameof(filter));
            }

            return filterResult;
        }
    }
}
