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
    [Route("api/RoomReservation")]
    public class RoomReservationController : Controller
    {
        private readonly Context _context;

        public RoomReservationController(Context context)
        {
            _context = context;
        }

        // GET: api/RoomReservation
        [HttpGet]
        public IEnumerable<RoomReservation> GetRoomReservations()
        {
            return _context.RoomReservations;
        }

        // GET: api/RoomReservation/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomReservation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomReservation = await _context.RoomReservations.SingleOrDefaultAsync(m => m.RoomId == id);

            if (roomReservation == null)
            {
                return NotFound();
            }

            return Ok(roomReservation);
        }

        // PUT: api/RoomReservation/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomReservation([FromRoute] int id, [FromBody] RoomReservation roomReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != roomReservation.RoomId)
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
                if (!RoomReservationExists(id))
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

        // POST: api/RoomReservation
        [HttpPost]
        public async Task<IActionResult> PostRoomReservation([FromBody] RoomReservation roomReservation)
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
                if (RoomReservationExists(roomReservation.RoomId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRoomReservation", new { id = roomReservation.RoomId }, roomReservation);
        }

        // DELETE: api/RoomReservation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomReservation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomReservation = await _context.RoomReservations.SingleOrDefaultAsync(m => m.RoomId == id);
            if (roomReservation == null)
            {
                return NotFound();
            }

            _context.RoomReservations.Remove(roomReservation);
            await _context.SaveChangesAsync();

            return Ok(roomReservation);
        }

        private bool RoomReservationExists(int id)
        {
            return _context.RoomReservations.Any(e => e.RoomId == id);
        }
    }
}