using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminUsersController : Controller
{
    private readonly IUserRepository _userRepository;

    public AdminUsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<IActionResult> List()
    {
        var users = await _userRepository.GetAll();
        var usersViewModel = new UserViewModel
        {
            Users = new List<User>()
        };
        foreach (var user in users)
        {
            usersViewModel.Users.Add(new Models.ViewModels.User
            {
                Id = Guid.Parse(user.Id),
                Username = user.UserName,
                EmailAddress = user.Email
            });
        }
        return View(usersViewModel);
    }
}