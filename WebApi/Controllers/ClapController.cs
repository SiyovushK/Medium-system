using Domain.Entities;
using Domain.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClapController
{
    ClapService clapService = new();

    [HttpGet]
    public async Task<Response<List<Clap>>> GetAllAsync()
    {
        return await clapService.GetAllAsync();
    }
    
    [HttpGet("{id:int}")]
    public async Task<Response<Clap>> GetByIdAsync(int id)
    {
        return await clapService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<Response<string>> CreateAsync(Clap clap)
    {
        return await clapService.CreateAsync(clap);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(Clap clap)
    {
        return await clapService.UpdateAsync(clap);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteAsync(int ID)
    {
        return await clapService.DeleteAsync(ID);
    }
}