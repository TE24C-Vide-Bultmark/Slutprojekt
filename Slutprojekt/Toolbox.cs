// orange colored "building" ANSI code: \u001b[38;5;208mbuilding\u001b[0m

public class Toolbox
{
    // introducerar spelet och låter spelaren namnge staden samt lägger till de första människorna
    public static string Intro(List<string> people, int day)
    {
        // förklarar spelet
        Console.WriteLine("Hello and welcome to my game!");
        Console.WriteLine("In this game you manage a city and watch it grow.");
        // spelaren får välja några namn
        Console.WriteLine("Please choose a name for your city.");
        string cityname;
        do
        {
            // läser in namn
            cityname = Console.ReadLine();
            // om namnet INTE är tillåtet, körs loopen igen
        } while (!CheckName(cityname, []));
        Console.WriteLine($"And who is the first founder of {cityname}?");
        AddPerson(people, day);
        Console.WriteLine($"And who is the second founder");
        AddPerson(people, day);
        return cityname;
    }





    // skriver upp namn på staden och de byggnader du kan bygga
    public static void DisplayBuildingOptions(List<Building> buildingOptions)
    {
        Console.WriteLine($"Currently \u001b[38;5;208mbuilding\u001b[0m: {buildingOptions[0].name} ({buildingOptions[0].progress}/{buildingOptions[0].costAmount} {buildingOptions[0].costResource.name})");
        Console.WriteLine("Options:");
        // loopen går genom alla byggnader spelaren kan spela
        for (int iteration = 1; iteration < buildingOptions.Count; iteration++)
        {
            // skriver upp byggnadens plats, namn, progress, kostnad och vilken resurs den kostar
            Console.WriteLine($"{iteration}) {buildingOptions[iteration].name} ({buildingOptions[iteration].progress}/{buildingOptions[iteration].costAmount} {buildingOptions[iteration].costResource.name})");
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
        // kollar om du har fler byggnader eller personer
        if (people.Count >= buildings.Count)
        {
            // körs om du har fler personer än byggnader
            for (int i = people.Count - 1; i >= buildings.Count; i--) Console.WriteLine(i+1 + ") " + people[i] + " - \u001b[38;5;208mbuilding\u001b[0m");
            // körs för de personer so  har en byggnad
            for (int i = buildings.Count - 1; i >= 0; i--) Console.WriteLine(i+1 + ") " + people[i] + " - " + buildings[i].name);
        }
        else
        {
            // om du har fler byggander än personer
            for (int i = buildings.Count - 1; i >= people.Count; i--) Console.WriteLine(i+1 + ") [empty] - " + buildings[i].name);
            // körs för de byggnader som har personer
            for (int i = people.Count - 1; i >= 0; i--) Console.WriteLine(i+1 + ") " + people[i] + " - " + buildings[i].name);
        }
    }





    // byter plats på vald byggnad och byggnaden under konstruktion, om spelaren skrev in input som inte korresponderar med en byggnad går vi över till nästa dag
    public static bool ReadInput(List<Building> buildingOptions, List<string> graveyard, List<Building> buildings)
    {
        string input = Console.ReadLine();
        int inputInt;
        // sätter in det skrivna numret i input
        bool success = int.TryParse(input, out inputInt);
        if (input == "g")
        {
            Console.Clear();
            // skriver ut kyrkogården
            Console.WriteLine("In memory of:");
            for (int i = 0; i < graveyard.Count; i++) Console.WriteLine(graveyard[i]);
            Console.WriteLine("\nPress enter to go back");
            Console.ReadLine();
            return false;
        }
        else if (input == "h")
        {
            Console.Clear();
            DisplayBuildingOptions(buildingOptions);
            int secondInput;
            Console.WriteLine("Enter the number left to the building you want to start building");
            int.TryParse(Console.ReadLine(), out secondInput);
            if (buildingOptions.Count > secondInput && secondInput > 0)
            {
                Building temp = buildingOptions[secondInput];
                buildingOptions[secondInput] = buildingOptions[0];
                buildingOptions[0] = temp;
            }
            return false;
        }
        // byter plats på byggnader i staden
        else if (buildings.Count > inputInt && success)
        {
            int secondInput;
            Console.WriteLine("Enter the number left to the building you want to switch it with");
            int.TryParse(Console.ReadLine(), out secondInput);
            secondInput--;
            Building temp = buildings[inputInt];
            buildings[inputInt] = buildings[secondInput];
            buildings[secondInput] = temp;
            return false;
        }
        // om spelarens input korresponderar till en av byggvalen börjar staden bygga den byggnaden
        // om inputen inte korresponderar till en byggnad tolkas detta som en pass
        else return true;
    }





    public static void Produce(List<Resource> resources, List<Building> buildings, List<string> people)
    {
        // loop som går genom varje byggnad
        for (int iterationBuilding = 0; iterationBuilding < buildings.Count; iterationBuilding++)
        {
            // variabel som håller koll på om en byggnad kan producera sina resurser eller inte
            bool canProduce = CheckCanproduce(buildings, iterationBuilding, people);
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





    public static void PopulationGrowth(Resource food, List<string> people, int day)
    {
        // ökar population
        if (food.amount >= people.Count * people.Count)
        {
            food.amount = 0;
            Console.Clear();
            Console.WriteLine("congratulations, a new member of your city has appeared!");
            AddPerson(people, day);
        }
    }





    public static void BuildingWork(List<string> people, List<Building> buildings, List<Building> buildingOptions)
    {
        // kollar om har personer som bygger
        if (people.Count > buildings.Count)
        {
            // bygger på byggnad
            if (buildingOptions[0].costResource.amount > people.Count - buildings.Count)
            {
                // om du har mer resurser än personer som bygger så tas förlorar du reurser lika med hur många som bygger och byggnaden får så mycket progress
                buildingOptions[0].progress += people.Count - buildings.Count;
                buildingOptions[0].costResource.amount -= people.Count - buildings.Count;
            }
            else
            {
                // om du har färre resurser än du har personer som bygger går alla resurser till progress och antalet reurser blir 0
                buildingOptions[0].progress += buildingOptions[0].costResource.amount;
                buildingOptions[0].costResource.amount = 0;
            }
            // lägger till byggnaden om den är färdig
            if (buildingOptions[0].progress >= buildingOptions[0].costAmount)
            {
                buildingOptions[0].progress = 0;
                buildings.Add(buildingOptions[0]);
            }
        }
    }





    public static void Research(List<Building> technologies, List<Building> buildingOptions, Resource science, List<Resource> resources)
    {
        List<Building> techOptions = [];
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
    public static void AddPerson(List<string> people, int day)
    {
        // frågar spelaren om ett namn för den nya personen
        Console.WriteLine("please choose a name for the new person.");
        string name;
        // loopen kollar efter otillåtna namn
        do
        {
            // läser in namn
            name = Console.ReadLine();
            // om namnet INTE är tillåtet, körs loopen igen
        } while (!CheckName(name, people));
        // lägger till det skrivna namnet in i staden
        people.Add(name + ": born day " + day);
    }





    // kollar om ett namn är tillåtet
    public static bool CheckName(string name, List<string> names)
    {
        if (name.Length < 1)
        {
            Console.WriteLine("name must be at least 1 character");
            return false;
        }
        if (name.Length > 50)
        {
            Console.WriteLine("name must be at most 50 characters");
            return false;
        }
        foreach (string item in names)
        {
            if (name == item)
            {
                Console.WriteLine("name can not be the same as another person");
                return false;
            }
        }
        return true;
    }





    // kollar om en byggnad kan producera
    public static bool CheckCanproduce(List<Building> buildings, int iterationBuilding, List<string> people)
    {
        foreach (KeyValuePair<Resource, int> item in buildings[iterationBuilding].production)
        {
            // kollar om produktionen skulle resultera i ett negativt antal resurser eller om det finns en person
            if (item.Key.amount + item.Value < 0 || iterationBuilding > people.Count - 1)
            {
                return false;
            }
        }
        return true;
    }





    public static List<List<Building>> GenerateTechnologyTree()
    {
        // teknologier, det ska kunna försvinna byggnader från dessa, därav måste de vara listor istället för arrayer
        List<Building> technologies0 = [Building.library, Building.quarry, Building.quarry, Building.sawmill];
        List<Building> technologies1 = [Building.bigFarm, Building.library, Building.quarry, Building.badMine];
        List<Building> technologies2 = [Building.bigFarm, Building.bigFarm, Building.badMine, Building.goodMine, Building.forge];

        // skappar ett teknologiträd och lägger in alla teknologier
        List<List<Building>> technologytree = [];
        technologytree.Add(technologies0);
        technologytree.Add(technologies1);
        technologytree.Add(technologies2);
        return technologytree;
    }





    // tillåter spelaren att byta plats på byggnader
    public static void ChangeLayout(List<Building> buildings)
    {
        Console.WriteLine("select which building to switch");
        int building1;
        bool success = int.TryParse(Console.ReadLine(), out building1);
        Console.WriteLine("select which building to switch it with");
        int building2;
        success = int.TryParse(Console.ReadLine(), out building2);

        // bytter plats på de tvp valda byggnaderna
        Building temp = buildings[building1];
        buildings[building1] = buildings[building2];
        buildings[building1] = temp;
    }

    public static void KillPeople(List<string> people, List<string> graveyard, int day)
    {
        // loopen går igenom alla personer i din stad
        for (int iteration = 0; iteration < people.Count; iteration++)
        {
            // slumpar ett tal mellan från och med 0 till och med 99
            if (0 == Random.Shared.Next(100))
            {
                // dödar personen om den får 0
                graveyard.Add(people[iteration] + " died day " + day);
                people.RemoveAt(iteration);
            }
        }
    }




    public static void NewDay(int day, List<Resource> resources, List<Building> buildings, List<string> people, List<Building> buildingOptions, List<string> graveyard, List<List<Building>> technologyTree)
    {
        Produce(resources, buildings, people);
        BuildingWork(people, buildings, buildingOptions);
        PopulationGrowth(Resource.food, people, day);
        KillPeople(people, graveyard, day);
        // om spelaren har tillräckligt med forskning får de välja mellan 2 nya teknologier
        if (Resource.science.amount >= 50)
        {
            for (int i = 0; i < technologyTree.Count; i++)
            {
                if (technologyTree[i].Count > 2)
                {
                    Research(technologyTree[i], buildingOptions, Resource.science, resources);
                    break;
                }
            }
        }
    }
}