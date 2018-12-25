using System;
using System.Collections.Generic;
using TaskManagerCommon.Components;

namespace TaskManagerModel
{
    public interface IDataModel
    {
        FilterType Filter { get; set; }
        SortType Sort { get; set; }
        void AddTask(UserTask task);
        void UpdateTask(UserTask task);
        void DeleteTask(long id);
        void DeleteTasks(List<long> tasksIdList);
        void DeleteAllTasks();
        void DeleteCompletedTasks(DateTime today);
        List<UserTask> GetAllTasks();
        List<UserTask> GetTasksOfDay(DateTime date);
        List<UserTask> GetTasksOfDays(DateTime beginDate, DateTime endDate);
        List<DateTime> GetAllTaskDates();
        List<DateTime> GetTaskNotifyDates(DateTime day); 
        event EventHandler TasksDbUpdated;
    }
}
