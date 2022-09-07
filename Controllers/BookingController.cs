using System.Security.Claims;
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
        public BookingController(IBookingRepository bookingRepository, IAuthService authService)
        {
            _bookingRepository = bookingRepository;
            _authService = authService;
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
        public async Task<ActionResult<Booking>> CreateNewBooking(BookingDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid Booking");
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