using ElibAPI.Controllers;
using ElibAPI.Data;
using ElibAPI.Dtos;
using ElibAPI.DTOs;
using ElibAPI.Models;
using ELibAPI.Dtos;
using ELibAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace ELibAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GroupsController : ApiBaseController
{
    private readonly LibDbContext _ctx;
    public GroupsController(LibDbContext ctx)
    {
        _ctx = ctx;
    }

    [HttpPost("add-group")]
    [UserTokenFilter]
    public async Task<IActionResult> DoPost([FromForm] GroupDTO group)
    {
        if (group == null)
        {
            return BadRequest(new
            {
                status = false,
                message = "Required data is invalid!"
            });
        }
        var groupCheck = _ctx.Groups.Where(x => x.Name == group.Name);
        if (groupCheck != null)
        {
            return BadRequest(new
            {
                status = false,
                message = "Group already existed!"
            });
        }
        var grouptoadd = new Group()
        {
            Name = group.Name,
        };
        _ctx.Groups.Add(grouptoadd);
        await _ctx.SaveChangesAsync();

        return Ok(new
        {
            status = true,
            message = $"Group successfully added!"
        });
    }
    
    [HttpPost("update-group")]
    [UserTokenFilter]
    public async Task<IActionResult> DoPost(int group_id, string name,string group_for)
    {
        var group = await _ctx.Groups.FindAsync(group_id);
        if (group == null)
        {
            return BadRequest(new
            {
                status = false,
                message = "Required data is invalid!"
            });
        }
        var groupCheck = _ctx.Groups.Where(x => x.Name == name);
        if (groupCheck != null)
        {
            return BadRequest(new
            {
                status = false,
                message = "Group already existed!"
            });
        }
        group.Name = name;
        group.Group_For = group_for;
         
        _ctx.Groups.Add(group);
        await _ctx.SaveChangesAsync();

        return Ok(new
        {
            status = true,
            message = $"Group successfully added!"
        });
    }

    [HttpPost("add-teacher-to-group")]
    [UserTokenFilter]
    public async Task<IActionResult> DoPost([FromForm] int teacher_id,int group_id)
    {
        var user  = await _ctx.Users.FindAsync(teacher_id);
        if (user == null || user.Role != "Teacher")
        {
            return BadRequest(new
            {
                status = false,
                message = "Required user data is invalid!"
            });
        }
        var group = await _ctx.Groups.FindAsync(group_id);
        if (group == null) {
            return BadRequest(new
            {
                status = false,
                message = "Group not found!"
            });
        }
        var teacherGroup = new TeachersGroup()
        {
            GroupId = group_id,
            TeacherId = teacher_id
        };
        _ctx.TeachersGroups.Add(teacherGroup);
        await _ctx.SaveChangesAsync();
         
        return Ok(new
        {
            status = true,
            message = $"Teacher successfully added to {group.Name}!"
        }); 
    }
    // DELETE: api/Books/5
    [HttpDelete("{group_id}")]
    public async Task<IActionResult> DeleteGroup(int group_id)
    {
        var group = await _ctx.Groups.FindAsync(group_id);
        if (group == null)
        {
            return NotFound(new
            {
                message = "Failed to delete!"
            });
        }

        _ctx.Groups.Remove(group);
        await _ctx.SaveChangesAsync();

        return Ok(new
        {
            message = "Successfully deleted!"
        });
    }





}