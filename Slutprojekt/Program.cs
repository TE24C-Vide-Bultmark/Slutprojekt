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

string cityname = Toolbox.Intro(people);
int day = 1;

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

    Console.WriteLine();
    // skriver ut kyrkogård
    Console.WriteLine("graveyard:");
    for (int i = 0; i < graveyard.Count; i++)
    {
        // Console.WriteLine(graveyard[i]);
    }

    Console.WriteLine("\n\n--------------------------------------------------------------------------------");
    Console.WriteLine("Press Enter to go to the next day");
    Console.WriteLine("Enter the number next to the building you want to build");

    // spelaren får möjlighet att byta vilken byggnad som byggs
    if (Toolbox.SwitchBuilding(buildingOptions))
    {
        day++;
        Toolbox.Produce(resources, buildings, people);
        Toolbox.BuildingWork(people, buildings, buildingOptions);
        Toolbox.PopulationGrowth(Resource.food, people);
        // personer dör
        for (int iteration = 0; iteration > people.Count; iteration++)
        {
            // slumpar ett tal mellan från och med 0 till och med 99
            if (0 == Random.Shared.Next(100))
            {
                // dödar personen om deu rullar 0
                graveyard.Add(people[iteration]);
                people.RemoveAt(iteration);
            }
        }
        // om spelaren har tillräckligt med forskning får de välja mellan 2 nya teknologier
        if (Resource.science.amount >= 50)
        {
            for (int i = 0; i < technologytree.Count; i++)
            {
                if (technologytree[i].Count > 2)
                {
                    Toolbox.Research(technologytree[i], buildingOptions, Resource.science, resources);
                    break;
                }
            }
        }
    }
}