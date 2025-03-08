using System.Threading.Tasks;
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
    public async Task<IActionResult> UpdateScore(int id, [FromBody] Player player)
    {
        var playerDb = _playerService.GetAsync(id).Result;
        if (playerDb == null)
            return NotFound("Jugador no encontrado.");

        
        await _playerService.UpdateAsync(id, player);

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