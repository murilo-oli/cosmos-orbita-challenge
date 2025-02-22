using System;
using System.ComponentModel.DataAnnotations;
using Cosmos.Domain.Enums;

namespace Cosmos.Domain.Entities;

public class User : Base
{
    [StringLength(64)]
    public byte[] Password {get;set;} = default!;

    [StringLength(255)]
    public string AvatarPath {get;set;} = default!;
    public EUserRole Role {get;set;} = EUserRole.Member;
}
