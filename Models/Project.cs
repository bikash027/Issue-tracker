using System.Collections.Generic;
namespace BugTracker.Models{
    public class Project{
        public int Id{get;set;}
        public string Title{get;set;}
        public ICollection<ProjectUser> ProjectUsers{get;set;}
        public string ManagerId{get;set;}
        public ICollection<Task> Tasks{get;set;}
    }
}