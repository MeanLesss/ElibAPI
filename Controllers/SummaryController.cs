using Microsoft.AspNetCore.Mvc;
using ELibAPI.Dtos;
using ElibAPI.Controllers;
using ElibAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Swashbuckle.AspNetCore.Annotations;

namespace ELibAPI.Controllers;

public class SummaryController : ApiBaseController
{
    private readonly LibDbContext _ctx;
    public SummaryController(LibDbContext ctx)
    {
        this._ctx = ctx;
    }
    [HttpGet]
    [Authorize]
    [SwaggerOperation(
        Summary = "Get all summaries for a Librarian,Teacher",
        Description = "Retrieves a list of summaries associated with a specific user Librarian,Teacher."
    )]
    public async Task<IActionResult> GetAllSummary(int user_id)
    {
        var user = await _ctx.Users.FirstOrDefaultAsync(u => u.Id == user_id);
        if (user != null && user.Role == "Librarian")
        {
            return Ok(new
            {
                groups = _ctx.Groups.Count(),
                books = _ctx.Books.Count(),
                students = _ctx.Users.Where(x => x.Role == "Student").Count(),
                teachers = _ctx.Users.Where(x => x.Role == "Teacher").Count(),
                librarian = _ctx.Users.Where(x => x.Role == "Librarian").Count(),
                downloads = _ctx.Downloads.Count(),
                status = "SUCCESS",
                error = ""
            });
        }
        else if (user != null && user.Role == "Teacher")
        {
            return Ok(new
            {
                books = _ctx.Books.Count(),
                groups = _ctx.Groups.Count(),
                status = "SUCCESS",
                error = ""
            });
        }
        else
        {
            return BadRequest(new { status = "FAILED", error = "user_id is invalid." });
        }
    }
}