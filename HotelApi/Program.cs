/* PROGRAM ARCHITECTURE
 *
 * Here is a reminder of the program structure:
 * 
 * The Hotel Reservation project uses the 3-Tier architecture to separate the UI, Business logic 
 * and Data access layers. The Data layer should be accessible independent of main program and 
 * therefore, the DAL was carved out of the main project into the HotelApi project. The Common
 * project is the business logic layer that merely defines model classes that will be consumed by
 * HotelApi and Hotel Reservation Sytsem (HRP). The HRP project is the main project and the
 * presentation layer of this program. It a console application that exposes a text-based UI to the
 * user.
 * 
 * HotelApi is partially structured in the Model-View-Controller (MVC) architectural pattern. Know
 * that HotelApi is a WebAPI and as such, it does not need to implement Views (V). What the HotelApi
 * project does is implement Controllers (C) and get its Models (M) from the Common project.
 *
 * In the Controllers folder, see ValuesController.cs for an explanation of Controllers.
 *
 * CONTROLLER CLASSES
 * This file was auto-generated when the project was created.
 * 
 * The main point of HotelApi is to define controllers. The Controller refers to the C in the MVC
 * pattern. What is a controller? An application has inputs. Each input needs logic to determine
 * what needs to be done with it. The controller holds that logic.
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
