using API.Helpers;

namespace API;

public class VisitsParams: PaginationParams
{
    public int UserId{get;set;}
    public string Predicate{get;set;}
    public string FilterVisits{get;set;} = "all";
}

