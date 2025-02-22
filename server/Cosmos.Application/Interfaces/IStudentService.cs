using Cosmos.Application.DTOs;
using Cosmos.Application.Utilities;
using Cosmos.Infrastructure.Utilities;

namespace Cosmos.Application.Interfaces;

public interface IStudentService
{
    Task<ResponseModel> Add(CreateStudentDTO model, CancellationToken cancellationToken);
    Task<ResponseModel> Update(long Id, UpdateStudentDTO model, CancellationToken cancellationToken);
    Task<ResponseModel> SetStatus(long Id, bool status, CancellationToken cancellationToken);
    Task<ResponseModel> Delete(long Id, CancellationToken cancellationToken);
    Task<ResponseModel> GetFiltered(Filter filter, CancellationToken cancellationToken);
    Task<ResponseModel> GetAllFiltered(Pagination? pagination, Filter filter, CancellationToken cancellationToken);
}
