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
    public async Task<IActionResult> GetScore(int id)
    {
        var player = await _playerService.GetAsync(id);
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
    public async Task<IActionResult> CreatePlayer([FromBody] Player player)
    {
        var playerDb = await _playerService.GetAsync(player.Id);
        if (playerDb != null)
            return BadRequest("Jugador ya existe.");

        await _playerService.CreateAsync(player);

        return Ok(player);
    }

}