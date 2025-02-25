using System;
using System.ComponentModel.DataAnnotations;

namespace Cosmos.Application.DTOs;

public class LoginDTO
{
    [Required]
    [StringLength(320, MinimumLength = 3, ErrorMessage = "O email do estudante deve ter entre {2} e {1} caracteres.")]
    [EmailAddress]
    public string Email {get;set;} = default!;

    [Required]
    public string Password { get; set; } = default!;
}
