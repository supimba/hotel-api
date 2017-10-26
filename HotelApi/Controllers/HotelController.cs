/* CONTROLLER CLASSES
 * The main point of HotelApi is to define controllers. The Controller refers to the C in the MVC
 * pattern. What is a controller? An application has inputs. Each input needs logic to determine
 * what needs to be done with it. A controller holds logic to manage inputs.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Common.Models;
using PostgresEFCore.Providers;

namespace HotelApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Hotel")]
    public class HotelController : Controller
    {
        private readonly Context _context;

        public HotelController(Context context)
        {
            _context = context;
        }

        // GET: api/Hotel
        [HttpGet]
        public IEnumerable<Hotel> GetHotels()
        {
            return _context.Hotels;
        }

        // GET: api/Hotel/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotel([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotel = await _context.Hotels.SingleOrDefaultAsync(m => m.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

        // PUT: api/Hotel/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel([FromRoute] long id, [FromBody] Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hotel.Id)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!hotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotel
        [HttpPost]
        public async Task<IActionResult> PostHotel([FromBody] Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotel = await _context.Hotels.SingleOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return Ok(hotel);
        }

        // GET: api/Hotel/Room
        [HttpGet("Room")]
        public IEnumerable<HotelRoom> GetHotelRooms()
        {
            return _context.HotelRooms;
        }

        // GET: api/Hotel/2/Room/5
        [HttpGet("{id}/Room/{roomNumber}")]
        public async Task<IActionResult> GetHotelRoom([FromRoute] long id, [FromRoute] long roomNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotelRoom = await _context.HotelRooms.SingleOrDefaultAsync(m => m.RoomNumber == roomNumber && m.HotelId == id);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return Ok(hotelRoom);
        }

        // PUT: api/Hotel/2/Room/5
        [HttpPut("{id}/Room/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom([FromRoute] int id, [FromRoute] long roomNumber, [FromBody] HotelRoom hotelRoom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hotelRoom.RoomNumber)
            {
                return BadRequest();
            }

            _context.Entry(hotelRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!hotelRoomExists(id, roomNumber))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotel/5/Room
        [HttpPost("{id}")]
        public async Task<IActionResult> PostHotelRoom([FromRoute] long id, [FromBody] HotelRoom hotelRoom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.HotelRooms.Add(hotelRoom);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (hotelRoomExists(id, hotelRoom.RoomNumber))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHotelRoom", new { id = hotelRoom.RoomNumber }, hotelRoom);
        }

        // DELETE: api/Hotel/2/Room/5
        [HttpDelete("{id}/Room/{roomNumber}")]
        public async Task<IActionResult> DeleteHotelRoom([FromRoute] long id, [FromRoute] long roomNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotelRoom = await _context.HotelRooms.SingleOrDefaultAsync(m => m.RoomNumber == roomNumber && m.HotelId == id);
            if (hotelRoom == null)
            {
                return NotFound();
            }

            _context.HotelRooms.Remove(hotelRoom);
            await _context.SaveChangesAsync();

            return Ok(hotelRoom);
        }

        // GET: api/Hotel/Room/Reservation
        [HttpGet("Room/Reservations")]
        public IEnumerable<RoomReservation> GetRoomReservations()
        {
            return _context.RoomReservations;
        }

        // GET: api/Hotel/2/Room/6/Reservation/5
        [HttpGet("{id}/Room/{roomNumber}/Reservation/{reservationId}")]
        public async Task<IActionResult> GetRoomReservationForRoom([FromRoute] long id, [FromRoute] long roomNumber, [FromRoute] long reservationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomReservation = await _context.RoomReservations.SingleOrDefaultAsync(m => m.RoomNumber == id && m.HotelId == id && m.ReservationId == reservationId);

            if (roomReservation == null)
            {
                return NotFound();
            }

            return Ok(roomReservation);
        }

        // PUT: api/Hotel/8/Room/3/Reservation/5
        [HttpPut("{id}/Room/{roomNumber}/Reservation/{reservationId}")]
        public async Task<IActionResult> PutRoomReservation([FromRoute] long id, [FromRoute] long roomNumber, [FromRoute] long reservationId, [FromBody] RoomReservation roomReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomReservation.RoomNumber)
            {
                return BadRequest();
            }

            _context.Entry(roomReservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!roomReservationExists(id, roomNumber, reservationId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotel/8/Room/5/Reservation/4
        [HttpPost("{id}/Room/{roomNumber}/Reservation/{reservationId}")]
        public async Task<IActionResult> PostRoomReservation([FromRoute] long id, [FromRoute] long roomNumber, [FromRoute] long reservationId, [FromBody] RoomReservation roomReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RoomReservations.Add(roomReservation);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (roomReservationExists(id, roomNumber, reservationId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRoomReservation", new { id = roomReservation.RoomNumber }, roomReservation);
        }

        // DELETE: api/Hotel/8/Room/3/Reservation/5
        [HttpDelete("{id}/Room/{roomNumber}/Reservation/{reservationId}")]
        public async Task<IActionResult> DeleteRoomReservation([FromRoute] int id, [FromRoute] long roomNumber, [FromRoute] long reservationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomReservation = await _context.RoomReservations.SingleOrDefaultAsync(m => m.RoomNumber == roomNumber && m.HotelId == id && m.ReservationId == reservationId);
            if (roomReservation == null)
            {
                return NotFound();
            }

            _context.RoomReservations.Remove(roomReservation);
            await _context.SaveChangesAsync();

            return Ok(roomReservation);
        }

        private bool hotelExists(long id)
        {
            return _context.Hotels.Any(e => e.Id == id);
        }

        private bool hotelRoomExists(long id, long roomNumber)
        {
            return _context.HotelRooms.Any(e => e.RoomNumber == roomNumber && e.HotelId == id);
        }

        private bool roomReservationExists(long id, long roomNumber, long reservationId)
        {
            return _context.RoomReservations.Any(e => e.RoomNumber == id);
        }
    }
}