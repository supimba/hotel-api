/* THE ARCHITECTURE OF THE HOTELAPI PROJECT
 *
 * This project encapsulates the Data Access Layer and the Controllers of the Hotel Reservation
 * System Project. It is designed as a WebAPI project and intended to be a standalone program that
 * runs on a server. This project's intended client is Hotel Reservation System (HRS), but it is
 * designed to be language and program agnostic. In other words, HotelApi can talk to clients
 * besides HRS, even if they are written in other languages.
 *
 * HotelAPI is partially structured in the MVC pattern. Specifically, it gets Models (M) from
 * the Common project and implements Controllers (C). As it is a WebAPI project, it should not and
 * does not have Views (V) implemented. That is the business of the HRS project. See below for a
 * discussion about this architecture.
 *
 * For an explanation about Controllers, see HotelController.cs in the Controllers folder.
 *
 *     1. MVC Architecture.txt
 *
 ***************************************************************************************************
 ***************************************************************************************************
 *
 * 
 * MODEL-VIEW-CONTROLLER ARCHITECTURE
 *
 * Here is an introduction to MVC:
 * (Source: https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93controller)
 *
 *     Model–view–controller (MVC) is a software architectural pattern for implementing user
 *     interfaces on computers. It divides a given application into three interconnected parts.
 *     This is done to separate internal representations of information from the ways information
 *     is presented to, and accepted from, the user. The MVC design pattern decouples these major
 *     components allowing for efficient code reuse and parallel development.
 *
 *     Traditionally used for desktop graphical user interfaces (GUIs), this architecture has
 *     become popular for designing web applications and even mobile, desktop and other clients.
 *     Popular programming languages like Java, C#, Ruby, PHP and others have popular MVC frameworks
 *     that are currently being used in web application development straight out of the box.
 *
 * The multi-tier architecture traditionally organizes a codebase into a 3-tier hierarchy: The
 * presentation layer, which is the user interface layer, sits on top of the stack of layers; the
 * logic layer sits in the middle, and the data access layer sits at the bottom. The client layer
 * never directly talks to the data layer, it must pass through the logic layer to talk to the data
 * layer. Interactions between layers is linear and hierarchical. This architecture separates a
 * program into modules, based on function. It is best suited for programs in a client-server
 * relationship.
 *
 * MVC is a software architectural pattern that is primarily used for individual programs/modules,
 * not client-server architectures. To modularize the program, it is necessary to "separate
 * internal representations of information from the ways information is presented to, and accepted
 * from, the user. The MVC design pattern decouples these major components allowing for efficient
 * code reuse and parallel development."
 * (Source: https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93controller) MVC seeks to
 * separate concerns by extracting these components into 3 layers: Model, View, and Controller.
 * Unlike the multi-tier architecture, MVC's components interact in a rectangular structure. To
 * decipher the diagram that explains this structure, start with the User.
 *
 * 
 *                           +-----------------+
 *                           |     MODELS      |
 *        +----------------+ |                 | <----------------+
 *        | If the Model     |  (IN DATABASE)  |   The Controller |
 *        | Changes, the     +-----------------+   Manipulates    |
 *        | View gets                              Models in the  |
 *        | Updated                                DB.            |
 *        |                                                       |
 *        v                                                       +
 *                          The View sends user input
 * +-----------------+          to the Controller          +-----------------+
 * |                 | +---------------------------------> |                 |
 * |       VIEW      |                                     |    CONTROLLER   |
 * |                 | <---------------------------------+ |                 |
 * +-----------------+  The Controller sometimes updates   +-----------------+
 *                             the View directly
 *        +                                                       ^
 *        |                                                       |
 *        |                                                       |
 *        | The User                               The User issues|
 *        | Interface (UI)                         orders via the |
 *        | presents info    +-----------------+   UI to the      |
 *        | to the user      |                 |   Controller     |
 *        +--------------->  |     THE USER    | -----------------+
 *                           |                 |
 *                           +-----------------+
 *
 *
 * The three parts of MVC:
 * 
 * 1. Model: The model layer consists of classes that define important business entities in your
 *    program. Example: In a Calendar program, Dates, Alarms, Contacts etc. are model objects on
 *    which the program will operate. These objects will be stored in the program's database.
 *
 *    Despite the fact that the database will contain model objects, the implementation of a
 *    program's data access layer (the database) and all its associated business logic should be
 *    separate from the Model layer. 
 *
 * 2. View: "A view is a (visual) representation of its model. It would ordinarily highlight
 *    certain attributes of the model and suppress others. It is thus acting as a presentation
 *    filter. (Source: https://blog.codinghorror.com/understanding-model-view-controller/) The
 *    View layer is the presentation layer i.e. it formats and presents data from models to the
 *    user. Armed with this information, the User can use the View (UI) to issue orders to to the
 *    program. The View can be a webpage, a GUI, a text-based UI etc.
 *
 * 3. Controller: The controller layer is the brain of an application. The Controller layer will
 *    respond to user-initiated events by issuing orders to models and views. By doing these
 *    things, this layer controls and directs application flow. When it receives user requests
 *    via the UI, Controllers will process, interpret and validate these requests. Then it will
 *    query and modify models and create or update Views to fulfill the user request.
 * 
 *
 ***************************************************************************************************
 ***************************************************************************************************
 *
 *
 * Please note that both Program.cs and Startup.cs were auto-generated.
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HotelApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
