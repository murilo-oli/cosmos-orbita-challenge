using System;
using System.ComponentModel.DataAnnotations;
using Cosmos.Application.DTOs;
using Cosmos.Application.Interfaces;
using Cosmos.Application.Utilities;
using Cosmos.Domain.Entities;
using Cosmos.Infrastructure.Interfaces;

namespace Cosmos.Application.Services;

public class AuthService : IAuthService
{
    private readonly IBaseRepository<User> _userRepository;

    public AuthService(IBaseRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResponseDTO> ValidateLogin(LoginDTO loginDTO)
    {
        //valida model
        ResponseDTO checkModel = CheckModel(loginDTO);

        if (!checkModel.Success) return checkModel;

        User? userFound = await _userRepository.GetByInclude(u => u.Email == loginDTO.Email);

        if(userFound == null) return ResponseManager.NotFound("Nenhum usuário encontrado.");

        bool validPassword = PasswordEncrypter.ValidateHash(loginDTO.Password, userFound.Password);

        if(!validPassword) ResponseManager.Forbidden("Usuário não autenticado.");

        userFound.Password = "";

        return ResponseManager.Success(userFound);
    }

    private ResponseDTO CheckModel(LoginDTO model)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model);

        if (String.IsNullOrEmpty(model.Email.Trim()) || String.IsNullOrEmpty(model.Password.Trim()))
        {
            return ResponseManager.InvalidModel("Campos vazios.");
        }

        if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
        {
            return ResponseManager.InvalidModel(validationResults.First().ErrorMessage);
        }

        if (!ValidateEmailCPF.ValidateEmail(model.Email)) return ResponseManager.InvalidModel($"O email {model.Email} é inválido!");

        return ResponseManager.Success();
    }
}
