using System.Net;
using Dapper;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;

namespace Infrastructure.Services;

public class ArticleService
{
    DataContextAsync _context = new();

    public async Task<Response<List<Article>>> GetAllAsync()
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"SELECT * FROM articles";
        var result = await connection.QueryAsync<Article>(sql);

        return new Response<List<Article>>(result.ToList());
    }

    public async Task<Response<Article>> GetByIdAsync(int ID)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"SELECT * FROM articles WHERE id = @Id";
        var result = await connection.QueryFirstOrDefaultAsync<Article>(sql, new {Id = ID});

        return result == null
            ? new Response<Article>(HttpStatusCode.NotFound, "Not found")
            : new Response<Article>(result);
    }

    public async Task<Response<string>> CreateAsync(Article article)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"
            INSERT INTO articles(userId, title, content, description, createdAt, status)
            VALUES
            (@userId, @title, @content, @description, @createdAt, @status)
        ";
        var result = await connection.ExecuteAsync(sql, article);

        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Something went wrong")
            : new Response<string>("Article created successefully");
    }

    public async Task<Response<string>> UpdateAsync(Article article)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"
            UPDATE articles
            SET userId=@userId, title=@title, content=@content, description=@description, createdAt=@createdAt, status=@status
            WHERE id = @Id
        ";
        var result = await connection.ExecuteAsync(sql, article);

        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Something went wrong")
            : new Response<string>("Article updated successefully");
    }

    public async Task<Response<string>> DeleteAsync(int ID)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"
            DELETE FROM articles
            WHERE id = @Id
        ";
        var result = await connection.ExecuteAsync(sql, new {Id = ID});

        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Something went wrong")
            : new Response<string>("Article deleted successefully");
    }
}