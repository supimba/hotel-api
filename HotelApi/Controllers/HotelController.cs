/* CONTROLLER CLASSES AND ENDPOINT METHODS
 * 
 * One of the main purposes of the HotelApi project is to define controllers (The other is to set
 * up the Data Access Layer.). The Controller refers to the C in the MVC pattern. What is a
 * controller? When a user provides input to an application, controllers process and respond to it.
 * Controllers contain logic to query and alter models and to create and update new views.
 *
 * The Controller for this project is the class HotelController. In a non-networked program, a
 * Controller class has normal methods that control application flow by reacting to user actions.
 * But for networked projects such as this one, where a multi-tier architecture separates the
 * project into a user-facing client program, a server-based database backend and Common clases
 * library, the client must communicate to the database over a network. In this context, 'Network'
 * can refer to the internet or LANs such as your home network. If the client and server are
 * running on the same machine, the communication happens only on the local network.
 *
 * To facilitate inter-program communication, HotelApi must have its Controller class
 * (HotelController) expose an API to the network. Through this API, any network-based program can
 * communicate with the database. For the special case of networked programs, HotelController
 * uses a collection of public methods called Endpoints to expose an API to the network. In the
 * context of an API, Endpoints are just public methods inside Controller classes that can be called
 * through a network. All endpoints are controller methods, but not all controller methods
 * are endpoints. 
 * 
 * HotelController implements the REST (Representational State Transfer) architecture. It's an
 * architecture for managing state information in designing distributed systems. "It is not a
 * standard but a set of constraints, such as being stateless, having a client/server relationship,
 * and a uniform interface. REST is not strictly related to HTTP, but it is most commonly associated
 * with it." (Source: https://spring.io/understanding/REST) All network-facing APIs i.e. Endpoints,
 * that are intended for communicating over the internet use the HTTP protocol to send and receive
 * requests.
 *
 * This controller class features HTTP requests such as GET, PUT, POST  etc. that implement Create,
 * Read, Update and Delete (CRUD) operations for Hotel, HotelRoom and RoomReservation objects. The
 * other tables in the database are lookup tables, on which CRUD ops will not be performed. Hence, 
 * only these three tables have endpoints.
 *
 *
 * GENERATING CONTROLLER CLASSES
 * 
 * The methods in this class were all auto-generated. 
 * 
 * VISUAL STUDIO
 * 
 * In Visual Studio, right-click the Controller folder, select 
 * Add | Controller | (API Controller with actions, using Entity Framework). In the message box 
 * that pops up, select the Model class (Hotel, HotelRoom or RoomReservation), the Data Context 
 * Class (Context.cs), and the name of the Controller. Do this three times to generate controllers 
 * for Hotel, HotelRoom and RoomReservation objects. HotelController.cs is an amalgamation of 
 * these three files. 
 * 
 * INTELLIJ RIDER
 * As of September 11, 2017, this feature does not exist in IntelliJ Rider; you may be able to 
 * generate it through the terminal. See this link:
 * https://stackoverflow.com/questions/41011700/how-to-generate-controller-using-dotnetcore-command-line
 * 
 * Go the the main program folder (hotel-api) and find the project folder (HotelApi). Open a 
 * command prompt window. If you want to see general options, run 'dotnet ef -help'.
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
    /* CLASS ATTRIBUTES: Attributes are often used on methods and properties, but they can be added
     * to the class. What the attributes do depend on which ones were included. Attributes can be
     * thought of as helper tags. Sample use cases: You can use attributes to require that a user 
     * has to be authenticated to be able to use that class, or specify a middleware, an exception
     * handler, or just extra information the class can use.
     *
     * For the HotelController class, the attributes add metadata to the assemblies. The first
     * attribute probably warns consumers of this class that its output is in JSON. The second
     * attribute specifies that routes to the endpoints in this class must begin with `api/Hotel`.
     * See below for much more on Route variables, which are very important.
     */
    [Produces("application/json")]
    [Route("api/Hotel")]
    public class HotelController : Controller
    {
        // This is an instance of class Context. Class Context defines the Data Model of the 
        // database. All endpoints in this class, which are defined in the methods below, talk to 
        // the database through _context. As this instance is readonly, it prevents modification of 
        // the model relationships in the class when the program is running, but through this 
        // instance of Context class, HotelController can perform Create, Read, Update & Delete
        // (CRUD) operations on the database.
        private readonly Context _context;
        
        // When this program runs, .NET Core will instantiate HotelController and pass it a Context 
        // object. When HotelController is instantiated, it assigns a private instance of the 
        // Context class, with the options that were defined in the MyDbContextFactory class 
        // (hostname, username, password, connection string and database name), which contains all 
        // the Model and Relation information that was defined in the Context class. This gives
        // the controller methods below direct access to the database.
        public HotelController(Context context)
        {
            _context = context;
        }
        
        
        // PRIVATE METHODS (Not Endpoints):
        // This private method checks the database to determine if a Hotel object exists.
        private bool hotelExists(long id)
        {
            return _context.Hotels.Any(e => e.Id == id);
        }

        
        // This private method checks the database to determine if a HotelRoom object exists.
        private bool hotelRoomExists(long id, long roomNumber)
        {
            return _context.HotelRooms.Any(e => e.RoomNumber == roomNumber && e.HotelId == id);
        }
        

        // This private method checks the database to determine if a Room Reservation object exists.
        private bool roomReservationExists(long id, long roomNumber, long reservationId)
        {
            return _context.RoomReservations.Any(e => e.RoomNumber == id);
        }
        
        
        // This endpoint returns a Collection of all Hotels in the database. (R in CRUD)
        // GET: api/Hotel
        [HttpGet]
        public IEnumerable<Hotel> GetHotels()
        {
            return _context.Hotels;
        }

        
        // ENDPOINTS:
        /* ROUTE VARIABLE
         * Just above this endpoint, note this attribute: [HttpGet("{id}")]. It lets the method know
         * that it will be invoked through incoming HTTP GET requests. Every endpoint has a unique
         * identifier called a ROUTE and this attribute defines a part of it for the endpoint.
         * "{id}" is a part of the route called the Action. More on this in the third paragraph.
         *
         * Why is a route needed when endpoint methods have names? A client program cannot directly
         * invoke methods on a server program over a network. Instead, when a client wants to call
         * an endpoint in a Web API, it makes an HTTP request to the server. In the request body,
         * the client will include all the data the invoked endpoint will need. This data is passed
         * to the server in a string called the route, via a HTTP request. 
         *
         * Before making a call to an endpoint, consult the API documentation to identify the route
         * of the endpoint you want to invoke. In MVC, the route has a standardized format:
         * {domain}/{controller}/{action}. The {domain} name is the address that hosts the server.
         * The {controller} is the name of the controller. The {action} is an identifier for a
         * specific endpoint in the controller class. Actions are defined in an attribute above an
         * endpoint method. They can be a number or strings; check the type of the endpoint's
         * paramter.
         *
         * Let's look at some examples:
         *
         *     Route: "aws.amazon.com/hostName/Hotel/5" (domain/controller/action)
         *     Action: "5" or "users/98" or "users/3453/parent/mother/children" 
         *
         * 
         * THE GetHotel() ENDPOINT
         * 
         * The auto-generated comment above this endpoint notes the general route format:
         * api/Hotel/5. This appears to be a generic, example route, not a real one. Check the API
         * docs, for GetHotel()'s real route. An action value uniquely identifies this endpoint.
         * What does this endpoint's action value looks like? How does it help to distinguish
         * between two endpoints that accept long values when callers cannot invoke by method name?
         * The answer may be beyond the scope of this comment.
         * 
         * When the client invokes this endpoint, GetHotel(), it will retrieve a Hotel object from
         * the database and return it to the caller. GetHotel() specifies its input requirements
         * in its parameters: it requires the caller to provide the ID number of the desired hotel
         * object. Note that the id parameter is actually annotated with the [FromRoute] attribute
         * which corresponds to the [HttpGet("{id}")] attribute above GetHotel(). This means that
         * id argument will come from the route of the current request. It must be a long as
         * demanded by the parameter.
         *
         * Potentially Useful Source on Attribute Routing:
         * https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2
         */
        
        // This endpoint method returns a Hotel object from the Database to the client. (R in CRUD)
        // GET: api/Hotel/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotel([FromRoute] long id)
        {
            // Task<IActionResult> is the standard response for an asynchronous method.
            // IActionResult is the standard response type for a synchronous method.
            
            // This if-statement's purpose is unknown; it may be checking if all the properties of 
            // the model have values.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            // Note the id argument passed into this method. This line searches the database for an
            // object matching the id argument. This doesn't have to be done asynchronously, but 
            // since this will take an unknown amount of time, you don't want this request to block
            // the running of the rest of the application.
            var hotel = await _context.Hotels.SingleOrDefaultAsync(m => m.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            // This line returns an HTTP status code for OK and a hotel object from the database.
            // The HTTP Status code for OK = 200.
            return Ok(hotel);
        }

        
        // This method updates a Hotel object in the Database. (U in CRUD)
        // PUT: api/Hotel/5
        // The statement below (called a route string) is a put request attribute that specifies a 
        // Hotel Id. It is the Id of the Hotel that this method will update. It is passed in from 
        // EF Core. (??)
        [HttpPut("{id}")]        
        public async Task<IActionResult> PutHotel([FromRoute] long id, [FromBody] Hotel hotel)
        {
            // This if-statement's purpose is unknown.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            // The client makes a request and it comes in over the network. Before proceeding, this 
            // if statement checks if the Id number is found in the database. The if statement is 
            // checking if the Id from the route string (see the attribute above this method) 
            // matches a Hotel Id in the database. (??)
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            // This statement sets some state property to indicate that the object was modified so 
            // EF Core will do something with it.
            _context.Entry(hotel).State = EntityState.Modified;

            
            // This try-catch statement is attempting to save the state change above asynchronously.
            // It is prepared to catch and handle a concurrency exception that might be thrown.
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

            // This returns an HTTP status code. NoContent's status code = 204. This is returned
            // as a IActionResult, which has to be wrapped in Task<> in async methods.
            return NoContent();
        }

        
        // This endpoint method receives a Hotel object from the client and creates a new Hotel in
        // the Database. (C in CRUD)
        // POST: api/Hotel
        [HttpPost]
        public async Task<IActionResult> PostHotel([FromBody] Hotel hotel)
        {
            // This if-statement's purpose is unknown.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // A Hotel object is added to the Database asynchronously. 
            _context.Hotels.Add(hotel);
            // This sentence adds the object to the database asynchronously so the program doesn't 
            // hang waiting for it to complete.
            await _context.SaveChangesAsync();

            // Poor Method Name. This instruction is unclear.
            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }
        
        // This endpoint method receives the ID of a Hotel object from the client and deletes this
        // object from the Database. (D in CRUD)
        // DELETE: api/Hotel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel([FromRoute] long id)
        {
            // This if-statement's purpose is unknown.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // The next section of statements searches the database asynchronously for a hotel 
            // object that matches the id argument...
            var hotel = await _context.Hotels.SingleOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            // ... which is then deleted from the database.
            _context.Hotels.Remove(hotel);
            // This sentence does the delete to the database asynchronously so the program doesn't 
            // hang waiting for it to complete.
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
        public async Task<IActionResult> GetHotelRoom([FromRoute] long id, 
                                                      [FromRoute] long roomNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotelRoom = await _context.HotelRooms
                .SingleOrDefaultAsync(m => m.RoomNumber == roomNumber && m.HotelId == id);

            if (hotelRoom == null)
            {
                return NotFound();
            }

            return Ok(hotelRoom);
        }

        
        // PUT: api/Hotel/2/Room/5
        [HttpPut("{id}/Room/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom([FromRoute] int id, 
                                                      [FromRoute] long roomNumber, 
                                                      [FromBody] HotelRoom hotelRoom)
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
        public async Task<IActionResult> PostHotelRoom([FromRoute] long id, 
                                                       [FromBody] HotelRoom hotelRoom)
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
        public async Task<IActionResult> DeleteHotelRoom([FromRoute] long id, 
                                                         [FromRoute] long roomNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hotelRoom = await _context.HotelRooms
                .SingleOrDefaultAsync(m => m.RoomNumber == roomNumber && m.HotelId == id);
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
        public async Task<IActionResult> GetRoomReservationForRoom([FromRoute] long id, 
                                                                   [FromRoute] long roomNumber, 
                                                                   [FromRoute] long reservationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomReservation = await _context.RoomReservations
                .SingleOrDefaultAsync(m => m.RoomNumber == id && m.HotelId == id 
                                           && m.ReservationId == reservationId);

            if (roomReservation == null)
            {
                return NotFound();
            }

            return Ok(roomReservation);
        }
        

        // PUT: api/Hotel/8/Room/3/Reservation/5
        [HttpPut("{id}/Room/{roomNumber}/Reservation/{reservationId}")]
        public async Task<IActionResult> PutRoomReservation([FromRoute] long id, 
                                                            [FromRoute] long roomNumber, 
                                                            [FromRoute] long reservationId, 
                                                            [FromBody] RoomReservation roomReservation)
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
        public async Task<IActionResult> PostRoomReservation([FromRoute] long id, 
                                                             [FromRoute] long roomNumber, 
                                                             [FromRoute] long reservationId, 
                                                             [FromBody] RoomReservation roomReservation)
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

            return CreatedAtAction("GetRoomReservation", new { id = roomReservation.RoomNumber }, 
                                                             roomReservation);
        }

        
        // DELETE: api/Hotel/8/Room/3/Reservation/5
        [HttpDelete("{id}/Room/{roomNumber}/Reservation/{reservationId}")]
        public async Task<IActionResult> DeleteRoomReservation([FromRoute] int id, 
                                                               [FromRoute] long roomNumber, 
                                                               [FromRoute] long reservationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var roomReservation = await _context.RoomReservations
                .SingleOrDefaultAsync(m => m.RoomNumber == roomNumber && m.HotelId == id 
                                           && m.ReservationId == reservationId);
            if (roomReservation == null)
            {
                return NotFound();
            }

            _context.RoomReservations.Remove(roomReservation);
            await _context.SaveChangesAsync();

            return Ok(roomReservation);
        }
    }
}