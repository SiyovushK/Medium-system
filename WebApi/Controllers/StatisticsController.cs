using Domain.Entities;
using Domain.Responses;
using Domain.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController
{
    StatisticService statisticService = new();

    [HttpGet("UserArticles")]
    public async Task<Response<List<Article>>> GetUserArticlesAsync(int userID)
    {
        return await statisticService.GetUserArticlesAsync(userID);
    }

    [HttpGet("RecentCommentsOnArticles")]
    public async Task<Response<List<Comment>>> GetArticleRecentCommentsAsync(int articleID)
    {
        return await statisticService.GetArticleRecentCommentsAsync(articleID);
    }

    [HttpGet("ClapsOnArticle")]
    public async Task<Response<int>> GetArticleClapsCountAsync(int articleID)
    {
        return await statisticService.GetArticleClapsCountAsync(articleID);
    }

    [HttpGet("RecentArticles")]
    public async Task<Response<List<UserNameAndAllArticle>>> GetRecentArticlesAsync()
    {
        return await statisticService.GetRecentArticlesAsync();
    }

    [HttpGet("UserStats")]
    public async Task<Response<List<ArticleAndCommentCount>>> GetUserStatsAsync(int userID)
    {
        return await statisticService.GetUserStatsAsync(userID);
    }
}