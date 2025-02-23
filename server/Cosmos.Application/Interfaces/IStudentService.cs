using Cosmos.Application.DTOs;
using Cosmos.Application.Utilities;
using Cosmos.Infrastructure.Utilities;

namespace Cosmos.Application.Interfaces;

public interface IStudentService
{
    Task<ResponseDTO> Add(CreateStudentDTO model, CancellationToken cancellationToken);
    Task<ResponseDTO> Update(long Id, UpdateStudentDTO model, CancellationToken cancellationToken);
    Task<ResponseDTO> SetStatus(long Id, bool status, CancellationToken cancellationToken);
    Task<ResponseDTO> Delete(long Id, CancellationToken cancellationToken);
    Task<ResponseDTO> GetFiltered(FilterDTO filter, CancellationToken cancellationToken);
    Task<ResponseDTO> GetAllFiltered(Pagination? pagination, FilterDTO filter, CancellationToken cancellationToken);
    Task<ResponseDTO> GetAll(Pagination? pagination, CancellationToken cancellationToken);
}
