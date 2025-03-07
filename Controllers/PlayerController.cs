using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;

namespace RpgApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly PlayerContext _context;

    public PlayerController(PlayerContext context)
    {
        _context = context;
    }

    // Obtener el score de un jugador
    [HttpGet("{id}")]
    public IActionResult GetScore(int id)
    {
        var player = _context.Players.FirstOrDefault(p => p.Id == id);
        if (player == null)
            return NotFound("Jugador no encontrado.");

        return Ok(player.Score);
    }

    // Actualizar el score de un jugador
    [HttpPut("{id}")]
    public IActionResult UpdateScore(int id, [FromBody] int newScore)
    {
        var player = _context.Players.FirstOrDefault(p => p.Id == id);
        if (player == null)
            return NotFound("Jugador no encontrado.");

        player.Score = newScore;
        _context.SaveChanges();

        return Ok($"Nuevo score: {player.Score}");
    }
}