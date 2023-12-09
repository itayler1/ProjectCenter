
using gameCenter.Projects.TodoList.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace gameCenter.Projects.TodoList
{
    /// <summary>
    /// Interaction logic for TodoList.xaml
    /// </summary>
    public partial class TodoList : Window
    {
        private TodoListModel _todoList = new TodoListModel();

        public TodoList()
        {
            InitializeComponent();
            InitializeTasks();
        }

        private void InitializeTasks()
        {
            _todoList.ReadTaskMemory();
            listTasks.ItemsSource = _todoList.Tasks;

            foreach (var item in listTasks.Items)
            {
                if (item is TodoTask todoTask && todoTask.IsComplete)
                {
                    ListBoxItem container = (ListBoxItem)listTasks.ItemContainerGenerator.ContainerFromItem(item);

                    if (container != null)
                    {
                        CheckBox checkBox = container.FindName("chkTask") as CheckBox;

                        if (checkBox != null)
                        {
                            checkBox.IsChecked = true;
                        }
                    }
                }
            }

        }

        private void OnTaskToggled(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is TodoTask task)
            {
                _todoList.ToggleTaskIsComplete(task.Id);
            }
        }


        private void OnTextBlockMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                TextBlock textBlock = sender as TextBlock;
                StackPanel parent = textBlock.Parent as StackPanel;
                TextBox editTextBox = parent.FindName("editTaskDescription") as TextBox;
                Button btnSave = parent.FindName("btnSave") as Button;
                Button btnDelete = parent.FindName("btnDelete") as Button;

                textBlock.Visibility = Visibility.Collapsed;
                editTextBox.Visibility = Visibility.Visible;
                btnSave.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;

                editTextBox.Text = textBlock.Text;
            }
        }

        private void OnSaveEdit(object sender, RoutedEventArgs e)
        {

            Button btnSave = sender as Button;
            StackPanel parent = btnSave.Parent as StackPanel;
            Button btnDelete = parent.FindName("btnDelete") as Button;
            TextBox editTextBox = parent.FindName("editTaskDescription") as TextBox;
            TextBlock textBlock = parent.FindName("txtTaskDescription") as TextBlock;
            TodoTask task = textBlock.DataContext as TodoTask;

            editTextBox.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;

            textBlock.Visibility = Visibility.Visible;

            textBlock.Text = editTextBox.Text;
            _todoList.UpdateTask(task.Id, editTextBox.Text);
        }

        private void OnDelete (object sender, RoutedEventArgs e)
        {
            Button btnDelete = sender as Button;
            StackPanel parent = btnDelete.Parent as StackPanel;
            Button btnSave = parent.FindName("btnSave") as Button;
            TextBox editTextBox = parent.FindName("editTaskDescription") as TextBox;
            TextBlock textBlock = parent.FindName("txtTaskDescription") as TextBlock;
            TodoTask task = textBlock.DataContext as TodoTask;

            editTextBox.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;

            textBlock.Visibility = Visibility.Visible;

            textBlock.Text = editTextBox.Text;
            _todoList.DeleteTask(task.Id, editTextBox.Text);
        }

        private void OnAddTask(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewTask.Text))
            {
                TodoTask newTask = new TodoTask(_todoList.Tasks.Count + 1, txtNewTask.Text, DateTime.Now, false);
                _todoList.AddNewTask(newTask);
                txtNewTask.Clear();
            }
        }

        private void SaveList(object sender, RoutedEventArgs e) => _todoList.UpdateTaskMemory();
    }
}


