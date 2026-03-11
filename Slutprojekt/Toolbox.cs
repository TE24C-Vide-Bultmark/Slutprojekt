using System.Runtime.Versioning;
using System.Xml;

public class Toolbox
{
    // introducerar spelet och låter spelaren namnge staden samt grundaren
    public static string Intro(List<string> people)
    {
        Console.WriteLine("Hello and welcome to my game!");
        Console.WriteLine("In this game you manage a city and watch it grow.");
        Console.WriteLine("Please choose a name for your city.");
        string cityname = Console.ReadLine();
        Console.WriteLine($"And who are the two founders of {cityname}?");
        Console.Write("Farmer: ");
        people.Add(Console.ReadLine());
        Console.Write("Carpenter: ");
        people.Add(Console.ReadLine());
        return cityname;
    }





    // skriver upp namn på staden och de byggnader du kan bygga
    public static void DisplayBuildqueue(List<Building> buildqueue)
    {
        Console.WriteLine($"building - {buildqueue[0].name} ({buildqueue[0].progress}/{buildqueue[0].costAmount} {buildqueue[0].costResource.name})");

        int iteration = 1;
        while (iteration < buildqueue.Count)
        {
            Console.WriteLine($"{iteration}) {buildqueue[iteration].name} ({buildqueue[iteration].progress}/{buildqueue[iteration].costAmount} {buildqueue[iteration].costResource.name})");
            iteration++;
        }

    }





    // skriver upp resurserna
    public static void DisplayResources(List<Resource> resources, List<string> people, int day)
    {
        Console.WriteLine("day " + day);
        Console.WriteLine("food needed until next person: " + people.Count * people.Count);
        int iteration = 0;
        while (iteration < resources.Count)
        {
            Console.WriteLine(resources[iteration].name + ": " + resources[iteration].amount);
            iteration++;
        }
    }





    // skriver upp personer och arbetsuppgifter
    public static void DisplayWork(List<string> people, List<Building> buildings)
    {
        int iteration = people.Count - 1;
        while (iteration >= 0)
        {
            Console.Write(people[iteration]);
            if (iteration >= buildings.Count)
            {
                Console.WriteLine(" - building");
            }
            else
            {
                Console.WriteLine(" - " + buildings[iteration].name);
            }
            iteration--;
        }
    }





    // byter plats på vald byggnad och byggnaden under konstruktion, om spelaren skrev in input som inte korresponderar med en byggnad går vi över till nästa dag
    public static bool SwitchBuilding(List<Building> buildqueue)
    {
        int input;
        int.TryParse(Console.ReadLine(), out input);
        if (buildqueue.Count > input && input > 0)
        {
            Building temp = buildqueue[input];
            buildqueue[input] = buildqueue[0];
            buildqueue[0] = temp;
            return false;
        }
        else return true;
    }





    public static void Produce(List<Resource> resources, List<Building> buildings, List<string> people)
    {
        // loop som går igenom varje resurs
        int iterationResource = 0;
        while (iterationResource < resources.Count)
        {
            int iterationBuilding = 0;
            while (iterationBuilding < buildings.Count)
            {
                // kollar efter byggnader som producerar en viss resurs och ökar produktionen av den resursen med så mycket byggnaden producerar
                if (buildings[iterationBuilding].productionResource == resources[iterationResource])
                {
                    resources[iterationResource].amount += buildings[iterationBuilding].productionAmount;
                }
                iterationBuilding++;
            }
            iterationResource++;
        }
        // produktion ej baserat på byggnader
        resources[0].amount -= people.Count;
        resources[2].amount += people.Count;
    }





    public static void PopulationGrowth(Resource food, List<string> people)
    {
        // ökar population
        if (food.amount >= people.Count * people.Count)
        {
            food.amount = 0;
            Console.Clear();
            Console.WriteLine("congratulations your a new member of your city has appeared!");
            string name = "";
            while (name.Length < 1 || name.Length > 20)
            {
                Console.WriteLine("please choose a name for the member. Name must be between 1 and 20 characters");
                name = Console.ReadLine();
            }
            people.Add(name);
        }
    }





    public static void BuildingWork(List<string> people, List<Building> buildings, List<Building> buildqueue)
    {
        // bygger på byggnad
        if (buildqueue[0].costResource.amount > people.Count - buildings.Count)
        {
            buildqueue[0].progress += people.Count - buildings.Count;
            buildqueue[0].costResource.amount -= people.Count - buildings.Count;
        }
        else
        {
            buildqueue[0].progress += buildqueue[0].costResource.amount;
            buildqueue[0].costResource.amount = 0;
        }
        // lägger till byggnaden om den är färdig
        if (buildqueue[0].progress >= buildqueue[0].costAmount)
        {
            buildqueue[0].progress = 0;
            buildings.Add(buildqueue[0]);
        }
    }





    public static void Research(List<Building> techOptions, List<Building> technologies, List<Building> buildingOptions, Resource science, List<Resource> resources)
    {
        techOptions = [];
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
        AddResource(resources, techOptions, choice);
    }





    public static void AddResource(List<Resource> resources, List<Building> techOptions, int choice)
    {
        int itertion = 0;
        while (itertion < resources.Count)
        {
            if (resources[itertion] == techOptions[choice - 1].productionResource)
            {
                break;
            }
            else if (itertion == resources.Count - 1)
            {
                resources.Add(techOptions[choice - 1].productionResource);
            }
            itertion++;
        }
    }
}