// skapar variabler som används i spelet
// jag använder listor istället för arrayer då jag vill att programmet ska kunna lägga till objekt i de under programmets gång, detta gäller för alla listor i programmet

// resurserna i spelet

Resource food = new Resource() { name = "Food" };
Resource wood = new Resource() { name = "Wood" };
Resource science = new Resource() { name = "Science" };
Resource stone = new Resource() { name = "Stone" };
Resource ore = new Resource() { name = "Ore" };
Resource metal = new Resource() { name = "metal" };
List<Resource> resources = [food, wood, science];

// byggnaderna i spelet
Building farm = new Building() { name = "Farm", costResource = wood, production = new() {{food, 3}}};
Building carpentry = new Building() { name = "Carpentry", costAmount = 5, costResource = wood , production = new() {{wood, 1}}};
List<Building> buildingOptions = [farm, carpentry];
Building bigFarm = new Building() { name = "Big Farm", costAmount = 30, costResource = stone, production = new() {{food, 10}} };
Building library = new Building() { name = "Library", costAmount = 3, costResource = wood, production = new() {{science, 1}} };
Building quarry = new Building() { name = "Quarry", costAmount = 15, costResource = wood, production = new() {{stone, 1}} };
Building sawmill = new Building() { name = "Sawmill", costAmount = 10, costResource = wood, production = new() {{wood, 2}} };
Building badMine = new Building() { name = "Bad Mine", costAmount = 40, costResource = wood, production = new() {{ore, 1}} };
Building goodMine = new Building() { name = "Good Mine", costAmount = 15, costResource = stone, production = new() {{ore, 1}, {stone, 1}} };
Building forge = new Building() {};
List<List<Building>> technologytree = [];
List<Building> technologies0 = [library, quarry, quarry, sawmill];
technologytree.Add(technologies0);
List<Building> technologies1 = [bigFarm, library, quarry, badMine];
technologytree.Add(technologies1);
List<Building> technologies2 = [bigFarm, bigFarm, badMine, goodMine];
technologytree.Add(technologies2);
List<Building> techOptions = [];

List<string> people = [];

List<Building> buildings = [farm, carpentry];

string cityname = Toolbox.Intro(people);
int day = 1;

while (true)
{
    // skriver upp display
    Console.Clear();

    Console.WriteLine($"City name: {cityname}\n");
    Toolbox.DisplayBuildqueue(buildingOptions);

    Console.WriteLine();
    Toolbox.DisplayResources(resources, people, day);

    Console.WriteLine();
    Toolbox.DisplayWork(people, buildings);

    Console.WriteLine("\n\n--------------------------------------------------------------------------------");
    Console.WriteLine("Press Enter to go to the next day");
    Console.WriteLine("Enter the number next to the building you want to build to start building it");

    // spelaren får möjlighet att byta vilken byggnad som byggs
    if (Toolbox.SwitchBuilding(buildingOptions))
    {
        day++;
        Toolbox.Produce(resources, buildings, people);
        Toolbox.BuildingWork(people, buildings, buildingOptions);
        Toolbox.PopulationGrowth(food, people);
        // om spelaren har tillräckligt med forskning får de välja mellan 2 nya teknologier
        if (science.amount >= 50)
        {
            for (int i = 0; i < technologytree.Count; i++)
            {
                if (technologytree[i].Count>1)
                {
                    Toolbox.Research(techOptions, technologytree[i], buildingOptions, science, resources);
                    break;
                }
            }
        }
    }
}