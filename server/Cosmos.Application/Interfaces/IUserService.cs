using Cosmos.Application.DTOs;
using Cosmos.Application.Utilities;
using Cosmos.Domain.Enums;
using Cosmos.Infrastructure.Utilities;

namespace Cosmos.Application.Interfaces;

public interface IUserService
{
    Task<ResponseDTO> Add(CreateUserDTO model, CancellationToken cancellationToken);
    Task<ResponseDTO> Update(long Id, UpdateUserDTO model, CancellationToken cancellationToken);
    Task<ResponseDTO> SetStatus(long Id, bool status, CancellationToken cancellationToken);
    Task<ResponseDTO> GetAll(Pagination? pagination, CancellationToken cancellationToken);
    Task<ResponseDTO> GetById(long Id, CancellationToken cancellationToken);
    Task<ResponseDTO> Delete(long Id, CancellationToken cancellationToken);
}
