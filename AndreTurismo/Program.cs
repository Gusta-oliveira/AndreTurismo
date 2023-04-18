// See https://aka.ms/new-console-template for more information
using System.Runtime.ConstrainedExecution;
using AndreTurismo.Controllers;
using AndreTurismo.Models;

Console.WriteLine("Hello, World!");

Hotel hotel = new()
{
    Name = "Hotel Paraiso",
    Address = new Address() { Street = "Avinida Das Flores",
                              Number = 123,
                              Neighborhood = "Jd Primavera",
                              CEP = "14807123",
                              
    }
};
