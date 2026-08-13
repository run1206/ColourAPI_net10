using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ColourAPI_net10.Models;

public static class PrepDB
{
    public static void PrepColours(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        SeedData(serviceScope.ServiceProvider.GetService<ColourContext>()!);
    }

    public static void SeedData(ColourContext? context)
    {
        if (context is null)
            throw new InvalidOperationException("ColourContext is null.");

        Console.WriteLine($"Applying Migrations...");

        context.Database.Migrate();

        if (!context.ColourItems.Any())
        {
            Console.WriteLine("Adding data - seeding...");
            context.ColourItems.AddRange(
                new Colour() {ColourName = "Red"},
                new Colour() {ColourName = "Yellow"},
                new Colour() {ColourName = "Orange"},
                new Colour() {ColourName = "Green"},
                new Colour() {ColourName = "Blue"},
                new Colour() {ColourName = "Indigo"},
                new Colour() {ColourName = "Violet"}
            );
        }
        else
            Console.WriteLine("Data already present, not seeding");
    }
}
