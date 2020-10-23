using System;
using System.Collections.Generic;
using System.Text;

namespace TaskList
{
    class TaskItem
    {
        // local fields
        private string taskOwner;
        private string description;
        private DateTime dueDate;
        private bool complete;

        // properties
        public string TaskOwner
        {
            get { return taskOwner; }
            set { taskOwner = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }
        public bool Complete
        {
            get { return complete; }
            set { complete = value; }
        }

        // constructor
        public TaskItem(string TaskOwner, string Description, DateTime DueDate)
        {
            taskOwner = TaskOwner;
            description = Description;
            dueDate = DueDate;
            complete = false;
        }
    }
}
