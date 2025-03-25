using System.Net;
using Dapper;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;

namespace Infrastructure.Services;

public class ClapService
{
    DataContextAsync _context = new();

    public async Task<Response<List<Clap>>> GetAllAsync()
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"SELECT * FROM Claps";
        var result = await connection.QueryAsync<Clap>(sql);

        return new Response<List<Clap>>(result.ToList());
    }

    public async Task<Response<Clap>> GetByIdAsync(int ID)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"SELECT * FROM Claps WHERE id = @Id";
        var result = await connection.QueryFirstOrDefaultAsync<Clap>(sql, new {Id = ID});

        return result == null
            ? new Response<Clap>(HttpStatusCode.NotFound, "Not found")
            : new Response<Clap>(result);
    }

    public async Task<Response<string>> CreateAsync(Clap clap)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"
            INSERT INTO Claps(articleId, userId, count, createdAt)
            VALUES
            (@articleId, @userId, @count, @createdAt)
        ";
        var result = await connection.ExecuteAsync(sql, clap);

        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Something went wrong")
            : new Response<string>("Clap created successefully");
    }

    public async Task<Response<string>> UpdateAsync(Clap clap)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"
            UPDATE Claps
            SET articleId=@articleId, userId=@userId, count=@count, createdAt=@createdAt
            WHERE id = @Id
        ";
        var result = await connection.ExecuteAsync(sql, clap);

        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Something went wrong")
            : new Response<string>("Clap updated successefully");
    }

    public async Task<Response<string>> DeleteAsync(int ID)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"
            DELETE FROM Claps
            WHERE id = @Id
        ";
        var result = await connection.ExecuteAsync(sql, new {Id = ID});

        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Something went wrong")
            : new Response<string>("Clap deleted successefully");
    }
}