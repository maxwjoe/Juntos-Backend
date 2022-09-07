using Juntos.Data;
using Juntos.Interfaces;
using Juntos.Models;
using Microsoft.AspNetCore.Mvc;

namespace Juntos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly IEventRepository _eventRepository;

        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }


        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetAllEvents()
        {
            var eventsDb = await _eventRepository.GetAll();
            return Ok(eventsDb);
        }

        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent([FromBody] EventDto request)
        {

            Event newEvent = new Event
            {
                CapacityLimit = request.CapacityLimit,
                BookingTimeLimitMinutes = request.BookingTimeLimitMinutes,
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                DoesRepeat = request.DoesRepeat,
                RepeatOption = request.RepeatOption,
                EventDateAndTime = request.EventDateAndTime,
                OwnerId = request.OwnerId,
                AssociatedClub = request.AssociatedClub,
                AllowedMembershipRefs = request.AllowedMembershipRefs,
            };

            Event createdEvent = await _eventRepository.Add(newEvent);

            if (createdEvent == null)
            {
                return BadRequest("Unable to create event");
            }

            return Ok(createdEvent);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Event>> UpdateEvent([FromBody] EventDto updates, [FromRoute] int id)
        {
            Event existingEvent = await _eventRepository.GetByIdAsync(id);

            if (existingEvent == null)
            {
                return BadRequest("Could not find event");
            }

            Event updatedEvent = await _eventRepository.Update(existingEvent, updates);
            return Ok(updatedEvent);
        }

    }
}