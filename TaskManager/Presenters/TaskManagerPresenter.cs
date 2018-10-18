﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using TaskManager.Views;
using TaskManager.DataModels;
using TaskManager.Components;

namespace TaskManager.Presenters
{
    public class TaskManagerPresenter : ITaskManagerPresenter
    {
        private IMainWindow _mainWindow;
        private IDataModel _dataModel;
        private readonly IDateConverter _dateConverter;

        public TaskManagerPresenter(IMainWindow mainWindow, IDataModel dataModel, IDateConverter dateConverter)
        {
            _mainWindow = mainWindow;
            _dataModel = dataModel;
            _dateConverter = dateConverter;

            _dataModel.TasksDBUpdated += dataModel_TasksDBUpdated;

            _mainWindow.EnableEditRemoveControls(false);
            _mainWindow.SetUserTasksToTasksList(LoadTasksOfDay(_mainWindow.DateSelected));
            _mainWindow.CurrentCalendarDateChanged += mainWindow_CurrentCalendarDateChanged;
            _mainWindow.SelectionListUpdated += mainWindow_SelectionListUpdated;
            _mainWindow.UserTaskAdded += mainWindow_UserTaskAdded;
            _mainWindow.UserTaskUpdated += mainWindow_UserTaskUpdated;
            _mainWindow.UserTaskDeleted += mainWindow_UserTaskDeleted;
            _mainWindow.Show();
        }

        private void mainWindow_UserTaskDeleted(object sender, UserTaskEventArgs e)
        {
            RemoveTask(e.UserTaskView);
        }

        private void mainWindow_UserTaskUpdated(object sender, UserTaskEventArgs e)
        {
            EditTask(e.UserTaskView);
        }

        private void mainWindow_UserTaskAdded(object sender, UserTaskEventArgs e)
        {
            AddTask(e.UserTaskView);
        }

        private void mainWindow_SelectionListUpdated(object sender, EventArgs e)
        {
            _mainWindow.EnableEditRemoveControls(_mainWindow.TaskSelected);
        }

        private void dataModel_TasksDBUpdated(object sender, EventArgs e)
        {
            _mainWindow.SetUserTasksToTasksList(LoadTasksOfDay(_mainWindow.DateSelected));
        }

        private void mainWindow_CurrentCalendarDateChanged(object sender, TaskDateEventArg e)
        {
            _mainWindow.SetUserTasksToTasksList(LoadTasksOfDay(e.Date));
        }

        public void AddTask(UserTaskView task)
        {
            if (task == null)
                throw new NullTaskException();

            UserTask userTask = new UserTask
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Priority = task.Priority.ToString(),
                IsNotified = 0
            };

            userTask.TaskDate = _dateConverter.ConvertDateToString(task.TaskDate);
            userTask.NotifyDate = _dateConverter.ConvertDateToString(task.NotifyDate);

            _dataModel.AddTask(userTask);
        }

        public void EditTask(UserTaskView task)
        {
            if (task == null)
                throw new NullTaskException();

            UserTask userTask = new UserTask
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Priority = task.Priority.ToString(),
                IsNotified = 0
            };

            userTask.TaskDate = _dateConverter.ConvertDateToString(task.TaskDate);
            userTask.NotifyDate = _dateConverter.ConvertDateToString(task.NotifyDate);

            _dataModel.UpdateTask(userTask);
        }

        public void RemoveTask(UserTaskView task)
        {
            if (task == null)
                throw new NullTaskException();

            UserTask userTask = new UserTask
            {
                Id = task.Id
            };

            _dataModel.DeleteTask(userTask);
        }

        public List<UserTaskView> LoadAllTasks()
        {
            List<UserTask> tasks = _dataModel.GetAllTasks();

            List<UserTaskView> tasksForView = new List<UserTaskView>();
            try
            {
                foreach (var task in tasks)
                {
                    tasksForView.Add(
                        new UserTaskView {
                            Id = task.Id,
                            Name = task.Name,
                            Description = task.Description,
                            Priority = TaskPriority.High, //
                            TaskDate = _dateConverter.ParseDateToString(task.TaskDate),
                            NotifyDate = _dateConverter.ParseDateToString(task.NotifyDate)
                        });
                }
                return tasksForView;
            }
            catch(Exception ex)
            {
                throw new MappingTaskException(ex.Message, ex);
            }
        }

        public List<UserTaskView> LoadTasksOfDay(DateTime day)
        {
            List<UserTask> tasks = _dataModel.GetTasksOfDay(_dateConverter.ConvertDateToString(day));

            List<UserTaskView> tasksForView = new List<UserTaskView>();

            try
            {
                foreach (var task in tasks)
                {
                    tasksForView.Add(
                        new UserTaskView
                        {
                            Id = task.Id,
                            Name = task.Name,
                            Description = task.Description,
                            Priority = TaskPriority.High, //
                            TaskDate = _dateConverter.ParseDateToString(task.TaskDate),
                            NotifyDate = _dateConverter.ParseDateToString(task.NotifyDate)
                });
                }
                return tasksForView;
            }
            catch(Exception ex)
            {
                throw new MappingTaskException(ex.Message, ex);
            }
        }
    }
}
