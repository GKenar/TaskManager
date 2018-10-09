﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        private ITaskManagerPresenter _presenter;

        private EditTaskWindow _addOrEditTaskWindow;

        private TasksManagerWindow _tasksManagerWindow;

        public MainWindow(EditTaskWindow addOrEditTaskWindow, TasksManagerWindow tasksManagerWindow)
        {
            _addOrEditTaskWindow = addOrEditTaskWindow;
            _tasksManagerWindow = tasksManagerWindow;

            InitializeComponent();
        }

        public void BindPresenter(ITaskManagerPresenter presenter)
        {
            _presenter = presenter;
        }

        public void SetUserTasksToTasksList(List<UserTaskView> tasks)
        {
            TaskList.ItemsSource = tasks;
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            var calendar = sender as Calendar;

            if(calendar.SelectedDate.HasValue)
            {
                TaskList.ItemsSource = _presenter.LoadTasksOfDay(calendar.SelectedDate.Value);
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonControl_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
