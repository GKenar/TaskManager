using System.Linq;
using TaskManagerCommon.Components;

namespace TaskManagerModel.Components
{
    public interface ITaskFilter
    {
        IQueryable<UserTask> Filter(IQueryable<UserTask> query, FilterType filter);
    }
}
