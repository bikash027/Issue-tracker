using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BugTracker.Data;
using BugTracker.Models;
using BugTracker.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace BugTracker.Controllers{
    public class ProjectsController:Controller{
        private readonly BugTrackerContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public ProjectsController(BugTrackerContext context,
                                  SignInManager<ApplicationUser> signInManager,
                                  UserManager<ApplicationUser> userManager){
            this.signInManager=signInManager;
            this.context= context;
            this.userManager=userManager;
        }
        [Authorize]
        public async Task<IActionResult> Index(){
            var user=await userManager.GetUserAsync(User);
            var projects= new List<Project>();
            var projectUsers=from pu in context.projectUsers
                            select pu;
            projectUsers=projectUsers.Where(pu=> pu.User==user);
            foreach (var item in projectUsers)
            {
                projects.Add(await context.Projects.FindAsync(item.ProjectId));
            }
            var allProjectsVM= new AllProjectsViewModel{
                projects= projects,
            };
            // ViewData["projects"]=projects;
            return View(allProjectsVM);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index(AllProjectsViewModel allProjectsVM){
            if(ModelState.IsValid){
                var project=new Project{
                    Title= allProjectsVM.Title
                };
                await context.Projects.AddAsync(project);
                await context.SaveChangesAsync();
                var projectUser=new ProjectUser();
                projectUser.Project=project;
                var user=await userManager.GetUserAsync(User);
                projectUser.User=user;
                await context.projectUsers.AddAsync(projectUser);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Tasks(int projectId){
            var project=await context.Projects.FindAsync(projectId);
            
            return View();
        }
    }
}