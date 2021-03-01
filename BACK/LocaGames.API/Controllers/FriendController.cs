using LocaFriends.Domain.Response;
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
    public class FriendController : ControllerBase
    {
        private readonly IServiceFriend _serviceFriend;

        public FriendController(IServiceFriend serviceFriend)
        {
            _serviceFriend = serviceFriend;
        }

        [HttpGet("{page}/{size}")]
        public async Task<IActionResult> GetAllPagination(int page, int size)
        {
            try
            {
                var games = await _serviceFriend.GetAllPagination(page, size);
                return Ok(games);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetFriendsSelectList")]
        public async Task<IActionResult> GetFriendsSelectList()
        {
            try
            {
                var games = await _serviceFriend.GetFriendsSelectList();
                return Ok(games);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                FriendResponse game = await _serviceFriend.GetById(id);
                return Ok(game);
            }
            catch (Exception)
            {
                return BadRequest(new GameResponse(NotificacaoDto.ErroPadrao));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(FriendDto friendDto)
        {
            try
            {
                FriendResponse response = await _serviceFriend.Add(friendDto);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest(new GameResponse(NotificacaoDto.ErroPadrao));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(FriendDto friendDto)
        {
            try
            {
                FriendResponse response = await _serviceFriend.Update(friendDto);
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
                FriendResponse response = await _serviceFriend.Delete(id);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest(new GameResponse(NotificacaoDto.ErroPadrao));
            }
        }
    }
}
