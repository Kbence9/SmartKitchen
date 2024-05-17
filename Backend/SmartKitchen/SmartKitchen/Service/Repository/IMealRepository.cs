using SmartKitchen.Model;

namespace SmartKitchen.Service.Repository;

public interface IMealRepository
{
    Meal GetMeal(string name);
    void AddMeal(Meal meal);
    void DeleteMeal(Meal meal);
    void UpdateMeal(Meal meal);
    List<Meal> GetAllMeal();
}