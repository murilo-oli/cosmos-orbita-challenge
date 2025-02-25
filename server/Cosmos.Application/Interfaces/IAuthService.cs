using System;
using Cosmos.Application.DTOs;
using Cosmos.Application.Utilities;

namespace Cosmos.Application.Interfaces;

public interface IAuthService
{
    Task<ResponseDTO> ValidateLogin(LoginDTO loginDTO);
}
