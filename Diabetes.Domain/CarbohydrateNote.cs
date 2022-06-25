using Diabetes.Domain.Normalized;

namespace Diabetes.Domain
{
    public class CarbohydrateNote:Note
    {
        public Meal Meal { get; set; }
        
        public override ActionHistoryItem ToHistoryItem()
        {
            throw new System.NotImplementedException();
        }
    }
}