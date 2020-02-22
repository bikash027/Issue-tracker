using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace BugTracker.ViewModels{
    public class TasksViewModel{
        public List<Task> Tasks;
        // [Required]
        // public string Title{get;set;}
        public Project project{get;set;}
        public List<ApplicationUser> projectUsers{get;set;}
        public string NewUser_name{get;set;}
        public string NewUser_id{get;set;}
        public SelectList AllUser_names{get;set;}
        public SelectList AllUser_ids{get;set;}
        public bool isManager{get;set;}
    }
}