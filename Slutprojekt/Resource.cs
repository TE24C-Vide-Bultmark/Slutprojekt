public class Resource
{
    public string name = "missing";
    public int amount = 0;

    // resurserna i spelet
    public static Resource food = new() { name = "food" };
    public static Resource wood = new Resource() { name = "Wood" };
    public static Resource science = new Resource() { name = "Science" };
    public static Resource stone = new Resource() { name = "Stone" };
    public static Resource ore = new Resource() { name = "Ore" };
    public static Resource metal = new Resource() { name = "Metal" };
    public static Resource power = new Resource() { name = "Power" };
}