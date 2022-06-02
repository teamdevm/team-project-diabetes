namespace Diabetes.MVC.Models.Components
{
    public class AddSelectorModel
    {
        public bool IsMealActive { get; set; }
        public bool IsInsulinActive { get; set; }
        public bool IsSugarActive { get; set; }

        public AddSelectorModel(bool isMealActive, bool isInsulinActive, bool isSugarActive)
        {
            IsMealActive = isMealActive;
            IsInsulinActive = isInsulinActive;
            IsSugarActive = isSugarActive;
        }
    }
}