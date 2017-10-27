/* THE ARCHITECTURE OF THE HOTEL RESERVATION SYSTEM PROJECT
 *
 * The Hotel Reservation System Program (HRSP) has three parts. It uses the multi-tier architecture
 * to separate the UI, Business logic and Data Access layers. The three layers are: 
 *
 *     1. Hotel Reservation System: Presentation Layer (referred to as HRS/Client Application)
 *     2. Common: Business Logic Layer
 *     3. HotelApi: Data Access Layer (referred to as DAL)
 * 
 * Here is a review of this program's architecture:
 *
 * HRS is the client application. It is a console program that presents a text-based UI to the
 * user. It authenticates the user and determines the level of access to the DAL. HotelApi was
 * carved out of the main project into the HotelApi project because the DAL should be program and
 * language agnostic i.e. it should be accessible independent of the main program. HotelApi is
 * partially structured in the Model-View-Controller (MVC) architectural pattern. The Common
 * project is the business logic layer that defines model classes that will be consumed
 * by HotelApi and Hotel Reservation (HRS). 
 *
 * The Multi-tier architecture is designed for client-server architectures, not just for a single
 * application architecture. This architecture splits an application into different layers, in turn,
 * creating different applications and services. Here's a short-introduction to the client-server
 * architecture:
 *
 *     Client-server architecture (client/server) is a network architecture in which each computer
 *     or process on the network is either a client or a server.
 *
 *     Servers are powerful computers or processes dedicated to managing disk drives (file servers),
 *     printers (print servers), or network traffic (network servers). Clients are PCs or
 *     workstations on which users run applications. Clients rely on servers for resources, such as
 *     files, devices, and even processing power.
 *     (Source: https://www.webopedia.com/TERM/C/client_server_architecture.html)
 *
 * In the HRSP, the HRS is the client program. This console program runs client side and every
 * end-user has an instance of this program running on their computer. The WebAPI project,
 * HotelAPI, is the server-side program, which will provide database access to all authorized
 * clients. There will be only one instance of HotelApi, but many running instances of HRS.
 * Common is a class library that will compile down to a .dll file. It will be included with both
 * HotelApi and HRS.
 *
 * MVC is a better architecture for a single application. The main purpose is to separate the
 * application logic from the user interface. MVC and multi-tier architectures are not exclusive,
 * you aren't limited to choosing one or the other. HRSP uses both architectures. The HRS, Common,
 * and HotelApi make up the multi-tier architecture. Note that HotelApi is a WebAPI and as such,
 * it does not need to implement Views (V). What the HotelApi project does is implement
 * Controllers (C) while getting its Models (M) from the Common project.
 *
 * In the Controllers folder, see HotelController.cs for an explanation of Controllers.
 * 
 *
 ***************************************************************************************************
 ***************************************************************************************************
 *
 *
 * MODEL-VIEW-CONTROLLER ARCHITECTURE
 *
 * Here is an introduction to MVC from Wikipedia:
 * (https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93controller)
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
 * The 3-tier architecture organizes its components into a hierarchy: The presentation layer, which
 * is the user interface layer, sits on top, the logic layer sits in the middle, and the data access
 * layer sits at the bottom. The client layer never directly talks to the data layer; it must pass
 * through the logic layer to talk to the data layer. Interactions between layers is linear and
 * hierarchical. The 3-tier architecture separates a programs into modules, based on function. 
 *
 * MVC has three components: Model, View, and Controller. MVC is a software architectural pattern
 * that is primarily used to implement user interfaces (i.e. the presentation layer in the 3-tier
 * program). Unlike the 3-tier architecture, MVC's components interact in a triangular structure.
 *
 * 1. Model: The model layer consists of classes that define important business entities in your
 *    program. Example: In a Calendar program, Dates, Alarms, Contacts etc. are model objects on
 *    which the program will operate. These objects will be stored in the program's database, so
 *    the data layer will also need access to models. The model layer structures data objects and
 *    handles their storage.
 *
 * 2. View: "A view is a (visual) representation of its model. It would ordinarily highlight
 *    certain attributes of the model and suppress others. It is thus acting as a presentation
 *    filter. (Source: https://blog.codinghorror.com/understanding-model-view-controller/) The
 *    View layer is the presentation layer i.e. it handles what the user sees and interacts with.
 *    A webpage, a GUI, a text-based UI etc.
 *
 * 3. Controller: A controller is the link between a user and the program. It feeds information to
 *    the views on the user's screen and receives user input. The Controller will process and
 *    respond to user-initiated events by changing models and views. The Controller layer controls
 *    changes to the program by making decisions to user input.
 *
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
