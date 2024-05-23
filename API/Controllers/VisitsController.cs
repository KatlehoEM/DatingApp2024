using API.Controllers;
using API.DTOs;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;
public class VisitsController: BaseApiController
{
    private readonly IUnitOfWork _uow;
    public VisitsController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpPost("{username}")]
    public async Task<ActionResult> AddVisit(string username)
        {
            var visitorId = User.GetUserId();

            var visited = await _uow.UserRepository.GetUserByUsernameAsync(username);
            var visitor = await _uow.VisitsRepository.GetUserWithVisits(visitorId);

            if (visited == null) return NotFound();

            if (visitor.UserName == username) return BadRequest("You cannot visit yourself");

            var userVisit = await _uow.VisitsRepository.GetUserVisit(visitorId, visited.Id);

            if (userVisit != null){
                userVisit.VisitedDate = DateTime.UtcNow;
            }
            else{
                userVisit = new UserVisit
            {
                VisitorId = visitorId,
                Visitor = visitor,
                VisitedId = visited.Id,
                Visited = visited,
                VisitedDate = DateTime.UtcNow
            };
            }
            visitor.VisitedUsers.Add(userVisit);

            if (await _uow.Complete()) return Ok();

            return BadRequest("Failed to add visit");
        }

        [Authorize(Policy = "RequireVIPRole")]
        [HttpGet]
        public async Task<ActionResult<PagedList<VisitDto>>> GetUserVisits([FromQuery] VisitsParams visitsParams)
        {
            visitsParams.UserId = User.GetUserId();
            var users =  await _uow.VisitsRepository.GetUserVisits(visitsParams);

            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage,users.PageSize, users.TotalCount,
            users.TotalPages));
            return Ok(users);
        }


}
