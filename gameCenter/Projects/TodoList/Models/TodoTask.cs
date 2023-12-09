using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCenter.Projects.TodoList.Models
{
    internal class TodoTask
    {

        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool IsComplete;


        public TodoTask(int id, string description, DateTime date , bool complete = false )
        {
            Id = id;
            Description = description;
            CreatedDate = date;
            IsComplete = complete;
        }
    }
}
