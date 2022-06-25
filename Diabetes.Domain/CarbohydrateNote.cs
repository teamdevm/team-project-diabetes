using System;
using System.Collections.Generic;
using System.Linq;
using Diabetes.Domain.Enums;
using Diabetes.Domain.Normalized;

namespace Diabetes.Domain
{
    public class CarbohydrateNote:Note
    {
        public List<FoodPortion> Portions { get; set; } = new();
        public List<Food> Foods { get; set; } = new();

        private double PortionsCarbohydrates => Portions.Select(p => p.Food.Carbohydrate / 100.0 * p.MassInGr).Sum();

        public override ActionHistoryItem ToHistoryItem()
        {
            return new ActionHistoryItem
            {
                Id = Id,
                Type = NoteType.Carbohydrate,
                Title = NoteType.Carbohydrate.ToLocalizedString(),
                Value = $"{Value + PortionsCarbohydrates:f2}",
                DateTime = MeasuringDateTime,
                CreationDateTime = LastUpdate,
                Details = $"Добавлено продуктов: {Portions.Count}"
            };
        }
    }
}