using System;
using Cosmos.Domain.Entities;

namespace Cosmos.Application.Interfaces;

public interface ITokenService
{
    string GenerateJWT(User user);
}
