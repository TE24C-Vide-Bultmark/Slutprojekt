// skapar variabler som används i spelet
// jag använder listor istället för arrayer då jag vill att programmet ska kunna lägga till objekt i de under programmets gång, detta gäller för alla listor i programmet
Resource food = new Resource() { name = "Food" };
Resource wood = new Resource() { name = "Wood" };
Resource science = new Resource() { name = "Science"};
List<Resource> resources = [food, wood, science];

Building farm = new Building() { name = "Farm", productionAmount = 3, productionResource = food };
Building carpentry = new Building() { name = "Carpentry", productionAmount = 1, productionResource = wood };
List<Building> buildingOptions = [farm, carpentry];

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
        Toolbox.BuildingWork(wood, people, buildings, buildingOptions);
        Toolbox.Produce(resources, buildings, people);
        Toolbox.PopulationGrowth(food, people);
    }
}