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
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetFiltered([FromQuery] FilterDTO filter, CancellationToken cancellationToken)
        {
            ResponseDTO response = await _studentService.GetFiltered(filter, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("all")]
        [Authorize]
        public async Task<IActionResult> GetAllFiltered([FromQuery] Pagination? pagination, [FromQuery] FilterDTO filter, CancellationToken cancellationToken)
        {
            ResponseDTO response = await _studentService.GetAllFiltered(pagination, filter, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Authorize(Policy = "Manager")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Post([FromBody] CreateStudentDTO student, CancellationToken cancellationToken)
        {
            ResponseDTO response = await _studentService.Add(student, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Manager")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Put([FromBody] UpdateStudentDTO student, long id, CancellationToken cancellationToken)
        {
            ResponseDTO response = await _studentService.Update(id, student, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPatch("{id}/{status}")]
        [Authorize(Policy = "Manager")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Patch(long id, bool status, CancellationToken cancellationToken)
        {
            ResponseDTO response = await _studentService.SetStatus(id, status, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            ResponseDTO response = await _studentService.Delete(id, cancellationToken);

            return StatusCode(response.StatusCode, response);
        }
    }
}
