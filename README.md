# The Hotel Reservation System: A Brief Introduction to the Application

The Hotel Reservation System project is a client-server set of programs that manages room reservations in hotels.

It has three projects:

1. **hotel-reservation:** This is the client program of this application. It is a console application that presents a text-based UI to authenticate users and determines their level of access to the database.

2. **hotel-api:** This is the data access layer (DAL) of the application and it runs on the server. It contains the database and associated controllers. 

3. **common:** This is the business logic layer of the program. It contains model classes that will be consumed by, and thus is common to, the other two projects.


# hotel-api: The Server Program

This application is a program and language agnostic WebAPI database intended to run on a server. The client program, hotel-reservation, will contact hotel-api to query the database.