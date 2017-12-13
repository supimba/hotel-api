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
 * For an explanation about Controllers, see  in the Controllers folder.
 * MVC Architecture.txt
 *     1. MVC Architecture.txt
 *        HotelController.cs
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
