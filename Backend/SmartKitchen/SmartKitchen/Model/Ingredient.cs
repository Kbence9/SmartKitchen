namespace SmartKitchen.Model;

public class Ingredient
{
    public string Name;
    public float ENERC_KCAL;
    public float PROCNT;
    public float FAT;
    public float CHOCDF;
    public float FIBTG;
    public string? Image;

    public Ingredient()
    {
    }

    public Ingredient(string name, float enercKcal, float procnt, float fat, float chocdf, float fibtg, string? image)
    {
        Name = name;
        ENERC_KCAL = enercKcal;
        PROCNT = procnt;
        FAT = fat;
        CHOCDF = chocdf;
        FIBTG = fibtg;
        Image = image;
    }
}