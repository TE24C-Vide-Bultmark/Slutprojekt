public class Toolbox
{
    // introducerar spelet och låter spelaren namnge staden samt grundaren
    public static string Intro(List<string> people)
    {
        // förklarar spelet
        Console.WriteLine("Hello and welcome to my game!");
        Console.WriteLine("In this game you manage a city and watch it grow.");
        // spelaren får välja några namn
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
        // loopen går genom alla byggnader spelaren kan spela
        for (int iteration = 1; iteration < buildqueue.Count; iteration++)
        {
            // skriver upp byggnadens plats, namn, progress, kostnad och vilken resurs den kostar
            Console.WriteLine($"{iteration}) {buildqueue[iteration].name} ({buildqueue[iteration].progress}/{buildqueue[iteration].costAmount} {buildqueue[iteration].costResource.name})");
        }

    }





    // skriver upp resurserna
    public static void DisplayResources(List<Resource> resources, List<string> people, int day)
    {
        Console.WriteLine("day " + day);
        Console.WriteLine("food needed until next person: " + people.Count * people.Count);
        // skriver upp alla resurser spelaren kan producera
        for (int iteration = 0; iteration < resources.Count; iteration++)
        {
            Console.WriteLine(resources[iteration].name + ": " + resources[iteration].amount);
        }
    }





    // skriver upp personer och arbetsuppgifter
    public static void DisplayWork(List<string> people, List<Building> buildings)
    {
        // går genom all personer från nyast till äldst
        for (int iteration = people.Count - 1; iteration >= 0; iteration--)
        {
            // skriver namn
            Console.Write(people[iteration]);
            // om det inte finns någon byggnad att jobba i kommer de bygga nya byggnader
            if (iteration >= buildings.Count)
            {
                Console.WriteLine(" - building");
            }
            // om det finns en byggnad kommer de att jobba i den
            else
            {
                Console.WriteLine(" - " + buildings[iteration].name);
            }
        }
    }





    // byter plats på vald byggnad och byggnaden under konstruktion, om spelaren skrev in input som inte korresponderar med en byggnad går vi över till nästa dag
    public static bool SwitchBuilding(List<Building> buildqueue)
    {
        int input;
        // sätter in det skrivna numret i input
        int.TryParse(Console.ReadLine(), out input);
        // om spelarens input korresponderar till en av byggvalen börjar staden bygga den byggnaden
        if (buildqueue.Count > input && input > 0)
        {
            Building temp = buildqueue[input];
            buildqueue[input] = buildqueue[0];
            buildqueue[0] = temp;
            return false;
        }
        // om inputen inte korresponderar till en byggnad tolkas detta som en pass
        else return true;
    }





    public static void Produce(List<Resource> resources, List<Building> buildings, List<string> people)
    {
        // loop som går igenom varje resurs
        for (int iterationResource = 0; iterationResource < resources.Count; iterationResource++)
        {
            // looå som går genom varje byggnad
            for (int iterationBuilding = 0; iterationBuilding < buildings.Count; iterationBuilding++)
            {
                // kollar efter byggnader som producerar en viss resurs och ökar produktionen av den resursen med så mycket byggnaden producerar
                if (buildings[iterationBuilding].productionResource == resources[iterationResource])
                {
                    // producerar den resursen
                    resources[iterationResource].amount += buildings[iterationBuilding].productionAmount;
                }
            }
        }
        // produktion ej baserat på byggnader
        // rsources[0] är food
        resources[0].amount -= people.Count;
        // resources[2] är science
        resources[2].amount += people.Count;
    }





    public static void PopulationGrowth(Resource food, List<string> people)
    {
        // ökar population
        if (food.amount >= people.Count * people.Count)
        {
            food.amount = 0;
            Console.Clear();
            Console.WriteLine("congratulations, a new member of your city has appeared!");
            AddPerson(people);
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





    // denna metod ser till att du alltid har de resurser du kan producera i den resurslista
    public static void AddResource(List<Resource> resources, List<Building> techOptions, int choice)
    {
        // loop som körs för varje resurs
        for (int i=0; i < resources.Count; i++)
        {
            // om resursen redan finns bryts loopen
            if (resources[i] == techOptions[choice - 1].productionResource)
            {
                break;
            }
            // om resursen inte finns läggs den till i resurser
            else if (i == resources.Count - 1)
            {
                resources.Add(techOptions[choice - 1].productionResource);
            }
        }
    }





    // låter spelaren lägga till en person i staden
    public static void AddPerson(List<string> people)
    {
        // sätter namnet till ett otillåtet värde med syfte att köra loopen
        string name = "";
        // loopen kollar efter otillåtna värden
            while (name.Length < 1 || name.Length > 20)
            {
                // informerar om restriktioner
                Console.WriteLine("please choose a name for the new person. Name must be between 1 and 20 characters");
                // lägger in det skrivna namnet i en variabel
                name = Console.ReadLine();
            }
            // lägger till det skrivna namnet in i staden
            people.Add(name);
    }
}