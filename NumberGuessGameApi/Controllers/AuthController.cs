using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NumberGuessGameApi.Data;
using NumberGuessGameApi.DataTransferObjects;

namespace NumberGuessGameApi.Controllers;

[ApiController]
[Route("api/game/v1")]
public class AuthController : ControllerBase
{
    private readonly GameDbContext _context;

    public AuthController(GameDbContext context)
    {
        _context = context;
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

        return Ok(new LoginResponse
        {
            PlayerId = player.PlayerId,
            Message = "Login correcto."
        });
    }
}