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
    [Route("api/HotelRoom")]
    public class HotelRoomController : Controller
    {
        private readonly Context _context;

        public HotelRoomController(Context context)
        {
            _context = context;
        }

        // GET: api/HotelRoom
        [HttpGet]
        public IEnumerable<HotelRoom> GetHotelRooms()
        {
            return _context.HotelRooms;
        }

        // GET: api/HotelRoom/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelRoom([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotelRoom = await _context.HotelRooms.SingleOrDefaultAsync(m => m.RoomNumber == id);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return Ok(hotelRoom);
        }

        // PUT: api/HotelRoom/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelRoom([FromRoute] int id, [FromBody] HotelRoom hotelRoom)
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
                if (!HotelRoomExists(id))
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

        // POST: api/HotelRoom
        [HttpPost]
        public async Task<IActionResult> PostHotelRoom([FromBody] HotelRoom hotelRoom)
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
                if (HotelRoomExists(hotelRoom.RoomNumber))
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

        // DELETE: api/HotelRoom/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelRoom([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotelRoom = await _context.HotelRooms.SingleOrDefaultAsync(m => m.RoomNumber == id);
            if (hotelRoom == null)
            {
                return NotFound();
            }

            _context.HotelRooms.Remove(hotelRoom);
            await _context.SaveChangesAsync();

            return Ok(hotelRoom);
        }

        private bool HotelRoomExists(int id)
        {
            return _context.HotelRooms.Any(e => e.RoomNumber == id);
        }
    }
}