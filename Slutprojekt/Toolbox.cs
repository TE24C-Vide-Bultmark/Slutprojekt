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
}