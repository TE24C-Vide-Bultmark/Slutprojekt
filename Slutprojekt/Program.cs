// skapar variabler som används i spelet
// jag använder listor istället för arrayer då jag vill att programmet ska kunna lägga till objekt i de under programmets gång, detta gäller för alla listor i programmet

// resurserna i spelet
Resource food = new Resource() { name = "Food" };
Resource wood = new Resource() { name = "Wood" };
Resource science = new Resource() { name = "Science" };
List<Resource> resources = [food, wood, science];
Resource stone = new Resource() { name = "Stone" };

// byggnaderna i spelet
Building farm = new Building() { name = "Farm", productionAmount = 3, productionResource = food, costResource = wood };
Building carpentry = new Building() { name = "Carpentry", productionAmount = 1, productionResource = wood, costAmount = 5, costResource = wood };
List<Building> buildingOptions = [farm, carpentry];
Building bigFarm = new Building() { name = "Big Farm", productionAmount = 10, productionResource = food, costAmount = 40, costResource = stone };
Building library = new Building() { name = "Library", productionAmount = 1, productionResource = science, costAmount = 3, costResource = wood };
Building quarry = new Building() { name = "Quarry", productionAmount = 1, productionResource = stone, costAmount = 15, costResource = wood };
List<Building> technologies = [bigFarm, library, quarry, quarry];
List<Building> techOptions = [];

List<string> people = [];

List<Building> buildings = [farm, carpentry];

string cityname = Toolbox.Intro(people);
int day = 1;

while (true)
{
    // skriver upp display
    Console.Clear();

    Console.WriteLine(cityname + "\n");
    Toolbox.DisplayBuildqueue(buildingOptions);

    Console.WriteLine();
    Toolbox.DisplayResources(resources, people, day);

    Console.WriteLine();
    Toolbox.DisplayWork(people, buildings);

    // spelaren får möjlighet att byta vilken byggnad som byggs
    if (Toolbox.SwitchBuilding(buildingOptions))
    {
        day++;
        Toolbox.Produce(resources, buildings, people);
        Toolbox.BuildingWork(people, buildings, buildingOptions);
        Toolbox.PopulationGrowth(food, people);
        // om spelaren har tillräckligt med forskning får de välja mellan 2 nya teknologier
        if (science.amount >= 50 && technologies.Count > 1)
        {
            Console.Clear();
            Console.WriteLine("Congartualions your city discovered a new technology!");
            for (int i = 0; i < 2; i++)
            {
                int random = Random.Shared.Next(technologies.Count);
                techOptions.Add(technologies[random]);
                technologies.RemoveAt(random);
                Console.WriteLine(i + 1 + ") " + techOptions[i].name);
            }
            int choice = 0;
            while (choice < 1 || choice > techOptions.Count)
            {
                Console.WriteLine("Enter the number to the left of the tech you want to research");
                int.TryParse(Console.ReadLine(), out choice);
            }
            buildingOptions.Add(techOptions[choice - 1]);
            science.amount = 0;
            int itertion = 0;
            while (itertion < resources.Count)
            {
                if (resources[itertion] == techOptions[choice - 1].productionResource)
                {
                    break;
                }
                else if (itertion == resources.Count-1)
                {
                    resources.Add(techOptions[choice - 1].productionResource);
                }
                itertion++;
            }
        }
    }
}