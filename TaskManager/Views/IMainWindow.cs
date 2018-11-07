﻿using System;
using System.Collections.Generic;
using TaskManager.ViewComponents;
using TaskManager.Components;

namespace TaskManager.Views
{
    public interface IMainWindow
    {
        bool TaskSelected { get; }
        DateTime DateSelected { get; }
        FilterType ComboFilter { get; }
        void Show();
        void SetUserTasksToTasksList(List<UserTaskView> tasks);
        void EnableEditRemoveControls(bool enable);
        event EventHandler<UserTaskEventArgs> UserTaskUpdated;
        event EventHandler<UserTaskEventArgs> UserTaskAdded;
        event EventHandler<UserTaskEventArgs> UserTaskDeleted;
        event EventHandler<TaskDateEventArg> CurrentCalendarDateChanged;
        event EventHandler SelectionListUpdated;
        event EventHandler FilterTypeChanged;
    }
}
