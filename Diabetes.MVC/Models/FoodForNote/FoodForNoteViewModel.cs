using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Diabetes.Domain;
using Diabetes.Domain.Enums;
using Diabetes.MVC.Attributes.Validation;

namespace Diabetes.MVC.Models.FoodForNote
{
    public class FoodForNoteViewModel
    {
        [Required(ErrorMessage ="Обязательное поле")]
        [Display(Name = "Масса в граммах")]
        [Double(1, 5000, ErrorMessage = "Число должно быть от 1 до 5000 и не более чем с двумя знаками после запятой")]

        public string MassInGram {
            get => _massInGram?.Replace(',','.');
            set => _massInGram = value;
        }

        private string _massInGram = "0";
        
        public double MassInGramNum
        {
            get
            { 
                var success = double.TryParse(MassInGram,  NumberStyles.Float, CultureInfo.InvariantCulture, out var result);
                return success ? result : default;
            }
        }

        public Guid FoodId { get; set; }
        public Food Food { get; set; }
        
        public string Kcal => $"{Food?.Kcal / 100.0 * MassInGramNum:f2}";
        public string Fat => $"{Food?.Fat / 100.0 * MassInGramNum:f2}";
        public string Protein => $"{Food?.Protein / 100.0 * MassInGramNum:f2}";
        public string Carbohydrate => $"{Food?.Carbohydrate / 100.0 * MassInGramNum:f2}";
    }
}