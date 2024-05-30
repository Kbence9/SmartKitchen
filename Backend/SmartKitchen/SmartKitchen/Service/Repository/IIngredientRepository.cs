using SmartKitchen.Model;

namespace SmartKitchen.Service.Repository;

public interface IIngredientRepository
{
    Ingredient GetIngredient(string name);
    void AddIngredient(Ingredient ingredient);
    void DeleteIngredient(Ingredient ingredient);
    void UpdateIngredient(Ingredient ingredient);
    
}