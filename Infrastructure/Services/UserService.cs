using System.Net;
using Dapper;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;

namespace Infrastructure.Services;

public class UserService
{
    DataContextAsync _context = new();

    public async Task<Response<List<User>>> GetAllAsync()
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"SELECT * FROM users";
        var result = await connection.QueryAsync<User>(sql);

        return new Response<List<User>>(result.ToList());
    }

    public async Task<Response<User>> GetByIdAsync(int ID)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"SELECT * FROM users WHERE id = @Id";
        var result = await connection.QueryFirstOrDefaultAsync<User>(sql, new {Id = ID});

        return result == null
            ? new Response<User>(HttpStatusCode.NotFound, "Not found")
            : new Response<User>(result);
    }

    public async Task<Response<string>> CreateAsync(User user)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"
            INSERT INTO users(username, email, passwordHash, bio, createdAt)
            VALUES
            (@username, @email, @passwordHash, @bio, @createdAt)
        ";
        var result = await connection.ExecuteAsync(sql, user);

        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Something went wrong")
            : new Response<string>("User created successefully");
    }

    public async Task<Response<string>> UpdateAsync(User user)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"
            UPDATE users
            SET username=@username, email=@email, passwordHash=@passwordHash, bio=@bio, createdAt=@createdAt
            WHERE id = @Id
        ";
        var result = await connection.ExecuteAsync(sql, user);

        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Something went wrong")
            : new Response<string>("User updated successefully");
    }

    public async Task<Response<string>> DeleteAsync(int ID)
    {
        using var connection = await _context.GetConnectionAsync();
        var sql = @"
            DELETE FROM users
            WHERE id = @Id
        ";
        var result = await connection.ExecuteAsync(sql, new {Id = ID});

        return result == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Something went wrong")
            : new Response<string>("User deleted successefully");
    }
}