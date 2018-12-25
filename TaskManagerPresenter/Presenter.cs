using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerCommon.Components;
using TaskManagerModel;
using TaskManagerPresenter.Components;
using TaskManagerView;
using TaskManagerView.Components;

namespace TaskManagerPresenter
{
    public class Presenter : IPresenter
    {
        private readonly IMainWindow _mainWindow;
        private readonly IDataModel _dataModel;
        private readonly IPriorityConverter _priorityConverter;

        public Presenter(IMainWindow mainWindow, IDataModel dataModel, IPriorityConverter priorityConverter)
        {
            _mainWindow = mainWindow;
            _dataModel = dataModel;
            _priorityConverter = priorityConverter;
        }

        public void Initialize()
        {
            _mainWindow.Initialize();
            _mainWindow.EnableEditRemoveControls(false);
        }

        public void SortTypeChange(SortType sort)
        {
            if (sort == SortType.Undefined)
                throw new ArgumentException($"{nameof(sort)} is Undefined");

            _dataModel.Sort = sort;
        }

        public void FilterTypeChange(FilterType filter)
        {
            if (filter == FilterType.Undefined)
                throw new ArgumentException($"{nameof(filter)} is Undefined");

            _dataModel.Filter = filter;
        }

        public void ShowWindow()
        {
            try
            {
                _mainWindow.Show();
            }
            catch (Exception)
            {
                
            }
        }

        public void SelectionListUpdated()
        {
            _mainWindow.EnableEditRemoveControls(_mainWindow.TaskSelected);
        }

        public void AddTask(UserTaskView task)
        {
            if (task == null)
                throw new NullTaskException();

            try
            {
                UserTask userTask = new UserTask
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    Priority = _priorityConverter.ConvertToModelPriority(task.Priority),
                    IsNotified = task.IsNotified,
                    TaskDate = task.TaskDate,
                    NotifyDate = task.NotifyDate
                };

                _dataModel.AddTask(userTask);
            }
            catch (Exception ex)
            {
                _mainWindow.ShowMessageBox("Возникла ошибка при добавлении задачи\n"
                                          + ex.Message);
            }
        }

        public void EditTask(UserTaskView task)
        {
            if (task == null)
                throw new NullTaskException();

            try
            {
                UserTask userTask = new UserTask
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    Priority = _priorityConverter.ConvertToModelPriority(task.Priority),
                    IsNotified = task.IsNotified,
                    TaskDate = task.TaskDate,
                    NotifyDate = task.NotifyDate
                };

                _dataModel.UpdateTask(userTask);
            }
            catch (Exception ex)
            {
                _mainWindow.ShowMessageBox("Возникла ошибка при редактировании задачи"
                                          + ex.Message);
            }
        }

        public void RemoveTask(UserTaskView task)
        {
            if (task == null)
                throw new NullTaskException();

            try
            {
                _dataModel.DeleteTask(task.Id);
            }
            catch (Exception)
            {
                _mainWindow.ShowMessageBox("Возникла ошибка при удалении задачи");
            }
        }

        public void RefreshViewTasksList(DateInterval dateInterval)
        {
            try
            {
                _mainWindow.SetUserTasksToTasksList(LoadTasksOfDays(dateInterval));
            }
            catch (Exception)
            {
                _mainWindow.ShowMessageBox("Неудалось обновить список задач :C");
            }
        }

        public void RefreshViewHighlightList()
        {
            try
            {
                var tasksDates = _dataModel.GetAllTaskDates().ToList();
                _mainWindow.SetHighlightDates(tasksDates);
            }
            catch (Exception)
            {
                _mainWindow.ShowMessageBox("Неудалось обновить список задач :C");
            }
        }

        public List<UserTaskView> LoadAllTasks()
        {
            List<UserTask> tasks = _dataModel.GetAllTasks();

            try
            {
                var tasksForView = tasks.Select(task => new UserTaskView
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    Priority = _priorityConverter.ConvertToViewPriority(task.Priority),
                    TaskDate = task.TaskDate,
                    NotifyDate = task.NotifyDate,
                    IsNotified = task.IsNotified
                }).ToList();

                return tasksForView;
            }
            catch (Exception ex)
            {
                throw new MappingTaskException(ex.Message, ex);
            }
        }

        public List<UserTaskView> LoadTasksOfDay(DateTime day)
        {
            List<UserTask> tasks = _dataModel.GetTasksOfDay(day);

            try
            {
                var tasksForView = tasks.Select(task => new UserTaskView
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    Priority = _priorityConverter.ConvertToViewPriority(task.Priority),
                    TaskDate = task.TaskDate,
                    NotifyDate = task.NotifyDate,
                    IsNotified = task.IsNotified
                }).ToList();

                return tasksForView;
            }
            catch (Exception ex)
            {
                throw new MappingTaskException(ex.Message, ex);
            }
        }

        public List<UserTaskView> LoadTasksOfDays(DateInterval dateInterval)
        {
            List<UserTask> tasks = _dataModel.GetTasksOfDays(
                dateInterval.BeginDate,
                dateInterval.EndDate
                );

            try
            {
                var tasksForView = tasks.Select(task => new UserTaskView
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    Priority = _priorityConverter.ConvertToViewPriority(task.Priority),
                    TaskDate = task.TaskDate,
                    NotifyDate = task.NotifyDate,
                    IsNotified = task.IsNotified
                }).ToList();

                return tasksForView;
            }
            catch (Exception ex)
            {
                throw new MappingTaskException(ex.Message, ex);
            }
        }
    }
}
