using Cosmos.Application.DTOs;
using Cosmos.Application.Interfaces;
using Cosmos.Application.Utilities;
using Cosmos.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cosmos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            ResponseDTO response = await _authService.ValidateLogin(loginDTO);

            if (!response.Success) return StatusCode(response.StatusCode, response);

            var user = response.Data as User;

            if (user == null) return StatusCode(500, ResponseManager.InternalBadRequest(null, "Erro ao gerar token."));

            string generatedToken = _tokenService.GenerateJWT(response.Data);

            var loginResponse = new
            {
                Token = generatedToken,
                User = new
                {
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Role,
                    user.AvatarPath,
                }
            };

            return StatusCode(response.StatusCode, ResponseManager.Success(loginResponse, "Login bem-sucedido!"));
        }
    }
}
