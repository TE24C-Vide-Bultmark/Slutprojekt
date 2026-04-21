public class Building
{
    public string name = "missing";
    public Dictionary<Resource, int> production = new();
    public int costAmount = 10;
    public Resource costResource;
    public int progress = 0;

    // byggnaderna i spelet
    public static Building farm = new Building() { name = "Farm", costResource = Resource.wood, production = new() { { Resource.food, 3 } } };
    public static Building carpentry = new Building() { name = "Carpentry", costAmount = 5, costResource = Resource.wood, production = new() { { Resource.wood, 1 } } };
    public static Building bigFarm = new Building() { name = "Big Farm", costAmount = 30, costResource = Resource.stone, production = new() { { Resource.food, 10 } } };
    public static Building library = new Building() { name = "Library", costAmount = 3, costResource = Resource.wood, production = new() { { Resource.science, 1 } } };
    public static Building quarry = new Building() { name = "Quarry", costAmount = 15, costResource = Resource.wood, production = new() { { Resource.stone, 1 } } };
    public static Building sawmill = new Building() { name = "Sawmill", costAmount = 10, costResource = Resource.wood, production = new() { { Resource.wood, 2 } } };
    public static Building badMine = new Building() { name = "Bad Mine", costAmount = 40, costResource = Resource.wood, production = new() { { Resource.ore, 1 } } };
    public static Building goodMine = new Building() { name = "Good Mine", costAmount = 15, costResource = Resource.stone, production = new() { { Resource.ore, 1 }, { Resource.stone, 1 } } };
    public static Building forge = new Building() { name = "Forge", costAmount = 25, costResource = Resource.stone, production = new() { {Resource.wood, -1 }, { Resource.ore, -1 }, { Resource.metal, 1 } } };
    public static Building engine = new Building() { name = "Engine", costAmount = 50, costResource = Resource.metal, production = new() { { Resource.wood, -3}, { Resource.power, 1} } };
    public static Building factory = new Building() { name = "Factory", costAmount = 100, costResource = Resource.metal, production = new() { { Resource.power, -3}, { Resource.ore, -10}, { Resource.metal, 10} } };
    public static Building particleAccelerator = new Building() { name = "Particle Accelerator", costAmount = 1000, costResource = Resource.metal, production = new() { { Resource.power, -100}, { Resource.science, (int)((float)Math.PI*1000000)} } };
    
}