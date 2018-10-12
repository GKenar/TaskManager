﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using TaskManager.Views;
using TaskManager.DataModels;

namespace TaskManager.Presenters
{
    public class TaskManagerPresenter : ITaskManagerPresenter
    {
        private IMainWindow _mainWindow;
        private IDataModel _dataModel;

        public TaskManagerPresenter(IMainWindow mainWindow, IDataModel dataModel)
        {
            _mainWindow = mainWindow;
            _dataModel = dataModel;

            _dataModel.TasksDBUpdated += dataModel_TasksDBUpdated;

            _mainWindow.SetUserTasksToTasksList(LoadTasksOfDay(DateTime.Now));
            _mainWindow.CurrentCalendarDateChanged += mainWindow_CurrentCalendarDateChanged;
            _mainWindow.Show();

            //Test
            //LoadAllTasks();
        }

        private void dataModel_TasksDBUpdated(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void mainWindow_CurrentCalendarDateChanged(object sender, TaskDateEventArg e)
        {
            _mainWindow.SetUserTasksToTasksList(LoadTasksOfDay(e.Date));
        }

        public void AddTask(UserTaskView task)
        {
            throw new NotImplementedException();
        }

        public void EditTask(UserTaskView task)
        {
            throw new NotImplementedException();
        }

        public void RemoveTask(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserTaskView> LoadAllTasks()
        {
            List<UserTask> tasks = _dataModel.GetAllTasks();

            List<UserTaskView> tasksForView = new List<UserTaskView>();

            foreach (var task in tasks)
            {
                tasksForView.Add(
                    new UserTaskView {
                        Id = task.Id,
                        Name = task.Name,
                        Description = task.Description,
                        Priority = TaskPriority.High, //
                        TaskDate = DateTime.Parse(task.TaskDate.Date),
                        NotifyDate = DateTime.Parse(task.NotifyDate.Date)
                    });
            }

            return tasksForView;
        }

        public List<UserTaskView> LoadTasksOfDay(DateTime day)
        {
            List<UserTask> tasks = _dataModel.GetTasksOfDay(day.ToShortDateString());

            List<UserTaskView> tasksForView = new List<UserTaskView>();

            foreach (var task in tasks)
            {
                tasksForView.Add(
                    new UserTaskView
                    {
                        Id = task.Id,
                        Name = task.Name,
                        Description = task.Description,
                        Priority = TaskPriority.High, //
                        TaskDate = DateTime.Parse(task.TaskDate.Date),
                        NotifyDate = DateTime.Parse(task.NotifyDate.Date)
                    });
            }

            return tasksForView;
        }
    }
}
