using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BugTracker.Models;
namespace BugTracker.ViewModels{
    public class TasksViewModel{
        public List<Task> Tasks;
        [Required]
        public string Title{get;set;}
    }
}