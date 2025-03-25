using Domain.Entities;
using Domain.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticleController
{
    ArticleService articleService = new();

    [HttpGet]
    public async Task<Response<List<Article>>> GetAllAsync()
    {
        return await articleService.GetAllAsync();
    }
    
    [HttpGet("{id:int}")]
    public async Task<Response<Article>> GetByIdAsync(int id)
    {
        return await articleService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<Response<string>> CreateAsync(Article article)
    {
        return await articleService.CreateAsync(article);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(Article article)
    {
        return await articleService.UpdateAsync(article);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int ID)
    {
        return await articleService.DeleteAsync(ID);
    }
}