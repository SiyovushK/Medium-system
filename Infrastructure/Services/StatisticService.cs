using System.Net;
using Dapper;
using Domain.Entities;
using Domain.Responses;
using Domain.DTOs;
using Infrastructure.Data;

namespace Infrastructure.Services;

public class StatisticService
{
    DataContextAsync _context = new();

    public async Task<Response<List<Article>>> GetUserArticlesAsync(int userID)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"SELECT * FROM articles WHERE userId = @id";
        var result = await connection.QueryAsync<Article>(sql, new {id = userID});

        return result == null 
            ? new Response<List<Article>>(HttpStatusCode.NotFound, "Not found")
            : new Response<List<Article>>(result.ToList());
    }

    public async Task<Response<List<Comment>>> GetArticleRecentCommentsAsync(int articleID)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"
            SELECT * 
            FROM comments 
            WHERE articleId = @id
            ORDER BY createdAt DESC LIMIT 5
        ";
        var result = await connection.QueryAsync<Comment>(sql, new {id = articleID});

        return result == null 
            ? new Response<List<Comment>>(HttpStatusCode.NotFound, "Not found")
            : new Response<List<Comment>>(result.ToList());
    }

    public async Task<Response<int>> GetArticleClapsCountAsync(int articleID)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"
            SELECT COUNT(*)
            FROM claps
            WHERE articleId = @id
        ";
        var result = await connection.ExecuteScalarAsync<int>(sql, new {id = articleID});

        return new Response<int>(result);
    }

    public async Task<Response<List<UserNameAndAllArticle>>> GetRecentArticlesAsync()
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"
            SELECT u.username, a.*
            FROM articles a
            JOIN users u ON a.userId = u.id
            ORDER BY createdAt DESC LIMIT 10
        ";
        var result = await connection.QueryAsync<UserNameAndAllArticle>(sql);

        return result == null 
            ? new Response<List<UserNameAndAllArticle>>(HttpStatusCode.NotFound, "Not found")
            : new Response<List<UserNameAndAllArticle>>(result.ToList());
    }

    public async Task<Response<List<ArticleAndCommentCount>>> GetUserStatsAsync(int userID)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"
            SELECT 
                COUNT(a.id) AS ArticlesCount, 
                COUNT(c.id) AS CommentsCount
            FROM users u
            JOIN articles a ON a.userId = u.id
            JOIN comments c ON c.userId = u.id
            WHERE u.id = @id
        ";
        var result = await connection.QueryAsync<ArticleAndCommentCount>(sql, new {id = userID});

        return result == null 
            ? new Response<List<ArticleAndCommentCount>>(HttpStatusCode.NotFound, "Not found")
            : new Response<List<ArticleAndCommentCount>>(result.ToList());
    }

}