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
    public class BorrowedGameController : ControllerBase
    {
        private readonly IServiceBorrowedGame _serviceBorrowedGame;

        public BorrowedGameController(IServiceBorrowedGame serviceBorrowedGame)
        {
            _serviceBorrowedGame = serviceBorrowedGame;
        }

        [HttpPost("BorrowGame")]
        public async Task<IActionResult> BorrowGame(BorrowDto borrowDto)
        {
            try
            {
                var response = await _serviceBorrowedGame.BorrowGame(borrowDto);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest(new GameResponse(NotificacaoDto.ErroPadrao));
            }
        }

        [HttpPost("ReturnGame")]
        public async Task<IActionResult> ReturnGame(BorrowDto borrowDto)
        {
            try
            {
                var response = await _serviceBorrowedGame.ReturnGame(borrowDto);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest(new GameResponse(NotificacaoDto.ErroPadrao));
            }
        }
    }
}
