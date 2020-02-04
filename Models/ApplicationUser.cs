using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
namespace BugTracker.Models{
    public class ApplicationUser: IdentityUser{
        public ICollection<ProjectUser> ProjectUsers{get;set;}
        public ICollection<Task> Tasks{get;set;}

        public string Name{get;set;}

    }
}