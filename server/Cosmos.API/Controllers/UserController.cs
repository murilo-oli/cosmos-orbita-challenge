using Cosmos.Application.DTOs;
using Cosmos.Application.Interfaces;
using Cosmos.Application.Utilities;
using Cosmos.Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cosmos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] Pagination? pagination, CancellationToken cancellationToken)
        {
            ResponseDTO response = await _userService.GetAll(pagination, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] CreateUserDTO user, CancellationToken cancellationToken)
        {
            ResponseDTO response = await _userService.Add(user, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] UpdateUserDTO user, long id, CancellationToken cancellationToken)
        {
            ResponseDTO response = await _userService.Update(id, user, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("{id}/{status}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Patch(long id, bool status, CancellationToken cancellationToken)
        {
            ResponseDTO response = await _userService.SetStatus(id, status, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            ResponseDTO response = await _userService.Delete(id, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
