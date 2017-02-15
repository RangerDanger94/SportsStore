﻿using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();

            if(!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Kayak",
                        Description = "A boat for one person.",
                        Category = "Watersports",
                        Price = 275M
                    },
                    new Product
                    {
                        Name = "Lifejacket",
                        Description = "Protective and fashionable.",
                        Category = "Watersports",
                        Price = 48.95M
                    },
                    new Product
                    {
                        Name = "Soccer Ball",
                        Description = "FIFA-approved size and weight.",
                        Category = "Soccer",
                        Price = 19.50M
                    },
                    new Product
                    {
                        Name = "Corner Flags",
                        Description = "Give your playing field a professional touch.",
                        Category = "Soccer", 
                        Price = 34.95M
                    },
                    new Product
                    {
                        Name = "Stadium",
                        Description = "Flat-packed 35,000 seat stadium.",
                        Category = "Soccer",
                        Price = 79500M
                    },
                    new Product
                    {
                        Name = "Thinking Cap",
                        Description = "Improve your brain efficiency by 75%.",
                        Category = "Chess",
                        Price = 16M
                    },
                    new Product
                    {
                        Name = "Unsteady Chair",
                        Description = "Secretly give your opponent a disadvantage.",
                        Category = "Chess",
                        Price = 29.95M
                    },
                    new Product
                    {
                        Name = "Human Chess Board",
                        Description = "A fun game for the family",
                        Category = "Chess",
                        Price = 75M
                    },
                    new Product
                    {
                        Name = "Bling-Bling King",
                        Description = "Gold-plated, diamond-studded King",
                        Category = "Chess",
                        Price = 1200M
                    }
                );

                context.SaveChanges();
            }
        }
    }
}