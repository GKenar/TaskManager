﻿using DataModels;
using System;
using System.Linq;
using TaskManager.DataModels;

namespace TaskManager.DataModelComponents
{
    public class TasksFilter : ITaskFilter
    {
        public void Filter(IQueryable<UserTask> query, FilterType filter)
        {
            if (query == null)
                throw new ArgumentException("Query is null");

            switch(filter)
            {
                case FilterType.HighPriority:
                    query = query.Where(t => t.Priority == TaskPriority.High.ToString());
                    break;
                case FilterType.MediumPriority:
                    query = query.Where(t => t.Priority == TaskPriority.Medium.ToString());
                    break;
                case FilterType.LowPriority:
                    query = query.Where(t => t.Priority == TaskPriority.Low.ToString());
                    break;
            }
        }
    }
}
