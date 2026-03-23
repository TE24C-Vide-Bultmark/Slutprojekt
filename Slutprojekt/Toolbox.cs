// orange building ANSI code: \u001b[38;5;208mbuilding\u001b[0m

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
        Console.WriteLine($"And who is the founder of {cityname}?");
        AddPerson(people);
        Console.WriteLine($"And who is {people[0]}`s friend");
        AddPerson(people);
        return cityname;
    }





    // skriver upp namn på staden och de byggnader du kan bygga
    public static void DisplayBuildqueue(List<Building> buildqueue)
    {
        Console.WriteLine("--------------------------------------------------------------------------------");
        Console.WriteLine($"Currently \u001b[38;5;208mbuilding\u001b[0m: {buildqueue[0].name} ({buildqueue[0].progress}/{buildqueue[0].costAmount} {buildqueue[0].costResource.name})");
        Console.WriteLine("Queue:");
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
        Console.WriteLine("Day " + day);
        Console.WriteLine("Food needed until next person: " + people.Count * people.Count);
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
                Console.WriteLine(" - \u001b[38;5;208mbuilding\u001b[0m");
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
        // loop som går genom varje byggnad
        for (int iterationBuilding = 0; iterationBuilding < buildings.Count; iterationBuilding++)
        {
            // variabel som håller koll på om en byggnad kan producera sina resurser eller inte
            bool canProduce = true;
            foreach (KeyValuePair<Resource, int> item in buildings[iterationBuilding].production)
            {
                // kollar om produktionen skulle resultera i ett negativt anatal resurser
                if (item.Key.amount + item.Value < 0)
                {
                    canProduce = false;
                    break;
                }
            }
            // kollar om denna byggnaden kan producera sina resurser
            if (canProduce)
            {
                // går igenom varje kay-value pair i production
                foreach (KeyValuePair<Resource, int> item in buildings[iterationBuilding].production)
                {
                    // ökar antalet item.key (resource) med item.value (int)
                    item.Key.amount += item.Value;
                }
            }
        }
        // produktion ej baserat på byggnader. resources[0] är food och resources[2] är science
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
        // loopen körs en gång för varje tech spelaren ska kunna forska
        for (int i = 0; i < 3; i++)
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

        // loop som körs för varje objekt i vald byggnads production 
        foreach (KeyValuePair<Resource, int> item in techOptions[choice - 1].production)
        {
            // loop som körs för varje resurs
            for (int i = 0; i < resources.Count; i++)
            {
                // kolla om resursen redan är med i "resources"
                if (resources[i] == item.Key)
                {
                    break;
                }
                // om resursen inte finns läggs den till i resurser
                else if (i == resources.Count - 1)
                {
                    resources.Add(item.Key);
                    break;
                }
            }
        }
    }





    // låter spelaren lägga till en person i staden
    public static void AddPerson(List<string> people)
    {
        // frågar spelaren om ett namn för den nya personen
        Console.WriteLine("please choose a name for the new person.");
        string name;
        bool nameIsLegal;
        // loopen kollar efter otillåtna namn
        do{
            // läser in namn
            name = Console.ReadLine();
            nameIsLegal = checkName(name, people);
            // om namnet INTE är tillåtet, körs loopen igen
        } while (nameIsLegal!);
        // lägger till det skrivna namnet in i staden
        people.Add(name);
    }





    // kollar om ett namn är tillåtet
    public static bool checkName(string name, List<string> people)
    {
        // antar tillåtet namn
        bool nameIsLegal = true;
        if (name.Length < 1)
        {
            Console.WriteLine("name must be at least 1 charcahter");
            nameIsLegal = false;
        }
        if (name.Length > 15)
        {
            Console.WriteLine("name must be at most 15 charachters");
            nameIsLegal = false;
        }
        foreach (string person in people)
        {
            if (name == person)
            {
                Console.WriteLine("name can not be the same as another person");
                nameIsLegal = false;
                break;
            }
        }
        return nameIsLegal;
    }
}