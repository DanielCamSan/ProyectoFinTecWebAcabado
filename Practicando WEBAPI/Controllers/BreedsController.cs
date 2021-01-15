using Microsoft.AspNetCore.Mvc;
using Practicando_WEBAPI.Models;
using Practicando_WEBAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Practicando_WEBAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Practicando_WEBAPI.Controllers
{
    
    [Route("api/[controller]")]
    public class BreedsController : ControllerBase
    {
        private IBreedsService _breedService;
        public BreedsController(IBreedsService breedService)
        {
            _breedService = breedService;
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BreedModel>>> GetBreedsAsync()
        {
            try
            {
                return Ok(await _breedService.GetBreedsAsync());
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }

        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet("{breedId:int}", Name="GetBreed")]
        public async Task<ActionResult<BreedModel>> GetBreedAsync(int breedId)
        {
            try
            {
                return Ok(await _breedService.GetBreedAsync(breedId));
            }
            catch(NotFoundException ex)
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
        public async Task<ActionResult<BreedModel>> CreateBreedAsync([FromBody] BreedModel breedModel)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(breedModel);
                }
                var url = HttpContext.Request.Host;
                var createdBreed = await _breedService.CreateBreedAsync(breedModel);
                return CreatedAtRoute("GetBreed",new { breedId=createdBreed.Id},createdBreed);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Something happend: {ex.Message}");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{breedId:int}")]
       public async  Task<ActionResult<bool>> DeleteBreedAsync(int breedId)
        {
            try
            {
                return Ok(await _breedService.DeleteBreedAsync(breedId));
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
        [HttpPut("{breedId:int}")]
        public async Task<ActionResult<bool>> UpdateBreedAsync(int breedId, [FromBody] BreedModel breedModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    foreach (var pair in ModelState)
                    {
                        if (pair.Key == nameof(breedModel.TypesofUnity) && pair.Value.Errors.Count > 0)
                        {
                            return BadRequest(pair.Value.Errors);
                        }
                    }
                }
                return  await _breedService.UpdateBreedAsync(breedId, breedModel);
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
