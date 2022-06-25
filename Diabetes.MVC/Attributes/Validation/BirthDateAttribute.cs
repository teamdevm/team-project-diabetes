using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Diabetes.MVC.Attributes.Validation
{
    public class BirthDateAttribute:ValidationAttribute
    {
        private int _minMonths;
        private int _maxYears;

        private const int DaysInMonth = 30;
        private const int DaysInYear = 365;

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
            var dateDelta = DateTime.Now - value;
            return  dateDelta.Days > DaysInMonth * _minMonths 
                    && dateDelta.Days < DaysInYear * _maxYears;

        }
    }
}