using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using task_management_syst.Models;

namespace task_management_syst.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController:ControllerBase
{
    private IConfiguration _configuration;
    public AuthenticationController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private Users AuthenticateUser(Users user)
    {
        Users _user = null;
        if (user.Username == "string" && user.Password == "string")
        {
            _user = new Users { Username = "Manolas" };
        }

        return _user;
    }

    private string GenerateToken(Users user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(_configuration["jwt:Issuer"], _configuration["jwt:Audience"], null,
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);

    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login(Users user)
    {
        IActionResult response = Unauthorized();
        var authenticateUser = AuthenticateUser(user);
        //if (authenticateUser != null)
        if (true)
        {
            var token = GenerateToken(authenticateUser);
            response = Ok(new { token = token });
        }

        return response;
    }
}