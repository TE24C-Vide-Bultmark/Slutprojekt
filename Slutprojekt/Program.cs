// skapar alla variabler som används i spelet
Building farm = new Building() {name = "Farm"};
Building carpentry = new Building() {name = "Carpentry"};
List<Building> buildqueue = [farm, carpentry];

Resource food = new Resource() {name = "Food"};
Resource wood = new Resource() {name = "Wood"};
List<Resource> resources = [food, wood];

List<string> people = [];

Building cityCenter = new Building() {name = "City Center"};
List<Building> buildings = [cityCenter];

string cityname = Toolbox.Intro(people);

while (true)
{
    // skriver upp display
    Console.Clear();

    Console.WriteLine(cityname + "\n");
    Toolbox.DisplayBuildqueue(buildqueue);

    Console.WriteLine();
    Toolbox.DisplayResources(resources, people);

    Console.WriteLine();
    Toolbox.DisplayWork(people, buildings);

    // spelaren får möjlighet att byta vilken byggnad som byggs
    bool pass = Toolbox.SwitchBuilding(buildqueue);
}
