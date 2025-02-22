using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmos.Domain.Entities;

public abstract class Base
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id {get;set;} = 0;
    public bool IsActive {get;set;} = true;
}
