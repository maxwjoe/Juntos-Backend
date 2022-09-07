using Juntos.Interfaces;
using Juntos.Models;
using Microsoft.AspNetCore.Mvc;

namespace Juntos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private readonly IEventRepository _eventobjRepository;
        public EventController(IEventRepository eventobjRepository)
        {
            _eventobjRepository = eventobjRepository;
        }


        // GetAllEvents : Gets all the eventobjs in the database
        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetAllEvents()
        {
            var eventobjsDb = await _eventobjRepository.GetAll();

            if (eventobjsDb == null)
            {
                return BadRequest("Could not get eventobjs");
            }

            return Ok(eventobjsDb);
        }


        // CreateNewEvent : Creates a new eventobj in the database
        [HttpPost]
        public async Task<ActionResult<Event>> CreateNewEvent(EventDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid Event");
            }

            Event newEvent = new Event
            {
                OwnerId = request.OwnerId,
                ClubId = request.ClubId,
                CapacityLimit = request.CapacityLimit,
                BookingTimeLimit = request.BookingTimeLimit,
                RepeatOption = request.RepeatOption,
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                EventImageUrl = request.EventImageUrl,
                EventDateAndTime = request.EventDateAndTime
            };

            Event createdEvent = await _eventobjRepository.Create(newEvent);

            return Ok(createdEvent);
        }


        // UpdateExistingEvent : Updates an existing eventobj in Db
        [HttpPut]
        [Route("{eventobjId}")]
        public async Task<ActionResult<Event>> UpdatedExistingEvent([FromBody] EventDto updates, [FromRoute] int eventobjId)
        {
            Event existingEvent = await _eventobjRepository.GetByIdAsync(eventobjId);

            if (updates == null)
            {
                return Ok(existingEvent);
            }

            if (existingEvent == null)
            {
                return BadRequest("Event does not exist");
            }

            Event updatedEvent = await _eventobjRepository.Update(existingEvent, updates);

            return Ok(updatedEvent);
        }


        // DeleteExistingEvent : Deletes an existing eventobj from Db
        [HttpDelete]
        [Route("{eventobjId}")]
        public async Task<ActionResult<Event>> DeleteExistingEvent([FromRoute] int eventobjId)
        {
            Event deletedEvent = await _eventobjRepository.Delete(eventobjId);
            return Ok(deletedEvent);
        }



    }
}