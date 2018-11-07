﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskManager.ViewComponents;
using TaskManager.Components;

namespace TaskManager.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        public bool TaskSelected { get; private set; }

        public DateTime DateSelected { get; private set; }

        public FilterType ComboFilter { get; set; } = FilterType.All;

        public event EventHandler<UserTaskEventArgs> UserTaskUpdated = delegate { };

        public event EventHandler<UserTaskEventArgs> UserTaskAdded = delegate { };

        public event EventHandler<UserTaskEventArgs> UserTaskDeleted = delegate { };

        public event EventHandler<TaskDateEventArg> CurrentCalendarDateChanged = delegate { };

        public event EventHandler SelectionListUpdated = delegate { };

        public event EventHandler FilterTypeChanged = delegate { };

        public MainWindow()
        {
            DataContext = this;
            DateSelected = DateTime.Now;

            InitializeComponent();
        }

        public void SetUserTasksToTasksList(List<UserTaskView> tasks)
        {
            TaskList.ItemsSource = tasks;
        }

        public void EnableEditRemoveControls(bool enable)
        {
            buttonEdit.IsEnabled = enable;
            buttonDelete.IsEnabled = enable;
        }

        private void TaskList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;

            if (listBox != null)
                TaskSelected = listBox.SelectedItem != null;

            SelectionListUpdated(this, new EventArgs());
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) //Использовать e?
        {
            var calendar = sender as Calendar;

            if (calendar != null && calendar.SelectedDate.HasValue)
            {
                DateSelected = calendar.SelectedDate.Value;
                CurrentCalendarDateChanged(this, new TaskDateEventArg(DateSelected));
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            UserTaskView task = new UserTaskView()
            {
                Priority = TaskPriority.Low,
                TaskDate = DateTime.Now,
                NotifyDate = DateTime.Now
            };

            IEditTaskWindow editTaskWindow = new EditTaskWindow(task);
            bool? dialogResult = editTaskWindow.ShowDialog();

            if (dialogResult == true)
            {
                UserTaskAdded(this, new UserTaskEventArgs(task));
            }
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (TaskSelected == false)
                return;

            UserTaskView task = TaskList.SelectedItem as UserTaskView;

            if (task == null)
                return;

            IEditTaskWindow editTaskWindow = new EditTaskWindow(task);
            bool? dialogResult = editTaskWindow.ShowDialog();

            if (dialogResult == true)
            {
                UserTaskUpdated(this, new UserTaskEventArgs(task));
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (TaskSelected == false)
                return;

            UserTaskView task = TaskList.SelectedItem as UserTaskView;

            if (task == null)
                return;

            UserTaskDeleted(this, new UserTaskEventArgs(task));
        }

        private void ButtonControl_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Calendar_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender != null && Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
                Mouse.Capture(null);
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterTypeChanged(this, EventArgs.Empty);
        }
    }
}
