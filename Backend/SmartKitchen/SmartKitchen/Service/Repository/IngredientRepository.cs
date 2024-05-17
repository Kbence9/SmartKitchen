using SmartKitchen.Data;
using SmartKitchen.Model;

namespace SmartKitchen.Service.Repository;

public class IngredientRepository : IIngredientRepository
{
    private readonly SmartKitchenContext _smartKitchenContext;

    public IngredientRepository(SmartKitchenContext smartKitchenContext)
    {
        _smartKitchenContext = smartKitchenContext;
    }
    public Ingredient GetIngredient(string name)
    {
        return _smartKitchenContext.Ingredients.Where(i => i.Name == name)
            .FirstOrDefault();
    }

    public void AddIngredient(Ingredient ingredient)
    {
        _smartKitchenContext.Ingredients.Add(ingredient);
    }

    public void DeleteIngredient(Ingredient ingredient)
    {
        _smartKitchenContext.Ingredients.Remove(ingredient);
    }

    public void UpdateIngredient(Ingredient ingredient)
    {
        throw new NotImplementedException();
    }
}