// See https://aka.ms/new-console-template for more information

//7 awesome improvements for LINQ in .NET 6

//1
var names = new[] { "Nick", "Mike", "John", "Leyla", "David", "Damian" };
var chunked = names.Chunk(3);

//2
var firstSet = new[] { "Nick Chapsas" };
var secondSet = new[] { "Peter Chapsas" };
var thirdSet = new[] { "Maria Chapsas" };

var allNames = firstSet.Concat(secondSet).Concat(thirdSet);

if (allNames.TryGetNonEnumeratedCount(out var count))
{

}

//3
var ages = new[] { 24,26,23,53,44,19};
var experience = new[] { 4, 6, 3, 23, 14, 1 };
var zip = names.Zip(ages, experience);

//4
var family = new[]
{
    new FamilyMember("Nick Chapsas", 28),
    new FamilyMember("Peter Chapsas", 22),
    new FamilyMember("Maria Chapsas", 25),
};

var youngerOld = family.OrderBy(x => x.Age).First();
var olderOld = family.OrderByDescending(x => x.Age).First();

var younger = family.MinBy(x => x.Age);
var older = family.MaxBy(x => x.Age);

//5
var thirdItemFromTheEnd = names.ElementAt(^3);
//var slice = names.Skip(2).Take(2);
var slice = names.Take(2..4);
var lastThree = names.Take(^3..);

internal class FamilyMember
{
    private string _name;
    private int _age;

    public FamilyMember(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public string Name { get => _name; set => _name = value; }
    public int Age { get => _age; set => _age = value; }
}
