using System;
using System.ComponentModel.DataAnnotations;

namespace Diabetes.MVC.Attributes.Validation
{
    public class BirthDateAttribute:ValidationAttribute
    {
        private int _minMonths;
        private int _maxYears;

        public BirthDateAttribute(int minMonths, int maxYears)
        {
            _minMonths = minMonths;
            _maxYears = maxYears;
        }

        public override bool IsValid(object value)
        {
            return value is DateTime time && Validate(time);
        }

        private bool Validate(DateTime value)
        {
            return  DateTime.Now.AddYears(-_maxYears) < value 
                    && DateTime.Now.AddMonths(-_minMonths) > value;
        }
    }
}