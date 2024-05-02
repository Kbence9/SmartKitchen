namespace SmartKitchen.Model;

public class Meal
{
    public string Name;
    public ICollection<Ingredient> Ingredients;
    public Difficulty PreparationDifficulty;
    public float ENERC_KCAL;
    public float PROCNT;
    public float FAT;
    public float CHOCDF;
    public float FIBTG;
    public string Image;
    public string Video;
}