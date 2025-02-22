using System.ComponentModel.DataAnnotations;

namespace Cosmos.Domain.Entities;

public class Student : Base
{    
    [Required]
    [StringLength(11)]
    public string CPF {get;set;} = default!;
    
    [Required]
    public string RA {get;set;} = default!;
}
