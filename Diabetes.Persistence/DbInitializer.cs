using System;
using System.Collections.Generic;
using System.Linq;
using Diabetes.Domain;
using Diabetes.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(DataDbContext context)
        {
            context.Database.EnsureCreated();

            var items = new List<Food>
            {
                new Food
                {
                    Id = Guid.NewGuid(),
                    Name = "Яблоко",
                    Carbohydrate = 10,
                    Fat = 10,
                    Kcal = 10,
                    Protein = 10,
                    Details = "Небольшое описание"

                },

                new Food
                {
                    Id = Guid.NewGuid(),
                    Name = "Хлеб",
                    Carbohydrate = 5,
                    Fat = 5,
                    Kcal = 5,
                    Protein = 5,
                    Details = "Описание весьма среднего размера"

                },

                new Food
                {
                    Id = Guid.NewGuid(),
                    Name = "Молоко",
                    Carbohydrate = 15,
                    Fat = 15,
                    Kcal = 15,
                    Protein = 15,
                    Details =
                        "Описание гигантского, колоссального объёма. Длина такая огромная, что требуется много строчек."

                }
            };

            if (!context.Foods.Any())
            {
                context.Foods.AddRange(items);
                context.SaveChanges();
            }
        }
    }
}