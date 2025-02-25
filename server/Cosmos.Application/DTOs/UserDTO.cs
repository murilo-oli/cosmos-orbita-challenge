using System;
using System.ComponentModel.DataAnnotations;
using Cosmos.Domain.Enums;

namespace Cosmos.Application.DTOs;

public class CreateUserDTO
{
    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do estudante deve ter entre {2} e {1} caracteres.")]
    public string Name {get;set;} = default!;
    
    [Required]
    [StringLength(320, MinimumLength = 3, ErrorMessage = "O email do estudante deve ter entre {2} e {1} caracteres.")]
    [EmailAddress]
    public string Email {get;set;} = default!;
    
    [StringLength(64)]
    public string Password {get;set;} = default!;

    [StringLength(255)]
    public string AvatarPath {get;set;} = default!;
    public EUserRole Role {get;set;} = EUserRole.Member;
}

public class UpdateUserDTO
{
    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do estudante deve ter entre {2} e {1} caracteres.")]
    public string Name {get;set;} = default!;

    [StringLength(64)]
    public string Password {get;set;} = default!;

    [StringLength(255)]
    public string? AvatarPath {get;set;} = default!;
    public EUserRole Role {get;set;} = EUserRole.Member;
}
