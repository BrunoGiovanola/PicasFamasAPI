using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NumberGuessGameApi.Data;
using NumberGuessGameApi.DataTransferObjects;
using NumberGuessGameApi.Services;

namespace NumberGuessGameApi.Controllers;

[ApiController]
[Route("api/game/v1")]
public class AuthController : ControllerBase
{
    private readonly GameDbContext _context;
    private readonly TokenService _tokenService;

    public AuthController(GameDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Username y Password son obligatorios.");
        }

        var player = await _context.Players.FirstOrDefaultAsync(p =>
            p.Username == request.Username &&
            p.PasswordHash == request.Password);

        if (player == null)
        {
            return Unauthorized("Credenciales inválidas.");
        }

        var token = _tokenService.GenerateToken(player);

        return Ok(new LoginResponse
        {
            Token = token,
            Message = "Login correcto."
        }); ;
    }
}