using System.ComponentModel.DataAnnotations;
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

    public async Task<ResponseDTO> Add(CreateStudentDTO student, CancellationToken cancellationToken)
    {
        ResponseDTO checkModel = await CheckCreateModel(student, cancellationToken);

        if (!checkModel.Success) return checkModel;

        Student studentMounted = new Student()
        {
            Name = student.Name,
            Email = student.Email,
            CPF = student.CPF.Trim().Replace(".", "").Replace("-", ""),
            RA = student.RA,
            IsActive = true
        };

        try
        {
            await _studentRepository.Add(studentMounted, cancellationToken);
        }
        catch (Exception ex)
        {
            return ResponseManager.InternalBadRequest(ex.Message);
        }

        return ResponseManager.Created(studentMounted, $"Estudante cadastrado com sucesso!");
    }

    public async Task<ResponseDTO> AddRange(List<CreateStudentDTO> students, CancellationToken cancellationToken)
    {
        foreach (CreateStudentDTO student in students)
        {
            ResponseDTO checkModel = await CheckCreateModel(student, cancellationToken);

            if (!checkModel.Success) return checkModel;
        }

        List<Student> studentsToSave = new List<Student>();

        try
        {

            foreach (CreateStudentDTO student in students)
            {
                Student studentMounted = new Student()
                {
                    Name = student.Name,
                    Email = student.Email,
                    CPF = student.CPF.Trim().Replace(".", "").Replace("-", ""),
                    RA = student.RA,
                    IsActive = true
                };

                studentsToSave.Add(studentMounted);
            }


            await _studentRepository.AddRange(studentsToSave, cancellationToken);
        }
        catch (Exception ex)
        {
            return ResponseManager.InternalBadRequest(ex.Message);
        }

        return ResponseManager.Created(studentsToSave, $"Lista de estudantes cadastrados com sucesso!");
    }

    public async Task<ResponseDTO> Update(long Id, UpdateStudentDTO model, CancellationToken cancellationToken)
    {
        ResponseDTO checkModel = await CheckUpdateModel(Id, model, cancellationToken);

        if (!checkModel.Success) return checkModel;

        Student studentToUpdate = (Student)checkModel.Data!;

        try
        {

            studentToUpdate.Name = model.Name;
            studentToUpdate.Email = model.Email;

            await _studentRepository.Update(studentToUpdate, cancellationToken);
        }
        catch (Exception ex)
        {
            ResponseManager.InternalBadRequest(ex.Message);
        }

        return ResponseManager.Success(studentToUpdate);
    }

    public async Task<ResponseDTO> SetStatus(long Id, bool status, CancellationToken cancellationToken)
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

    public async Task<ResponseDTO> Delete(long Id, CancellationToken cancellationToken)
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

    public async Task<ResponseDTO> GetFiltered(FilterDTO filter, CancellationToken cancellationToken)
    {
        Func<IQueryable<Student>, IQueryable<Student>> query = BuildStudentQuery(filter);

        Student? studentFound = await _studentRepository.GetFiltered(query, cancellationToken);

        if (studentFound == null) return ResponseManager.NotFound("Nenhum estudante encontrado!");

        return ResponseManager.Success(studentFound, "Estudante carregado com sucesso!");
    }

    public async Task<ResponseDTO> GetById(long Id, CancellationToken cancellationToken)
    {
        Student? studentFound = await _studentRepository.GetById(Id, cancellationToken);

        if (studentFound == null) return ResponseManager.NotFound("Nenhum estudante encontrado!");

        return ResponseManager.Success(studentFound, "Estudante carregado com sucesso!");
    }

    public async Task<ResponseDTO> GetAllFiltered(Pagination? pagination, FilterDTO filter, CancellationToken cancellationToken)
    {
        Func<IQueryable<Student>, IQueryable<Student>> query = BuildStudentQuery(filter);

        IEnumerable<Student> studentFound = await _studentRepository.GetAllFiltered(query, pagination, cancellationToken);

        if (!studentFound.Any()) return ResponseManager.NotFound("Nenhum estudante encontrado!");

        return ResponseManager.Success(studentFound, "Estudantes carregados com sucesso!");
    }

    public async Task<ResponseDTO> GetAll(Pagination? pagination, CancellationToken cancellationToken)
    {
        IEnumerable<Student> studentFound = await _studentRepository.GetAll(pagination, cancellationToken);

        if (!studentFound.Any()) return ResponseManager.NotFound("Nenhum estudante encontrado!");

        return ResponseManager.Success(studentFound, "Estudantes carregados com sucesso!");
    }

    private static Func<IQueryable<Student>, IQueryable<Student>> BuildStudentQuery(FilterDTO filter)
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

    private async Task<ResponseDTO> CheckCreateModel(CreateStudentDTO model, CancellationToken cancellationToken)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model);

        if (String.IsNullOrEmpty(model.CPF.Trim()) ||
            String.IsNullOrEmpty(model.RA.Trim()) ||
            String.IsNullOrEmpty(model.Name.Trim()) ||
            String.IsNullOrEmpty(model.Email.Trim()))
        {
            return ResponseManager.InvalidModel("Campos vazios.");
        }

        if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
        {
            return ResponseManager.InvalidModel(validationResults.First().ErrorMessage);
        }

        if (!ValidateEmailCPF.ValidateEmail(model.Email)) return ResponseManager.InvalidModel($"O email {model.Email} é inválido!");

        if (!ValidateEmailCPF.ValidateCPF(model.CPF)) return ResponseManager.InvalidModel($"O CPF {model.CPF} é inválido!");

        if (!String.IsNullOrEmpty(model.RA))
        {
            bool studentFound = await _studentRepository.Exist(us => us.RA == model.RA, cancellationToken);

            if (studentFound) return ResponseManager.Conflict($"Estudante com RA {model.RA} já cadastrado.");
        }

        return ResponseManager.Success();
    }

    private async Task<ResponseDTO> CheckUpdateModel(long Id, UpdateStudentDTO model, CancellationToken cancellationToken)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model);

        if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
        {
            return ResponseManager.InvalidModel(validationResults.First().ErrorMessage);
        }

        Student? studentFound = await _studentRepository.GetById(Id, cancellationToken);

        if (studentFound == null) return ResponseManager.NotFound($"Estudante não encontrado.");

        return ResponseManager.Success(studentFound);
    }
}
