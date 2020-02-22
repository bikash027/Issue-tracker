using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BugTracker.Data;
using BugTracker.Models;
using BugTracker.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            // var projects= new List<Project>();

            //=============NAIVE WAY==========
            // var projectUsers=from pu in context.projectUsers
            //                 select pu;
            // projectUsers=projectUsers.Where(pu=> pu.User==user);
            // foreach (var item in projectUsers)
            // {
            //     projects.Add(await context.Projects.FindAsync(item.ProjectId));
            // }

            //==========USING JOIN============
            // var query=await context.projectUsers
            //             .Join(context.Projects,
            //                 projectUser=>projectUser.ProjectId,
            //                 project=> project.Id,
            //                 (projectUser,project)=>new{
            //                     userId= projectUser.UserId,
            //                     Id= project.Id,
            //                     Title= project.Title
            //                 })
            //             .Where(pu=> pu.userId==user.Id)
            //             .ToListAsync();
            // foreach(var item in query){
            //     projects.Add(new Project{
            //         Id= item.Id,
            //         Title= item.Title
            //     });
            // }

            //==========EF CORE MAGIC=============
            var projects=await context.projectUsers
                        .Where(pu=> pu.UserId==user.Id)
                        .Select(pu=> pu.Project)
                        .ToListAsync();
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
                var user=await userManager.GetUserAsync(User);
                var project=new Project{
                    Title= allProjectsVM.Title,
                    ManagerId= user.Id
                };
                await context.Projects.AddAsync(project);
                await context.SaveChangesAsync();
                var projectUser=new ProjectUser();
                projectUser.Project=project;
                projectUser.User=user;
                await context.projectUsers.AddAsync(projectUser);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> Tasks(int projectId){
            var project=await context.Projects.FindAsync(projectId);
            var user=await userManager.GetUserAsync(User);
            var isManager= (project.ManagerId==user.Id);
            var users= await context.projectUsers.
                        Where(pu=>pu.ProjectId==projectId).
                        Select(pu=>pu.User).
                        ToListAsync();
            var tasks=from t in context.Tasks select t;
            tasks= tasks.Where(t=> t.ProjectId==projectId);
            var tasksVM= new TasksViewModel{
                Tasks= await tasks.ToListAsync(),
                project= project,
                projectUsers= users,
                isManager= (project.ManagerId==user.Id)
            };
            if(isManager){
                var NewUsers= userManager.Users.
                        Where(u=> !(users.Contains(u)));
                var NewUser_names=new SelectList(NewUsers.Select(u=>u.Name));
                var NewUser_ids=new SelectList(NewUsers.Select(u=>u.Id));
                tasksVM.AllUser_ids=NewUser_ids;
                tasksVM.AllUser_names=NewUser_names;
            }
            return View(tasksVM);
        }
        [HttpPost]
        public async Task<IActionResult> TaskStage(int taskId, string stage){
            var task=await context.Tasks.FindAsync(taskId);
            task.Stage=stage;
            await context.SaveChangesAsync();
            return RedirectToAction("Tasks",new {projectId=task.ProjectId});
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(int projectId,TasksViewModel tasksVM){
            if(ModelState.IsValid){
                var projectUser= new ProjectUser{
                    Project= await context.Projects.FindAsync(projectId),
                    User= await userManager.FindByIdAsync(tasksVM.NewUser_id)
                };
                await context.projectUsers.AddAsync(projectUser);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AddTask(int projectId){
            var project=await context.Projects.FindAsync(projectId);
            
            // var users=userManager.Users
            //             .SelectMany(u=> u.ProjectUsers)
            //             .Where(pu=>pu.ProjectId==projectId)
            //             .Select(pu=>pu.User);
                        // .Where(u=> !u.Tasks.Contains())
            var users=await context.projectUsers
                        .Where(pu=> pu.ProjectId==projectId)
                        .Select(pu=> pu.User)
                        .ToListAsync();
            var userNames=new SelectList(users.Select(u=> u.Name));
            var userIds=new SelectList(users.Select(u=> u.Id));
            var taskVM= new TaskViewModel{
                AllUserNames=userNames,
                AllUserIds=userIds,
                ProjectId=projectId
            };
            return View(taskVM);
        }
        [HttpPost]
        public async Task<IActionResult> AddTask(int projectId,TaskViewModel taskVM){
            if(ModelState.IsValid){
                    var task=new BugTracker.Models.Task{
                    Description= taskVM.Description,
                    Deadline= taskVM.Deadline,
                    Stage="to do",
                    User=await userManager.FindByIdAsync(taskVM.UserId),
                    Project= await context.Projects.FindAsync(projectId)
                };
                await context.Tasks.AddAsync(task);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(taskVM);
        }
    }
}