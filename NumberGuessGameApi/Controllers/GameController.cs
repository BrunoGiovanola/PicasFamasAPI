using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NumberGuessGameApi.Data;
using NumberGuessGameApi.DataTransferObjects;
using NumberGuessGameApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace NumberGuessGameApi.Controllers;

[ApiController]
[Route("api/game/v1")]
public class GameController : ControllerBase
{
    private readonly GameDbContext _context;

    public GameController(GameDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterPlayerRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName) ||
    string.IsNullOrWhiteSpace(request.LastName) ||
    request.Age <= 0 ||
    string.IsNullOrWhiteSpace(request.Username) ||
    string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("FirstName, LastName, Age, Username y Password son obligatorios.");
        }

        var exists = await _context.Players.AnyAsync(p =>
            p.FirstName == request.FirstName &&
            p.LastName == request.LastName &&
            p.Age == request.Age);

        if (exists)
        {
            return BadRequest("El jugador ya existe.");
        }

        var player = new Player
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Age = request.Age,
            Username = request.Username,
            PasswordHash = request.Password
        };

        _context.Players.Add(player);
        await _context.SaveChangesAsync();

        return Ok(new RegisterPlayerResponse
        {
            PlayerId = player.PlayerId,
            Message = "Jugador registrado correctamente."
        });
    }

    [Authorize]
    [HttpPost("start")]
    public async Task<IActionResult> Start(StartGameRequest request)
    {
        var playerIdClaim = User.FindFirst("playerId")?.Value;

        if (string.IsNullOrWhiteSpace(playerIdClaim))
        {
            return Unauthorized("Token inválido: no contiene playerId.");
        }

        var playerId = int.Parse(playerIdClaim);

        var player = await _context.Players.FindAsync(playerId);

        if (player == null)
        {
            return NotFound("Jugador no encontrado.");
        }

        var activeGame = await _context.Games.AnyAsync(g =>
            g.PlayerId == playerId &&
            !g.IsFinished);

        if (activeGame)
        {
            return BadRequest("El jugador ya tiene un juego activo.");
        }

        var game = new Game
        {
            PlayerId = playerId,
            SecretNumber = GenerateSecretNumber(),
            IsFinished = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        return Ok(new StartGameResponse
        {
            GameId = game.GameId,
            Message = "Juego iniciado correctamente."
        });
    }

    [Authorize]
    [HttpPost("guess")]
    public async Task<IActionResult> Guess(GuessRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Number) ||
            request.Number.Length != 4 ||
            !request.Number.All(char.IsDigit) ||
            request.Number.Distinct().Count() != 4)
        {
            return BadRequest("El número debe tener 4 dígitos sin repetir.");
        }

        var game = await _context.Games.FindAsync(request.GameId);

        if (game == null)
        {
            return NotFound("Juego no encontrado.");
        }

        if (game.IsFinished)
        {
            return BadRequest("El juego ya está finalizado.");
        }

        return Ok("Intento recibido correctamente. La lógica de picas y famas se completa en el Paso 4.");
    }

    private static string GenerateSecretNumber()
    {
        var random = new Random();
        var digits = "0123456789".ToList();
        var result = "";

        for (int i = 0; i < 4; i++)
        {
            var index = random.Next(digits.Count);
            result += digits[index];
            digits.RemoveAt(index);
        }

        return result;
    }
}