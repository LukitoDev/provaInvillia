using LocaGames.Domain.Dtos;
using LocaGames.Domain.Entities;
using LocaGames.Domain.Interfaces.Services;
using LocaGames.Domain.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LocaGames.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IServiceGame _serviceGame;

        public GameController(IServiceGame serviceGame)
        {
            _serviceGame = serviceGame;
        }

        [HttpGet("{page}/{size}")]
        public async Task<IActionResult> GetAllPagination(int page, int size)
        {
            try
            {
                var games = await _serviceGame.GetAllPagination(page, size);
                return Ok(games);
            }
            catch (Exception)
            {
                return BadRequest(new GameResponse(NotificacaoDto.ErroPadrao));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                GameResponse game = await _serviceGame.GetById(id);
                return Ok(game);
            }
            catch (Exception)
            {
                return BadRequest(new GameResponse(NotificacaoDto.ErroPadrao));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(GameDto gameDto)
        {
            try
            {
                var response = await _serviceGame.Add(gameDto);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest(new GameResponse(NotificacaoDto.ErroPadrao));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(GameDto gameDto)
        {
            try
            {
                var response = await _serviceGame.Update(gameDto);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest(new GameResponse(NotificacaoDto.ErroPadrao));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _serviceGame.Delete(id);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest(new GameResponse(NotificacaoDto.ErroPadrao));
            }
        }
    }
}
