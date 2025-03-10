using ElibAPI.Controllers.Global_Function;
using ElibAPI.Data;
using ElibAPI.Dtos;
using ElibAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElibAPI.Controllers;

public class LoginController : ApiBaseController
{
    private LibDbContext _ctx;
    IConfiguration _configuration;
    private Global global = new Global();
    public LoginController(LibDbContext ctx,IConfiguration configuration)
    {
        this._ctx = ctx;
        _configuration = configuration;
        global = new Global(_configuration);
    }

    [HttpGet("get-all-user")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Object>>> GetAllUsers()
    {
        var userId = HttpContext.User.FindFirst("Id")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { status = "FAILED", error = "Invalid token." });
        }

        return await _ctx.Users.Select(x=>new { 
            GroupId=x.GroupId,
            Id=x.Id,
            Role=x.Role,
            Username = x.Username,
            Password=string.Empty
        
        }).ToListAsync();
    }

    [HttpPost("login")]
    [Produces("application/json")] 
    public Object Post([FromForm]Credentials userCred)
    {
        if (string.IsNullOrEmpty(userCred.Username) || string.IsNullOrEmpty(userCred.Password)) return BadRequest(new { error = "Invalid Credential!" });
        var str = global.CreateMD5(userCred.Password);
        var user = _ctx.Users.FirstOrDefault(u => u.Username == userCred.Username
            && u.Pwd == str);

        if(user == null) return Response.StatusCode = 400;
        user.Token = global.GenerateJWTToken(user);
        user.Pwd = string.Empty;
        return new
        { 
            errror = "",
            user = user
        };
    }
    [HttpPost("register")]
    [Produces("application/json")] 
    public ActionResult Post([FromForm] UserDTO userDto)
    {
        if (string.IsNullOrEmpty(userDto.Username) || string.IsNullOrEmpty(userDto.Password)) return BadRequest(new { error = "Invalid Credential!"});
        // Check if user already exists
        var existingUser = _ctx.Users.FirstOrDefault(u => u.Username == userDto.Username);

        if (existingUser != null)
        {
            // Return BadRequest if user already exists
            return BadRequest(new
            {
                error = "User already exists"
            });
        }

        // Hash password before saving
        var str = global.CreateMD5(userDto.Password);

        // Create new user (add to database)
        var newUser = new User
        {
            Username = userDto.Username,
            Pwd = str,
            GroupId = userDto.GroupId,
            Role = userDto.Role 
        };
        newUser.Token = global.GenerateJWTToken(newUser);

        _ctx.Users.Add(newUser);
        _ctx.SaveChanges();

        // Return OK with user details
        return Ok(new
        {
            token = newUser.Token,
            error = "",
            user = newUser
        });
    }

    [HttpPost("update-user")]
    [Produces("application/json")] 
    public async Task<ActionResult> UpdateUser([FromBody] UserDTO userDto)
    {
        if (string.IsNullOrEmpty(userDto.Username))
            return BadRequest(new { error = "Username is required!" });
         
        var existingUser = _ctx.Users.FirstOrDefault(u => u.Id == userDto.Id);

        if (existingUser == null)
        { 
            return NotFound(new { error = "User not found" });
        }
         
        if (!string.IsNullOrEmpty(userDto.Password))
        {
            existingUser.Pwd = global.CreateMD5(userDto.Password);
        }
         
        if (userDto.GroupId != null)
        {
            existingUser.GroupId = userDto.GroupId;
        }
         
        if (!string.IsNullOrEmpty(userDto.Role))
        {
            existingUser.Role = userDto.Role;
        } 
        //existingUser.Token = global.GenerateJWTToken(existingUser);

        _ctx.Users.Update(existingUser); 
        await _ctx.SaveChangesAsync();
         
        return Ok(new
        {
            token = existingUser.Token,
            error = "",
            user = existingUser
        });
    }



}
public class Credentials
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}