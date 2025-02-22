using System;

namespace Cosmos.Infrastructure.Utilities;

public class Pagination
{
    public int PageSize {get;set;} = 25;
    public int CurrentPage {get;set;} = 0;
}
