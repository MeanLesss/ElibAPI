using ElibAPI.Data;
using ELibAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ELibAPI.Filters;
public class UserTokenFilterAttribute : ActionFilterAttribute{
    private LibDbContext? ctx;
    private ILogger<UserTokenFilterAttribute>? logger;

    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ctx = context.HttpContext.RequestServices.GetService<LibDbContext>();
        logger = context.HttpContext.RequestServices.GetService<ILogger<UserTokenFilterAttribute>>();
        var args = context.ActionArguments;
        Console.WriteLine("Logger is "+logger);
        UserTokenDTO? token = null;
        foreach(var k in args.Keys){
            Console.WriteLine("{0}: {1}\n",k,args[k]);
            if(args[k] is UserTokenDTO){
                token = args[k] as UserTokenDTO;
                break;
            }
        }
        if(token !=null && token.User_token != null){
            var user = ctx?.Users.FirstOrDefault(u=>u.Token !=null && u.Token==(token.User_token));
            if(user == null){
                context.HttpContext.Response.StatusCode=400;
                context.Result = new ObjectResult(
                    new {status="FAILED",error="Invalid token."});    
            }
        }else{
            context.HttpContext.Response.StatusCode=400;
            context.Result = new ObjectResult(
                    new {status="FAILED",error="No token."});
        }
    }
}