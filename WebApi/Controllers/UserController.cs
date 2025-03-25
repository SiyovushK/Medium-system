using Domain.Entities;
using Domain.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController
{
    UserService userService = new();

    [HttpGet]
    public async Task<Response<List<User>>> GetAllAsync()
    {
        return await userService.GetAllAsync();
    }
    
    [HttpGet("{id:int}")]
    public async Task<Response<User>> GetByIdAsync(int id)
    {
        return await userService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<Response<string>> CreateAsync(User user)
    {
        return await userService.CreateAsync(user);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(User user)
    {
        return await userService.UpdateAsync(user);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int ID)
    {
        return await userService.DeleteAsync(ID);
    }
}