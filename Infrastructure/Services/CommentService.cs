using System.Net;
using Dapper;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;

namespace Infrastructure.Services;

public class CommentService
{
    DataContextAsync _context = new();

    public async Task<Response<List<Comment>>> GetAllAsync()
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"SELECT * FROM Comments";
        var result = await connection.QueryAsync<Comment>(sql);

        return new Response<List<Comment>>(result.ToList());
    }

    public async Task<Response<Comment>> GetByIdAsync(int ID)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"SELECT * FROM Comments WHERE id = @Id";
        var result = await connection.QueryFirstOrDefaultAsync<Comment>(sql, new {Id = ID});

        return result == null
            ? new Response<Comment>(HttpStatusCode.NotFound, "Not found")
            : new Response<Comment>(result);
    }

    public async Task<Response<string>> CreateAsync(Comment comment)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"
            INSERT INTO Comments(articleId, userId, content, createdAt)
            VALUES
            (@articleId, @userId, @content, @createdAt)
        ";
        var result = await connection.ExecuteAsync(sql, comment);

        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Something went wrong")
            : new Response<string>("Comment created successefully");
    }

    public async Task<Response<string>> UpdateAsync(Comment comment)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"
            UPDATE Comments
            SET articleId=@articleId, userId=@userId, content=@content, createdAt=@createdAt
            WHERE id = @Id
        ";
        var result = await connection.ExecuteAsync(sql, comment);

        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Something went wrong")
            : new Response<string>("Comment updated successefully");
    }

    public async Task<Response<string>> DeleteAsync(int ID)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"
            DELETE FROM Comments
            WHERE id = @Id
        ";
        var result = await connection.ExecuteAsync(sql, new {Id = ID});

        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Something went wrong")
            : new Response<string>("Comment deleted successefully");
    }
}