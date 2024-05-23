using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using Microsoft.EntityFrameworkCore;

namespace API;

public class VisitsRepository : IVisitsRepository
{
    private readonly DataContext _context;
    public VisitsRepository(DataContext context){
        _context = context;
    }
    public async Task<UserVisit> GetUserVisit(int visitorId, int visitedId)
    {
        return await _context.Visits.FindAsync(visitorId,visitedId);
    }

    public async Task<PagedList<VisitDto>> GetUserVisits(VisitsParams visitsParams)
    {
        var users = _context.Users
                    .OrderBy(u => u.UserName)
                    .AsQueryable();
        var visits = _context.Visits.AsQueryable();

        if (visitsParams.FilterVisits == "pastMonth")
        {
            var pastMonth = DateTime.Now.AddMonths(-1); 
            visits = visits.Where(v => v.VisitedDate >= pastMonth);
        }

        if(visitsParams.Predicate == "visited"){
            visits = visits.Where(v => v.VisitorId == visitsParams.UserId);
            users = visits.Select(v => v.Visited);
        }

        if(visitsParams.Predicate == "visitedBy"){
            visits = visits.Where(v => v.VisitedId == visitsParams.UserId);
            users = visits.Select(v => v.Visitor);
        }

    

        var visitList = users.Select(user => new VisitDto
        {
            Id = user.Id,
            UserName = user.UserName,
            KnownAs = user.KnownAs,
            Age = user.DateOfBirth.CalculateAge(),
            PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
            City = user.City,
            VisitedDate = visitsParams.Predicate == "visited"
                ? visits.Where(v => v.VisitedId == user.Id && v.VisitorId == visitsParams.UserId).Select(v => v.VisitedDate).FirstOrDefault()
                : visits.Where(v => v.VisitorId == user.Id && v.VisitedId == visitsParams.UserId).Select(v => v.VisitedDate).FirstOrDefault()
        });

        return await PagedList<VisitDto>.CreateAsync(visitList,visitsParams.PageNumber,visitsParams.PageSize);

    }

    public async Task<AppUser> GetUserWithVisits(int userId)
    {
        return await _context.Users
            .Include(x => x.VisitedUsers)
            .FirstOrDefaultAsync(x => x.Id == userId); 
    }
}
