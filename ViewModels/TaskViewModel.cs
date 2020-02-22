using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BugTracker.Models;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace BugTracker.ViewModels{
    public class TaskViewModel{
        [Required]
        public string Description{get;set;}
        public int ProjectId{get;set;}
        [DataType(DataType.Date)]
        public DateTime Deadline{get;set;}
        public string UserId{get;set;}
        public string UserName{get;set;}
        public SelectList AllUserNames{get;set;}
        public SelectList AllUserIds{get;set;}
    }
}