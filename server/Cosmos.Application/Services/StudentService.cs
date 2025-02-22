using System.Linq.Expressions;
using Cosmos.Application.DTOs;
using Cosmos.Application.Interfaces;
using Cosmos.Application.Utilities;
using Cosmos.Domain.Entities;
using Cosmos.Infrastructure.Interfaces;
using Cosmos.Infrastructure.Utilities;

namespace Cosmos.Application.Services;

public class StudentService : IStudentService
{
    private readonly IBaseRepository<Student> _studentRepository;

    public StudentService(IBaseRepository<Student> studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<ResponseModel> Add(CreateStudentDTO student, CancellationToken cancellationToken)
    {
        ResponseModel checkModel = await CheckCreateModel(student, cancellationToken);

        if (!checkModel.Success) return checkModel;

        try
        {
            Student studentMounted = new Student()
            {
                Name = student.Name,
                Email = student.Email,
                CPF = student.CPF,
                RA = student.RA,
                IsActive = true
            };

            await _studentRepository.Add(studentMounted, cancellationToken);
        }
        catch (Exception ex)
        {
            return ResponseManager.InternalBadRequest(ex.Message);
        }

        return ResponseManager.Created(student, $"Estudante cadastrado com sucesso!");
    }

    public async Task<ResponseModel> Update(long Id, UpdateStudentDTO model, CancellationToken cancellationToken)
    {
        ResponseModel checkModel = await CheckUpdateModel(Id, cancellationToken);

        if (!checkModel.Success) return checkModel;

        try
        {
            Student studentToUpdate = (Student)checkModel.Data!;

            studentToUpdate.Name = model.Name;
            studentToUpdate.Email = model.Email;

            await _studentRepository.Update(studentToUpdate, cancellationToken);
        }
        catch (Exception ex)
        {
            ResponseManager.InternalBadRequest(ex.Message);
        }

        return ResponseManager.Success();
    }

    public async Task<ResponseModel> SetStatus(long Id, bool status, CancellationToken cancellationToken)
    {
        Student? studentFound = await _studentRepository.GetById(Id, cancellationToken);

        if (studentFound == null) return ResponseManager.NotFound("Estudante não encontrado!");

        try
        {
            studentFound.IsActive = status;

            await _studentRepository.Update(studentFound, cancellationToken);
        }
        catch (Exception ex)
        {
            ResponseManager.InternalBadRequest(ex.Message);
        }

        return ResponseManager.Success();
    }

    public async Task<ResponseModel> Delete(long Id, CancellationToken cancellationToken)
    {
        Student? studentFound = await _studentRepository.GetById(Id, cancellationToken);

        if (studentFound == null) return ResponseManager.NotFound("Estudante não encontrado!");

        try
        {
            await _studentRepository.Remove(studentFound, cancellationToken);
        }
        catch (Exception ex)
        {
            ResponseManager.InternalBadRequest(ex.Message);
        }

        return ResponseManager.Success();
    }

    public async Task<ResponseModel> GetFiltered(Filter filter, CancellationToken cancellationToken)
    {
        Func<IQueryable<Student>, IQueryable<Student>> query = BuildStudentQuery(filter);

        Student? studentFound = await _studentRepository.Get(query, cancellationToken);

        if(studentFound == null) return ResponseManager.NotFound("Nenhum estudante encontrado!");

        return ResponseManager.Success(studentFound);
    }

    public async Task<ResponseModel> GetAllFiltered(Pagination? pagination, Filter filter, CancellationToken cancellationToken)
    {
        Func<IQueryable<Student>, IQueryable<Student>> query = BuildStudentQuery(filter);

        IEnumerable<Student> studentFound = await _studentRepository.GetAll(query, pagination, cancellationToken);

        if(studentFound == null) return ResponseManager.NotFound("Nenhum estudante encontrado!");

        return ResponseManager.Success(studentFound);
    }

    private static Func<IQueryable<Student>, IQueryable<Student>> BuildStudentQuery(Filter filter)
    {
        return query =>
        {
            if (filter.IsActive.HasValue)
                query = query.Where(st => st.IsActive == filter.IsActive.Value);

            if (!string.IsNullOrWhiteSpace(filter.RA))
                query = query.Where(st => st.RA.Contains(filter.RA));

            if (!string.IsNullOrWhiteSpace(filter.CPF))
                query = query.Where(st => st.CPF.Contains(filter.CPF));

            if (!string.IsNullOrWhiteSpace(filter.Email))
                query = query.Where(st => st.Email.Contains(filter.Email));

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(st => st.Name.Contains(filter.Name));

            return query.OrderBy(st => st.RA); // Ordenação dinâmica
        };
    }

    private async Task<ResponseModel> CheckCreateModel(CreateStudentDTO model, CancellationToken cancellationToken)
    {
        if (String.IsNullOrEmpty(model.CPF) ||
            String.IsNullOrEmpty(model.RA) ||
            String.IsNullOrEmpty(model.Name) ||
            String.IsNullOrEmpty(model.Email))
        {
            return ResponseManager.InvalidModel("Campos vazios.");
        }

        if (!String.IsNullOrEmpty(model.RA))
        {
            bool studentFound = await _studentRepository.Exist(us => us.RA == model.RA, cancellationToken);

            if (!studentFound) return ResponseManager.Conflict($"Estudante com RA {model.RA} já cadastrado.");
        }

        return ResponseManager.Success();
    }

    private async Task<ResponseModel> CheckUpdateModel(long Id, CancellationToken cancellationToken)
    {
        Student? studentFound = await _studentRepository.GetById(Id, cancellationToken);

        if (studentFound == null) return ResponseManager.NotFound($"Estudante não encontrado.");

        return ResponseManager.Success(studentFound);
    }
}
