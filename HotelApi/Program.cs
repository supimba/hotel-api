/* THE ARCHITECTURE OF THE HOTELAPI PROJECT
 *
 * 
 * This project encapsulates the Data Access Layer and the Controllers of the Hotel Reservation
 * System Project. It is a WebAPI project intended to be a standalone program that runs on a server.
 * This project's intended client is Hotel Reservation System (HRS), but it is designed to be
 * language and program agnostic. In other words, HotelApi can talk to clients besides HRS, even if
 * they are written in other languages.
 *
 * HotelAPI is partially structured in the MVC pattern. Specifically, it gets Models (M) from
 * the Common project and implements Controllers (C). As it is a WebAPI project, it should not and
 * does not have Views (V) implemented. That is the business of the HRS project. See below for a
 * discussion about this architecture.
 *
 * This project has a lot of notes, that are essential to learning how this project is structured
 * and how it operates. Topics include: How the MVC architecture works, Database Setup,
 * Data Providers, Data Model, Schema, Migrations, Controllers, Endpoints, REST architecture, HTTP
 * Requests, Route Variables and more. Read the notes in this order:
 * 
 *     1. WHAT IS MVC?:                    MVC Architecture.txt (HotelApi folder)
 *     2. DATABASE SETUP AND CONCEPTS:     Database Setup.txt (HotelApi folder)
 *     3. ALL ABOUT MIGRATIONS:            Migrations.txt (Migrations folder)
 *     4. CONTROLLERS & ENDPOINTS:         HotelController.cs (Controllers folder)
 *
 ***************************************************************************************************
 ***************************************************************************************************
 *
 * 
 * Please note that both Program.cs and Startup.cs were auto-generated when this WebAPI project was
 * created.
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
