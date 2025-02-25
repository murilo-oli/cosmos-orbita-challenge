using System.ComponentModel.DataAnnotations;
using Cosmos.Application.DTOs;
using Cosmos.Application.Interfaces;
using Cosmos.Application.Utilities;
using Cosmos.Domain.Entities;
using Cosmos.Domain.Enums;
using Cosmos.Infrastructure.Interfaces;
using Cosmos.Infrastructure.Utilities;

namespace Cosmos.Application.Services;

public class UserService : IUserService
{
    private readonly IBaseRepository<User> _userRepository;

    public UserService(IBaseRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResponseDTO> Add(CreateUserDTO model, CancellationToken cancellationToken)
    {
        ResponseDTO checkModel = await CheckCreateModel(model, cancellationToken);

        if (!checkModel.Success) return checkModel;

        string hashPass = PasswordEncrypter.GenerateHash(model.Password);

        User userMounted = new User()
        {
            Name = model.Name,
            Email = model.Email,
            IsActive = true,
            Role = EUserRole.Member,
            AvatarPath = model.AvatarPath,
            Password = hashPass
        };

        try
        {
            await _userRepository.Add(userMounted, cancellationToken);
        }
        catch (Exception ex)
        {
            return ResponseManager.InternalBadRequest(ex.Message);
        }

        return ResponseManager.Created(userMounted, $"Usuário cadastrado com sucesso!");
    }

    public async Task<ResponseDTO> GetAll(Pagination? pagination, CancellationToken cancellationToken)
    {
        IEnumerable<User> userFound = await _userRepository.GetAll(pagination, cancellationToken);

        if (!userFound.Any()) return ResponseManager.NotFound("Nenhum usuário encontrado!");

        return ResponseManager.Success(userFound);
    }

    public async Task<ResponseDTO> GetById(long Id, CancellationToken cancellationToken)
    {
        User? userFound = await _userRepository.GetById(Id, cancellationToken);

        if (userFound == null) return ResponseManager.NotFound("Nenhum usuário encontrado!");

        return ResponseManager.Success(userFound);
    }

    public async Task<ResponseDTO> SetStatus(long Id, bool status, CancellationToken cancellationToken)
    {
        User? userFound = await _userRepository.GetById(Id, cancellationToken);

        if (userFound == null) return ResponseManager.NotFound("Usuário não encontrado!");

        try
        {
            userFound.IsActive = status;

            await _userRepository.Update(userFound, cancellationToken);
        }
        catch (Exception ex)
        {
            ResponseManager.InternalBadRequest(ex.Message);
        }

        return ResponseManager.Success();
    }

    public async Task<ResponseDTO> Update(long Id, UpdateUserDTO model, CancellationToken cancellationToken)
    {
        ResponseDTO checkModel = await CheckUpdateModel(Id, model, cancellationToken);

        if (!checkModel.Success) return checkModel;

        User userToUpdate = (User)checkModel.Data!;

        try
        {
            userToUpdate.Name = model.Name;
            userToUpdate.AvatarPath = model.AvatarPath!;
            userToUpdate.Role = model.Role;

            if(!String.IsNullOrEmpty(model.Password)) userToUpdate.Password = PasswordEncrypter.GenerateHash(model.Password);

            await _userRepository.Update(userToUpdate, cancellationToken);
        }
        catch (Exception ex)
        {
            ResponseManager.InternalBadRequest(ex.Message);
        }

        return ResponseManager.Success(userToUpdate);
    }

    public async Task<ResponseDTO> Delete(long Id, CancellationToken cancellationToken)
    {
        User? userFound = await _userRepository.GetById(Id, cancellationToken);

        if (userFound == null) return ResponseManager.NotFound("Usuário não encontrado!");

        try
        {
            await _userRepository.Remove(userFound, cancellationToken);
        }
        catch (Exception ex)
        {
            ResponseManager.InternalBadRequest(ex.Message);
        }

        return ResponseManager.Success();
    }

    private async Task<ResponseDTO> CheckCreateModel(CreateUserDTO model, CancellationToken cancellationToken)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model);

        if (String.IsNullOrEmpty(model.Name.Trim()) ||
            String.IsNullOrEmpty(model.Email.Trim()) ||
            String.IsNullOrEmpty(model.Password.Trim()))
        {
            return ResponseManager.InvalidModel("Campos vazios.");
        }

        if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
        {
            return ResponseManager.InvalidModel(validationResults.First().ErrorMessage);
        }

        if (!ValidateEmailCPF.ValidateEmail(model.Email)) return ResponseManager.InvalidModel($"O email {model.Email} é inválido!");


        if (!String.IsNullOrEmpty(model.Email))
        {
            bool userFound = await _userRepository.Exist(us => us.Email == model.Email, cancellationToken);

            if (userFound) return ResponseManager.Conflict($"Usuário com email {model.Email} já cadastrado.");
        }

        return ResponseManager.Success();
    }

    private async Task<ResponseDTO> CheckUpdateModel(long Id, UpdateUserDTO model, CancellationToken cancellationToken)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model);

        if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
        {
            return ResponseManager.InvalidModel(validationResults.First().ErrorMessage);
        }

        User? userFound = await _userRepository.GetById(Id, cancellationToken);

        if (userFound == null) return ResponseManager.NotFound($"Usuário não encontrado.");

        return ResponseManager.Success();
    }
}
