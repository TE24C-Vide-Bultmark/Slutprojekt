// skapar variabler som används i spelet
// jag använder listor istället för arrayer då jag vill att programmet ska kunna lägga till objekt i de under programmets gång
// lista med de reurser du har tillgång till
List<Resource> resources = [Resource.food, Resource.wood, Resource.science];

// teknologier, det ska kunna försvinna byggnader från dessa, därav måste de vara listor istället för arrayer
List<Building> buildingOptions = [Building.farm, Building.carpentry];
List<List<Building>> technologytree = Toolbox.GenerateTechnologyTree();

// lista som lagrar alla personer i din stad samt en kyrkogård
List<string> people = [];
List<string> graveyard = [];

// lista med alla byggnader du har byggt
List<Building> buildings = [Building.farm, Building.carpentry];

int day = 1;
string cityname = Toolbox.Intro(people, day);

while (true)
{
    // skriver upp display
    Console.Clear();

    Console.WriteLine($"City name: {cityname}\n");
    Console.WriteLine($"Currently \u001b[38;5;208mbuilding\u001b[0m: {buildingOptions[0].name} ({buildingOptions[0].progress}/{buildingOptions[0].costAmount} {buildingOptions[0].costResource.name})");

    Console.WriteLine();
    Toolbox.DisplayResources(resources, people, day);

    Console.WriteLine();
    Toolbox.DisplayWork(people, buildings);

    Console.WriteLine("\n--------------------------------------------------------------------------------");
    Console.WriteLine("enter the number to the left of the building you want to switch");
    Console.WriteLine("g - show graveyard");
    Console.WriteLine("h - change currently building");
    Console.WriteLine("any - go to next day");

    // spelaren får möjlighet att byta vilken byggnad som byggs
    if (Toolbox.ReadInput(buildingOptions, graveyard, buildings))
    {
        day++;
        Toolbox.NewDay(day, resources, buildings, people, buildingOptions, graveyard, technologytree);
    }
}