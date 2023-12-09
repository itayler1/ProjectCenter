using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace gameCenter.Projects.TodoList.Models
{
    internal class TodoListModel
    {

        public ObservableCollection<TodoTask> Tasks { get; set; }

        public TodoListModel()
        {
            Tasks = new ObservableCollection<TodoTask>();
        }
        public void UpdateTask(int taskId, string newDescription)
        {
            TodoTask task = Tasks.FirstOrDefault(task => task.Id == taskId);
            if (task != null)
            {
                task.Description = newDescription;
            }
            else
            {
                throw new Exception("The task with this Id wasn't found");
            }
        }
        public void DeleteTask(int taskId, string newDescription)
        {
            TodoTask task = Tasks.FirstOrDefault(task => task.Id == taskId);
            if (task != null)
            {
                Tasks.Remove(task);
            }
            else
            {
                throw new Exception("The task with this Id wasn't found");
            }
        }
        public void ToggleTaskIsComplete(int taskId)
        {
            TodoTask task = Tasks.FirstOrDefault(task => task.Id == taskId);
            if (task != null)
            {
                task.IsComplete = !task.IsComplete;
            }
            else
            {
                throw new Exception("The task with this Id wasn't found");
            }
        }
        public void AddNewTask(TodoTask task)
        {
            Tasks.Add(task);
        }

        public void ReadTaskMemory()
        {
            if (!File.Exists("task.txt"))
            {
                using (File.Create("task.txt"))
                {

                };
            }
            try 
            {
                using (StreamReader streamReader = new StreamReader("task.txt"))
                {
                    
                    string line;
                    while ((line  = streamReader.ReadLine()) != null) 
                    {
                        string[] taskAtributes = line.Split(',');
                        bool isComp = taskAtributes[3] == "true"? true : false;
                        TodoTask tempTask = new TodoTask(int.Parse(taskAtributes[0]), taskAtributes[1], DateTime.Parse(taskAtributes[2]), isComp);
                        Tasks.Add(tempTask);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateTaskMemory()
        {
            File.Delete("task.txt");
            foreach (TodoTask task in Tasks)
            {
                File.AppendAllText("task.txt", $"{task.Id},{task.Description},{task.CreatedDate},{task.IsComplete}\n");
            }
        }

    }

}
