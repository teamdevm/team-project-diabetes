using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diabetes.Domain
{
    public class Food
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Kcal { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbohydrate { get; set; }
        public string Details { get; set; }
        public string Discriminator { get; private set; }

        public List<FoodPortion> Portions { get; set; } = new();
        public List<Meal> Meals { get; set; } = new();
    }
}