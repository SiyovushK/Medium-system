using Domain.Entities;
using Domain.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController
{
    CommentService commentService = new();

    [HttpGet]
    public async Task<Response<List<Comment>>> GetAllAsync()
    {
        return await commentService.GetAllAsync();
    }
    
    [HttpGet("{id:int}")]
    public async Task<Response<Comment>> GetByIdAsync(int id)
    {
        return await commentService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<Response<string>> CreateAsync(Comment comment)
    {
        return await commentService.CreateAsync(comment);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(Comment comment)
    {
        return await commentService.UpdateAsync(comment);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int ID)
    {
        return await commentService.DeleteAsync(ID);
    }
}