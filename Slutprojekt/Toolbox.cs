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
    public static void DisplayResources(List<Resource> resources, List<string> people, int day, List<Building> buildingOptions)
    {
        Console.WriteLine("Day " + day);
        Console.WriteLine("Food needed until next person: " + people.Count * people.Count);
        Console.WriteLine("Science needed until next technology: " + buildingOptions.Count * buildingOptions.Count * 20);
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
            for (int i = people.Count - 1; i >= buildings.Count; i--) Console.WriteLine("\u001b[38;5;208mbuilding\u001b[0m - " + people[i]);
            // körs för de personer so  har en byggnad
            for (int i = buildings.Count - 1; i >= 0; i--) Console.WriteLine(i + 1 + ") " + buildings[i].name + " - " + people[i]);
        }
        else
        {
            // om du har fler byggander än personer
            for (int i = buildings.Count - 1; i >= people.Count; i--) Console.WriteLine(i + 1 + ") " + buildings[i].name + " - [empty]");
            // körs för de byggnader som har personer
            for (int i = people.Count - 1; i >= 0; i--) Console.WriteLine(i + 1 + ") " + buildings[i].name + " - " + people[i]);
        }
    }





    // läser in spelarens input
    public static bool ReadInput(List<Building> buildingOptions, List<string> graveyard, List<Building> buildings)
    {
        string input = Console.ReadLine();
        int inputInt;
        // sätter in det skrivna numret i input
        int.TryParse(input, out inputInt);
        if (input == "g")
        {
            DisplayGraveyard(graveyard);
            return false;
        }
        else if (input == "h")
        {
            Console.Clear();
            DisplayBuildingOptions(buildingOptions);
            SwitchCurrentlyBuilding(buildingOptions);
            return false;
        }
        // byter plats på byggnader i staden
        else if (buildings.Count >= inputInt && inputInt > 0)
        {
            SwitchWork(buildings, inputInt);
            return false;
        }
        // om spelarens input korresponderar till något går spelet till nästa dag
        else return true;
    }





    public static void DisplayGraveyard(List<string> graveyard)
    {
        Console.Clear();
        // skriver ut kyrkogården
        Console.WriteLine("In memory of:");
        for (int i = 0; i < graveyard.Count; i++) Console.WriteLine(graveyard[i]);
        Console.WriteLine("\nPress enter to go back");
        Console.ReadLine();
    }





    public static void SwitchCurrentlyBuilding(List<Building> buildingOptions)
    {
        int secondInput;
        Console.WriteLine("Enter the number left to the building you want to start building");
        int.TryParse(Console.ReadLine(), out secondInput);
        if (buildingOptions.Count > secondInput && secondInput > 0)
        {
            Building temp = buildingOptions[secondInput];
            buildingOptions[secondInput] = buildingOptions[0];
            buildingOptions[0] = temp;
        }
    }





    public static void SwitchWork(List<Building> buildings, int inputInt)
    {
        int secondInput;
        Console.WriteLine("Enter the number left to the building you want to switch it with");
        int.TryParse(Console.ReadLine(), out secondInput);
        if (buildings.Count >= secondInput && secondInput > 0)
        {
            // anpassar input till listorna
            inputInt--;
            secondInput--;

            // byter plats på valda byggnader
            Building temp = buildings[inputInt];
            buildings[inputInt] = buildings[secondInput];
            buildings[secondInput] = temp;
        }
    }





    // producerar resurser
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




    // ökar population om det finns tillräckligt med mat
    public static void PopulationGrowth(Resource food, List<string> people, int day)
    {
        if (food.amount >= people.Count * people.Count)
        {
            food.amount = 0;
            Console.Clear();
            Console.WriteLine("Congratulations! A new member of your city has appeared!");
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
        // skapar en tom lista som ska lagra alla valbara teknologier
        List<Building> techOptions = [];
        Console.Clear();
        Console.WriteLine("Congratualations! Your city discovered a new technology!");
        // loopen körs en gång för varje tech spelaren ska kunna välja
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
                // om programmet har kollat alla resurser i "resources" utan att hitta den nya resursen, läggs den till i "resources"
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
        else if (name.Length > 50)
        {
            Console.WriteLine("name must be at most 50 characters");
            return false;
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
        List<Building> technologies0 = [Building.library, Building.quarry, Building.sawmill, Building.badMine];
        List<Building> technologies1 = [Building.bigFarm, Building.quarry, Building.badMine, Building.sawmill];
        List<Building> technologies2 = [Building.bigFarm, Building.badMine, Building.goodMine, Building.forge, Building.quarry];
        List<Building> technologies3 = [Building.forge, Building.engine, Building.bigFarm, Building.goodMine, Building.quarry];
        List<Building> technologies4 = [Building.forge, Building.engine, Building.factory];
        List<Building> technologies5 = [Building.particleAccelerator, Building.engine, Building.factory];

        // skappar ett teknologiträd och lägger in alla teknologier
        List<List<Building>> technologytree = [];
        technologytree.Add(technologies0);
        technologytree.Add(technologies1);
        technologytree.Add(technologies2);
        technologytree.Add(technologies3);
        technologytree.Add(technologies4);
        technologytree.Add(technologies5);
        return technologytree;
    }






    public static List<List<Building>> GenerateTechnologyTreeNew()
    {
        // teknologier, det ska kunna försvinna byggnader från dessa, därav måste de vara listor istället för arrayer
        List<Building> technologies0 = [];
        List<Building> technologies1 = [];
        List<Building> technologies2 = [];
        List<Building> technologies3 = [];
        List<Building> technologies4 = [];
        List<Building> technologies5 = [];

        // skappar ett teknologiträd och lägger in alla teknologier
        List<List<Building>> technologytree = [];
        technologytree.Add(technologies0);
        technologytree.Add(technologies1);
        technologytree.Add(technologies2);
        technologytree.Add(technologies3);
        technologytree.Add(technologies4);
        technologytree.Add(technologies5);

        // lägger yill byggnader i teknologilistorna med hjälp av for loopar
        for (int i = 0; i < 4; i++) technologytree[i].Add(Building.quarry);
        for (int i = 0; i < 3; i++) technologytree[i].Add(Building.bigFarm);
        for (int i = 0; i < 2; i++) technologytree[i].Add(Building.sawmill);
        for (int i = 0; i < 1; i++) technologytree[i].Add(Building.library);
        for (int i = 1; i < 4; i++) technologytree[i].Add(Building.badMine);
        for (int i = 0; i < 4; i++) technologytree[i].Add(Building.goodMine);
        for (int i = 0; i < 4; i++) technologytree[i].Add(Building.forge);
        for (int i = 0; i < 4; i++) technologytree[i].Add(Building.engine);
        for (int i = 0; i < 4; i++) technologytree[i].Add(Building.factory);
        for (int i = 5; i < 6; i++) technologytree[i].Add(Building.particleAccelerator);

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
        KillPeople(people, graveyard, day);
        PopulationGrowth(Resource.food, people, day);
        // om spelaren har 30 gånger mer forskning än teknologier får de välja mellan 3 nya teknologier
        if (Resource.science.amount >= buildingOptions.Count * buildingOptions.Count * 20)
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