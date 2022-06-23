using System;
using System.Linq;
using System.Text.Json;
using Diabetes.MVC.Models.FoodForNote;
using Diabetes.MVC.Models.Meal;
using Microsoft.AspNetCore.Http;

namespace Diabetes.MVC.Extensions
{
    public static class HttpContextExtensions
    {
        private const string AddFoodKey = "food";
        
        public static void AddFood(this HttpContext context, FoodForNoteViewModel foodViewModel)
        {
            MealViewModel meal;
            
            if (context.Session.Keys.Contains(AddFoodKey))
            {
                meal = JsonSerializer.Deserialize<MealViewModel>(context.Session.GetString(AddFoodKey));
                meal?.Foods.Add(foodViewModel);
            }
            else
            {
                meal = new MealViewModel();
                meal.Foods.Add(foodViewModel);
            }
            
            context.Session.SetString(AddFoodKey, JsonSerializer.Serialize(meal));
        }
        
        public static void Clear(this HttpContext context)
        {
            context.Session.Remove(AddFoodKey);
        }
        
        public static void RemoveFood(this HttpContext context, Guid foodId)
        {
            if (!context.Session.Keys.Contains(AddFoodKey)) 
                return;

            var meal = JsonSerializer.Deserialize<MealViewModel>(context.Session.GetString(AddFoodKey)) 
                       ?? new MealViewModel();
            meal.Foods = meal.Foods.Where(f => f.Food.Id != foodId).ToList();
            context.Session.SetString(AddFoodKey, JsonSerializer.Serialize(meal));
        }
        
        public static MealViewModel GetMeal(this HttpContext context)
        {
            if (context.Session.Keys.Contains(AddFoodKey))
                return JsonSerializer.Deserialize<MealViewModel>(context.Session.GetString(AddFoodKey));
            
            return new MealViewModel();
        }
        
        public static FoodForNoteViewModel GetFoodById(this HttpContext context, Guid id)
        {
            if (!context.Session.Keys.Contains(AddFoodKey))
                return null;
            
            var meal = JsonSerializer.Deserialize<MealViewModel>(context.Session.GetString(AddFoodKey));
            return meal?.Foods.FirstOrDefault(f => f.FoodId == id);
        }
        
        public static void Edit(this HttpContext context, FoodForNoteViewModel vm)
        {
            if (!context.Session.Keys.Contains(AddFoodKey))
                return;
            
            var meal = JsonSerializer.Deserialize<MealViewModel>(context.Session.GetString(AddFoodKey));

            if (meal == null)
                return;
            
            var food = meal.Foods.FirstOrDefault(f => f.FoodId == vm.FoodId);
            food.MassInGram = vm.MassInGram;
            
            context.Session.SetString(AddFoodKey, JsonSerializer.Serialize(meal));
        }
        
        public static void RememberData(this HttpContext context, MealViewModel vm)
        {
            MealViewModel meal;

            if (context.Session.Keys.Contains(AddFoodKey))
                meal = JsonSerializer.Deserialize<MealViewModel>(context.Session.GetString(AddFoodKey));
            else
                meal = new MealViewModel();

            meal.Value = vm.Value;
            meal.CreatingDate = vm.CreatingDate;
            meal.CreatingTime = vm.CreatingTime;
            meal.Comment = vm.Comment;
                
            context.Session.SetString(AddFoodKey, JsonSerializer.Serialize(meal));
        }
    }
}