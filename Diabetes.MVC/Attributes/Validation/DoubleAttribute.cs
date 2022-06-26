using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Diabetes.MVC.Attributes.Validation
{
    public class DoubleAttribute:ValidationAttribute
    {
        public static Regex regex = new Regex("^[0-9]([0-9]{0,2})?((\\,|\\.)[0-9]{1,2})?$");
        private int _min;
        private int _max;

        public DoubleAttribute(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public override bool IsValid(object value)
        {
            return value != null && Validate(value.ToString());
        }

        private bool Validate(string value)
        {
            var match = regex.IsMatch(value);
            value = value.Replace(',', '.');
            var parse = double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture , out var res);
            return  match && 
                    parse &&
                    res >= _min && res <= _max;
        }
    }
}