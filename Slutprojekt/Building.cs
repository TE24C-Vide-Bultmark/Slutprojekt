public class Building
{
    public string name = "missing";
    public Dictionary<Resource, int> production = new();
    public int costAmount = 10;
    public Resource costResource;
    public int progress = 0;
}