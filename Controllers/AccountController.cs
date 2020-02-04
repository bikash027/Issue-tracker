using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using BugTracker.Models;
using BugTracker.ViewModels;

namespace BugTracker.Controllers{
    public class AccountController: Controller{
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager){
            this.userManager= userManager;
            this.signInManager= signInManager;
        }
        [HttpGet]
        public IActionResult Register(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM){
            if(ModelState.IsValid){
                var user=new ApplicationUser{
                    UserName= registerVM.Email,
                    Email= registerVM.Email,
                    Name= registerVM.Name
                };
                var result= await userManager.CreateAsync(user, registerVM.Password);
                if(result.Succeeded){
                    await signInManager.SignInAsync(user,isPersistent:false);
                    return RedirectToAction("Index","Projects");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }  
                return View(registerVM);
            }
            return View(registerVM);
        }
        public IActionResult Login(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM){
            if(ModelState.IsValid){
                var result= await signInManager.PasswordSignInAsync(loginVM.Email,loginVM.Password,isPersistent: false,lockoutOnFailure: false);
                if(result.Succeeded){
                    return RedirectToAction("Index","Projects");
                }    
                ModelState.AddModelError("","login failed for some reason");
                return View(loginVM);
            }
            return View(loginVM);
        }
        [HttpPost]
        public async Task<IActionResult> Logout(){
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}