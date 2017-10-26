/* THE ARCHITECTURE OF HOTELAPI
 *
 * Here is a review of this program's architecture:
 * 
 * The Hotel Reservation System Program (HRSP) uses the 3-Tier architecture to separate the UI, 
 * Business logic and Data access layers. The DAL was carved out of the main project into the
 * HotelApi project because the Data layer should be program & language agnostic i.e. accessible
 * independent of the main program. The Common project is the business logic layer that defines
 * model classes that will be consumed by HotelApi and Hotel Reservation (HR). The HR project is
 * the the presentation layer of this program. It a console application that exposes a text-based
 * UI to the user.
 * 
 * HotelApi is partially structured in the Model-View-Controller (MVC) architectural pattern. Note
 * that HotelApi is a WebAPI and as such, it does not need to implement Views (V). What the HotelApi
 * project does is implement Controllers (C) while getting its Models (M) from the Common project.
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
 * 1. Model: are classes that define important business entities in your program. Example:
 *    In a Calendar program, Dates, Alarms, Contacts etc. are model objects on which the app will
 *    operate.
 *
 * 2. View: "A view is a (visual) representation of its model. It would ordinarily highlight
 *    certain attributes of the model and suppress others. It is thus acting as a presentation
 *    filter. (Source: https://blog.codinghorror.com/understanding-model-view-controller/)
 *
 * 3. Controller: "A controller is the link between a user and the system. It provides the user
 *    with input by arranging for relevant views to present themselves in appropriate places on the
 *    screen. It provides means for user output by presenting the user with menus or other means of
 *    giving commands and data. The controller receives such user output, translates it into the
 *    appropriate messages and pass these messages on to one or more of the views."
 *    (Source: https://blog.codinghorror.com/understanding-model-view-controller/)
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
