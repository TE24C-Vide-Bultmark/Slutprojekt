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
        Console.WriteLine($"And who is the founder of {cityname}?");
        people.Add(Console.ReadLine());
        return cityname;
    }





    // skriver upp namn på staden och de byggnader du kan bygga
    public static void DisplayBuildqueue(List<Building> buildqueue)
    {
        Console.WriteLine($"building - {buildqueue[0].name}");
        
        int iteration = 1;
        while(iteration < buildqueue.Count)
        {
            Console.WriteLine(iteration + ") " + buildqueue[iteration].name);
            iteration++;
        }
        
    }





    // skriver upp resurserna
    public static void DisplayResources(List<Resource> resources, List<string> people)
    {
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
        while (iteration > buildings.Count)
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
        if (buildqueue.Count > input && input >= 0)
        {
        Building temp = buildqueue[input]; 
        buildqueue[input] = buildqueue[0];
        buildqueue[0] = temp;  
        return false;    
        }
        else return true;
    }
}