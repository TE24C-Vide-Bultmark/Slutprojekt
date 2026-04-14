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
    Toolbox.DisplayBuildingOptions(buildingOptions);

    Console.WriteLine();
    Toolbox.DisplayResources(resources, people, day);

    Console.WriteLine();
    Toolbox.DisplayWork(people, buildings);    

    Console.WriteLine("\n\n--------------------------------------------------------------------------------");
    Console.WriteLine("1 - show graveyard");
    Console.WriteLine("2 - change currently building");
    Console.WriteLine("3 - manage city");
    Console.WriteLine("4 - go to next day");
    string input = Console.ReadLine();

    if (input == "1")
    {
        
    }

    // spelaren får möjlighet att byta vilken byggnad som byggs
    if (Toolbox.SwitchBuilding(buildingOptions, graveyard))
    {
        Toolbox.NewDay(day, resources, buildings, people, buildingOptions, graveyard, technologytree);
    }
}