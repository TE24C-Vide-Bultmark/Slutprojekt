using System.Runtime.Versioning;

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
        people.Add(Console.ReadLine());
        people.Add(Console.ReadLine());
        return cityname;
    }





    // skriver upp namn på staden och de byggnader du kan bygga
    public static void DisplayBuildqueue(List<Building> buildqueue)
    {
        Console.WriteLine($"building - {buildqueue[0].name} ({buildqueue[0].progress}/{buildqueue[0].cost})");
        
        int iteration = 1;
        while(iteration < buildqueue.Count)
        {
            Console.WriteLine(iteration + ") " + buildqueue[iteration].name);
            iteration++;
        }
        
    }





    // skriver upp resurserna
    public static void DisplayResources(List<Resource> resources, List<string> people, int day)
    {
        Console.WriteLine("day " + day);
        Console.WriteLine("food needed until next person: " + 3*people.Count);
        int iteration = 0;
        while (iteration < resources.Count)
        {
            Console.WriteLine(resources[iteration].name + ": " + resources[iteration].amount + " (" + resources[iteration].production + ")");
            iteration++;
        }
    }





    // skriver upp personer och arbetsuppgifter
    public static void DisplayWork(List<string> people, List<Building> buildings)
    {
        int iteration = people.Count-1;
        while (iteration >= buildings.Count)
        {
            Console.WriteLine(people[iteration] + " - building");
            iteration--;
        }
        while (iteration >= 0)
        {
            Console.WriteLine(people[iteration] + " - " + buildings[iteration].name);
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





    public static void Produce(List<Resource> resources, List<Building> buildings)
    {
        // spelet uppdaterar stadens resurser
        int iterationResource = 0;
        while (iterationResource < resources.Count)
        {
            int iterationBuilding = 0;
            while (iterationBuilding < buildings.Count)
            {
                // kollar efter byggnader som producerar food och ökar food med så mycket byggnaden producerar
                if (buildings[iterationBuilding].productionResource == resources[iterationResource])
                {
                    resources[iterationResource].amount += buildings[iterationBuilding].productionAmount;
                }
                iterationBuilding++;
            }
            iterationResource++;
        }
    }





    public static void PopulationGrowth(Resource food, List<string> people)
    {
        // ökar population
        if (food.amount >= people.Count*3)
        {
            food.amount=0;
            Console.Clear();
            Console.WriteLine("congratulations your a new member of your city has appeared!");
            Console.WriteLine("please choose a name for the member");
            people.Add(Console.ReadLine());
        }
    }




    public static void BuildingWork(Resource wood, List<string> people, List<Building> buildings, List<Building> buildqueue)
    {
        // bygger på byggnad
        if (wood.amount > people.Count-buildings.Count)
        {
        buildqueue[0].progress += people.Count-buildings.Count;
        wood.amount -= people.Count-buildings.Count;
        }
        else 
        {
            buildqueue[0].progress += wood.amount;
            wood.amount = 0;
        }
    }
}