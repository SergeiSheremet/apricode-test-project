using GameArchive.Api.Models.Dto;
using GameArchive.Api.Models.Validation;
using GameArchive.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameArchive.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {
        private readonly IGameServise _service;

        public GameController(IGameServise service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(await _service.ReadAllAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Json(await _service.ReadByIdAsync(id));
        }

        [HttpGet("{genre}")]
        public async Task<IActionResult> Get(string genre)
        {
            return Json(await _service.ReadByGenreAsync(genre));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GameDto game)
        {
            GameCreateValidator validator = new();
            var validationResult = validator.Validate(game);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            await _service.CreateAsync(game);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] GameDto game)
        {
            GameUpdateValidator validator = new();
            var validationResult = validator.Validate(game);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            await _service.UpdateAsync(game);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
