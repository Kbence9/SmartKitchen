using Microsoft.EntityFrameworkCore;
using SmartKitchen.Data;
using SmartKitchen.Model;

namespace SmartKitchen.Service.Repository;

public class MealRepository : IMealRepository
{
    private readonly SmartKitchenContext _smartKitchenContext;
    
    public MealRepository(SmartKitchenContext smartKitchenContext)
    {
        _smartKitchenContext = smartKitchenContext;
    }
    public Meal GetMeal(string name)
    {
        return _smartKitchenContext.Meals.Where(m => m.Name == name)
            .Include(m => m.Ingredients)
            .FirstOrDefault();
    }

    public void AddMeal(Meal meal)
    {
        _smartKitchenContext.Meals.Add(meal);
    }

    public void DeleteMeal(Meal meal)
    {
        _smartKitchenContext.Meals.Remove(meal);
    }

    public void UpdateMeal(Meal meal)
    {
        _smartKitchenContext.Update(meal);
    }

    public List<Meal> GetAllMeal()
    {
        return _smartKitchenContext.Meals.ToList();
    }
}