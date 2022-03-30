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

