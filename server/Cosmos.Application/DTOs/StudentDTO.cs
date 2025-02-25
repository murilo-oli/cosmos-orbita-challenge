using System.ComponentModel.DataAnnotations;

namespace Cosmos.Application.DTOs;

public class CreateStudentDTO
{
    [Required]
    [StringLength(14, ErrorMessage = "O CPF do estudante precisa ter {1} caracteres.")]
    public string CPF {get;set;} = default!;
    
    [Required]
    public string RA {get;set;} = default!;

    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do estudante deve ter entre {2} e {1} caracteres.")]
    public string Name {get;set;} = default!;
    
    [Required]
    [StringLength(320, MinimumLength = 3, ErrorMessage = "O email do estudante deve ter entre {2} e {1} caracteres.")]
    [EmailAddress]
    public string Email {get;set;} = default!;
}

public class UpdateStudentDTO
{
    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do estudante deve ter entre {2} e {1} caracteres.")]
    public string Name {get;set;} = default!;
    
    [Required]
    [StringLength(320, MinimumLength = 3, ErrorMessage = "O email do estudante deve ter entre {2} e {1} caracteres.")]
    [EmailAddress]
    public string Email {get;set;} = default!;
}
