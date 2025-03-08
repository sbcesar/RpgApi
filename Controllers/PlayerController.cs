using Microsoft.AspNetCore.Mvc;
using RpgApi.Models;
using RpgApi.Services;

namespace RpgApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly PlayerService _playerService;

    public PlayerController(PlayerService context)
    {
        _playerService = context;
    }

    // Obtener el score de un jugador
    [HttpGet("{id}")]
    public IActionResult GetScore(int id)
    {
        var player = _playerService.GetAsync(id).Result;
        if (player == null)
            return NotFound("Jugador no encontrado.");

        return Ok(player);
    }

    // Actualizar el score de un jugador
    [HttpPut("{id}")]
    public IActionResult UpdateScore(int id, [FromBody] int newScore)
    {
        var player = _playerService.GetAsync(id).Result;
        if (player == null)
            return NotFound("Jugador no encontrado.");

        player.Score = newScore;
        _playerService.UpdateAsync(id, player).Wait();

        return Ok($"Nuevo score: {player.Score}");
    }

    // Crear un nuevo jugador
    [HttpPost]
    public IActionResult CreatePlayer([FromBody] Player player)
    {
        var playerDb = _playerService.GetAsync(player.Id).Result;
        if (playerDb != null)
            return BadRequest("Jugador ya existe.");

        _playerService.CreateAsync(player).Wait();

        return Ok(player);
    }

}