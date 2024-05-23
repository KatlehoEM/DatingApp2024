using System.Data;
using API.Entities;

namespace API;

public class UserVisit
{
    public int VisitorId {get;set;}
    public AppUser Visitor {get;set;}
    public int VisitedId {get;set;}
    public AppUser Visited{get;set;}
     public DateTime VisitedDate {get;set;} = DateTime.UtcNow;
}
