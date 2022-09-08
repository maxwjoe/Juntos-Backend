using System.Security.Claims;
using Juntos.Interfaces;
using Juntos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juntos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly IEventRepository _eventObjRepository;
        private readonly IAuthService _authService;
        public EventController(IEventRepository eventObjRepository, IAuthService authService)
        {
            _eventObjRepository = eventObjRepository;
            _authService = authService;
        }

        //TODO: Make this find relative to a club not a user
        // GetAllEvents : Gets all the eventObjs in the database belonging to user
        [HttpGet]
        [Route("{clubId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Event>>> GetAllEvents([FromRoute] int clubId)
        {
            var eventObjsDb = await _eventObjRepository.GetAll(clubId);

            if (eventObjsDb == null)
            {
                return BadRequest("Could not get eventObjs");
            }

            return Ok(eventObjsDb);
        }


        // CreateNewEvent : Creates a new eventObj in the database
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Event>> CreateNewEvent(EventDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid Event");
            }

            User reqUser = await _authService.GetUserObjFromToken();

            if (reqUser == null)
            {
                return Unauthorized("You do not have the correct credentials");
            }

            Event newEvent = new Event
            {
                OwnerId = reqUser.Id,
                Title = request.Title,
                Description = request.Description,
                EventImageUrl = request.EventImageUrl,
                ClubId = request.ClubId,
                CapacityLimit = request.CapacityLimit,
                BookingTimeLimit = request.BookingTimeLimit,
                RepeatOption = request.RepeatOption,
                Location = request.Location,
                AllowedMemberships = request.AllowedMemberships,
                EventDateAndTime = request.EventDateAndTime
            };

            Event createdEvent = await _eventObjRepository.Create(newEvent);

            return Ok(createdEvent);
        }


        // UpdateExistingEvent : Updates an existing eventObj in Db
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("{eventObjId}")]
        public async Task<ActionResult<Event>> UpdatedExistingEvent([FromBody] EventDto updates, [FromRoute] int eventObjId)
        {
            Event existingEvent = await _eventObjRepository.GetByIdAsync(eventObjId);

            if (existingEvent == null || updates == null)
            {
                return BadRequest("Invalid Params");
            }

            Event updatedEvent = await _eventObjRepository.Update(existingEvent, updates);

            return Ok(updatedEvent);
        }


        // DeleteExistingEvent : Deletes an existing eventObj from Db
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{eventObjId}")]
        public async Task<ActionResult<Event>> DeleteExistingEvent([FromRoute] int eventObjId)
        {
            Event eventObjToDelete = await _eventObjRepository.GetByIdAsync(eventObjId);

            if (eventObjToDelete == null)
            {
                return BadRequest("Failed to delete eventObj");
            }

            Event deletedEvent = await _eventObjRepository.Delete(eventObjToDelete);
            return Ok(deletedEvent);
        }
    }
}