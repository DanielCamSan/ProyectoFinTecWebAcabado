using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practicando_WEBAPI.Exceptions;
using Practicando_WEBAPI.Models;
using Practicando_WEBAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Practicando_WEBAPI.Controllers
{
    [Route("api/Breeds/{breedId:int}/{controller}")]
    public class HeroesController:ControllerBase
    {
        private IHeroService _heroService;

        public HeroesController(IHeroService heroService)
        {
            _heroService = heroService;
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeroModel>>> GetHeroesAsync(int breedId)
        {
            try
            {
                return Ok(await _heroService.GetHeroesAsync(breedId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("{heroId:int}", Name = "GetHero")]
        public async Task<ActionResult<HeroModel>> GetHeroAsync(int breedId, int heroId)
        {
            try
            {
                return Ok(await _heroService.GetHeroAsync(breedId, heroId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<HeroModel>> CreateHeroAsync(int breedId, [FromBody] HeroModel hero)
        {
            try
            {
                await _heroService.CreateHeroAsync(breedId, hero);
                return CreatedAtRoute("GetHero", new { breedId = breedId, heroId = hero.Id }, hero);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{heroId:int}")]
        public async Task<ActionResult<bool>> UpdateHeroAsync(int breedId, int heroId, [FromBody] HeroModel hero)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    foreach (var pair in ModelState)
                    {
                        if (pair.Key == nameof(HeroModel.Level) && pair.Value.Errors.Count > 0)
                        {
                            return BadRequest(pair.Value.Errors);
                        }
                    }
                }
                return await _heroService.UpdateHeroAsync(breedId, heroId, hero);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{heroId:int}")]
        public async Task<ActionResult<bool>> DeleteHeroAsync(int breedId, int heroId)
        {
            try
            {
                return Ok(await _heroService.DeleteHeroAsync(breedId, heroId));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }

    }
}
