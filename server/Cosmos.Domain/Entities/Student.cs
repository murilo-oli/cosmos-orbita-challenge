using System.ComponentModel.DataAnnotations;

namespace Cosmos.Domain.Entities;

public class Student : Base
{
    [Required]
    [StringLength(100)]
    public string Name {get;set;} = default!;
    
    [Required]
    [StringLength(320)]
    public string Email {get;set;} = default!;
    
    [Required]
    [StringLength(11)]
    public string CPF {get;set;} = default!;
    
    [Required]
    public string RA {get;set;} = default!;
}
