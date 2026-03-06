// skapar variabler som används i spelet
// jag använder listor istället för arrayer då jag vill att programmet lägger till objekt under programmets gång
Resource food = new Resource() { name = "Food" };
Resource wood = new Resource() { name = "Wood" };
List<Resource> resources = [food, wood];

Building farm = new Building() { name = "Farm", productionAmount = 1, productionResource = food };
Building carpentry = new Building() { name = "Carpentry", productionAmount = 1, productionResource = wood };
List<Building> buildqueue = [farm, carpentry];

List<string> people = [];

List<Building> buildings = [farm, carpentry];

string cityname = Toolbox.Intro(people);
int day = 1;

while (true)
{
    // skriver upp display
    Console.Clear();

    Console.WriteLine(cityname + "\n");
    Toolbox.DisplayBuildqueue(buildqueue);

    Console.WriteLine();
    Toolbox.DisplayResources(resources, people, day);

    Console.WriteLine();
    Toolbox.DisplayWork(people, buildings);

    // spelaren får möjlighet att byta vilken byggnad som byggs
    if (Toolbox.SwitchBuilding(buildqueue))
    {
        day++;
        Toolbox.BuildingWork(wood, people, buildings, buildqueue);
        Toolbox.Produce(resources, buildings);
        Toolbox.PopulationGrowth(food, people);
    }
}