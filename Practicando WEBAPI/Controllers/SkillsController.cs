using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Practicando_WEBAPI.Exceptions;
using Practicando_WEBAPI.Models;
using Practicando_WEBAPI.Services;

namespace Practicando_WEBAPI.Controllers
{
    [Route("api/Breeds/{breedId:int}/Heroes/{heroId:int}/{controller}")]
    public class SkillsController : ControllerBase
    {
        private ISkillService _skillService;
        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillModel>>> GetSkillsAsync(int heroId)
        {
            try
            {
                return Ok(await _skillService.GetSkillsAsync(heroId));
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
        [HttpGet("{skillId:int}", Name = "GetSkill")]
        public async Task<ActionResult<SkillModel>> GetSkillAsync(int heroId, int skillId)
        {
            try
            {
                return Ok(await _skillService.GetSkillAsync(heroId, skillId));
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
        public async Task<ActionResult<SkillModel>> CreateSkillAsync(int breedId, int heroId, [FromBody] SkillModel skill)
        {
            try
            {
                await _skillService.CreateSkillAsync(heroId, skill);
                return CreatedAtRoute("GetSkill", new { breedId = breedId, heroId = heroId, skillId = skill.Id }, skill);
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
        [HttpDelete("{skillId:int}")]
        public async Task<ActionResult<bool>> DeleteSkillAsync(int heroId, int skillId)
        {
            try
            {
                return Ok(await _skillService.DeleteSkillAsync(heroId, skillId));
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
