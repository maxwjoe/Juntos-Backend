using System.Security.Claims;
using Juntos.Helper;
using Juntos.Interfaces;
using Juntos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juntos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly IBookingRepository _bookingRepository;
        private readonly IAuthService _authService;
        private readonly IEventRepository _eventRepository;
        private readonly GeneralHelper _helper;
        public BookingController(IBookingRepository bookingRepository, IAuthService authService, IEventRepository eventRepository, GeneralHelper helper)
        {
            _bookingRepository = bookingRepository;
            _authService = authService;
            _eventRepository = eventRepository;
            _helper = helper;
        }


        // GetAllBookings : Gets all the bookings in the database belonging to an event
        [HttpGet]
        [Route("{eventId}")]
        public async Task<ActionResult<List<Booking>>> GetAllBookings([FromRoute] int eventId)
        {
            var bookingsDb = await _bookingRepository.GetAll(eventId);

            if (bookingsDb == null)
            {
                return BadRequest("Could not get bookings");
            }

            return Ok(bookingsDb);
        }


        // CreateNewBooking : Creates a new booking in the database
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Booking>> CreateNewBooking(BookingDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid Booking");
            }

            // Check the user has the correct membership
            User user = await _authService.GetUserObjFromToken();
            Event eventObj = await _eventRepository.GetByIdAsync(request.EventId);

            if (user == null || eventObj == null)
            {
                return BadRequest("Failed to book");
            }

            if (!_helper.ValidateMembership(user.MembershipId, eventObj.AllowedMemberships))
            {
                return BadRequest("Invalid Membership");
            }


            Booking newBooking = new Booking
            {
                UserId = request.UserId,
                EventId = request.EventId,
            };

            Booking createdBooking = await _bookingRepository.Create(newBooking);

            return Ok(createdBooking);
        }


        // UpdateExistingBooking : Updates an existing booking in Db
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("{bookingId}")]
        public async Task<ActionResult<Booking>> UpdatedExistingBooking([FromBody] BookingDto updates, [FromRoute] int bookingId)
        {
            Booking existingBooking = await _bookingRepository.GetByIdAsync(bookingId);

            if (existingBooking == null || updates == null)
            {
                return BadRequest("Invalid Params");
            }

            Booking updatedBooking = await _bookingRepository.Update(existingBooking, updates);

            return Ok(updatedBooking);
        }


        // DeleteExistingBooking : Deletes an existing booking from Db
        [HttpDelete]
        [Route("{bookingId}")]
        public async Task<ActionResult<Booking>> DeleteExistingBooking([FromRoute] int bookingId)
        {
            Booking bookingToDelete = await _bookingRepository.GetByIdAsync(bookingId);

            if (bookingToDelete == null)
            {
                return BadRequest("Booking does not exist");
            }

            Booking deletedBooking = await _bookingRepository.Delete(bookingToDelete);
            return Ok(deletedBooking);
        }



    }
}