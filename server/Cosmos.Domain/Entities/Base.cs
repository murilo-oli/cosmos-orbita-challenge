using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmos.Domain.Entities;

public abstract class Base
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id {get;set;} = 0;
    public bool IsActive {get;set;} = true;
    
    [Required]
    [StringLength(100)]
    public string Name {get;set;} = default!;
    
    [Required]
    [StringLength(320)]
    [EmailAddress]
    public string Email {get;set;} = default!;
}
