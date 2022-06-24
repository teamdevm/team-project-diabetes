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
        public const string AddKey = "Add";
        public const string EditKey = "Edit";
        
        public static void AddFood(this HttpContext context, FoodForNoteViewModel foodViewModel, string key)
        {
            MealViewModel meal;
            
            if (context.Session.Keys.Contains(key))
            {
                meal = JsonSerializer.Deserialize<MealViewModel>(context.Session.GetString(key));
                meal?.Foods.Add(foodViewModel);
            }
            else
            {
                meal = new MealViewModel();
                meal.Foods.Add(foodViewModel);
            }
            
            context.Session.SetString(key, JsonSerializer.Serialize(meal));
        }
        
        public static void Clear(this HttpContext context, string key)
        {
            context.Session.Remove(key);
        }
        
        public static void RemoveFood(this HttpContext context, Guid foodId, string key)
        {
            if (!context.Session.Keys.Contains(key)) 
                return;

            var meal = JsonSerializer.Deserialize<MealViewModel>(context.Session.GetString(key)) 
                       ?? new MealViewModel();
            meal.Foods = meal.Foods.Where(f => f.Food.Id != foodId).ToList();
            context.Session.SetString(key, JsonSerializer.Serialize(meal));
        }
        
        public static MealViewModel GetMeal(this HttpContext context, string key)
        {
            if (context.Session.Keys.Contains(key))
                return JsonSerializer.Deserialize<MealViewModel>(context.Session.GetString(key));
            
            return new MealViewModel();
        }
        
        public static FoodForNoteViewModel GetFoodById(this HttpContext context, Guid id, string key)
        {
            if (!context.Session.Keys.Contains(key))
                return null;
            
            var meal = JsonSerializer.Deserialize<MealViewModel>(context.Session.GetString(key));
            return meal?.Foods.FirstOrDefault(f => f.FoodId == id);
        }
        
        public static void Edit(this HttpContext context, FoodForNoteViewModel vm, string key)
        {
            if (!context.Session.Keys.Contains(key))
                return;
            
            var meal = JsonSerializer.Deserialize<MealViewModel>(context.Session.GetString(key));

            if (meal == null)
                return;
            
            var food = meal.Foods.FirstOrDefault(f => f.FoodId == vm.FoodId);
            food.MassInGram = vm.MassInGram;
            
            context.Session.SetString(key, JsonSerializer.Serialize(meal));
        }
        
        public static void RememberData(this HttpContext context, MealViewModel vm, string key)
        {
            MealViewModel meal;

            if (context.Session.Keys.Contains(key))
                meal = JsonSerializer.Deserialize<MealViewModel>(context.Session.GetString(key));
            else
                meal = new MealViewModel();

            meal.Value = vm.Value;
            meal.CreatingDate = vm.CreatingDate;
            meal.CreatingTime = vm.CreatingTime;
            meal.Comment = vm.Comment;
                
            context.Session.SetString(key, JsonSerializer.Serialize(meal));
        }
        
        public static void AddMeal(this HttpContext context, MealViewModel vm, string key)
        {
            context.Session.SetString(key, JsonSerializer.Serialize(vm));
        }
    }
}