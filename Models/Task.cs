using System;
using System.ComponentModel.DataAnnotations;
namespace BugTracker.Models{
    public class Task{
        public int Id{get;set;}
        public Project Project{get;set;}
        public int ProjectId{get;set;}
        public ApplicationUser User{get;set;}
        public string UserId{get;set;}
        [Required]
        public string Description{get;set;}
        public DateTime Deadline{get;set;}
        [Required]
        public string Stage{get;set;}
    }
}