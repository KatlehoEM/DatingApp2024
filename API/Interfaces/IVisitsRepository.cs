using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API;

public interface IVisitsRepository
{
    Task<UserVisit> GetUserVisit(int visitorId, int visitedId);
    Task<AppUser> GetUserWithVisits(int userId);

    Task<PagedList<VisitDto>> GetUserVisits(VisitsParams visitsParams);

}
