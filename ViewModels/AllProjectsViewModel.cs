using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BugTracker.Models;
namespace BugTracker.ViewModels{
    public class AllProjectsViewModel{
        public List<Project> projects;
        [Required]
        public string Title{get;set;}
    }
}