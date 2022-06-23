using System;
using System.ComponentModel.DataAnnotations;
using Diabetes.Domain;
using Diabetes.Domain.Enums;

namespace Diabetes.MVC.Models.FoodForNote
{
    public class FoodForNoteViewModel
    {
        [Required(ErrorMessage ="Обязательное поле")]
        [Display(Name = "Масса")]
        public int MassInGram { get; set; }
        public Guid FoodId { get; set; }
        public Food Food { get; set; }
        
        public string Kcal => $"{Food?.Kcal / 100.0 * MassInGram:f2}";
        public string Fat => $"{Food?.Fat / 100.0 * MassInGram:f2}";
        public string Protein => $"{Food?.Protein / 100.0 * MassInGram:f2}";
        public string Carbohydrate => $"{Food?.Carbohydrate / 100.0 * MassInGram:f2}";
    }
}