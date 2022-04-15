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

//6
DateTime dateTime = DateTime.Parse("2020-01-06");
DateOnly date = DateOnly.FromDateTime(dateTime);
TimeOnly time = TimeOnly.FromDateTime(dateTime);

Console.WriteLine(time.ToString("O"));

//7
var userHandlers = new[]
{
    "users/okyrylchuk",
    "users/shanselman",
    "users/jaredpar",
    "users/davidfowl"
};

var users = new ConcurrentBag<GitHubUser>();

using HttpClient client = new()
{
BaseAddress = new Uri("https://api.github.com"),
};
client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("DotNet", "6"));

ParallelOptions parallelOptions = new()
{
MaxDegreeOfParallelism = 3
};

await Parallel.ForEachAsync(userHandlers, parallelOptions, async (uri, token) =>
{
var user = await client.GetFromJsonAsync<GitHubUser>(uri, token);
users.Add(user);
Console.WriteLine($"Name: {user.Name}\nBio: {user.Bio}\n");
});


//my test

//ForEachAsync example
var userTask = users.Select((user) => new Func<Task<int>>(() => UpdateUserAsync(user)));

await Parallel.ForEachAsync(userTask, new ParallelOptions
{
MaxDegreeOfParallelism = -1
}, async (updateUser, _) =>
{
await updateUser();
});

//WhenAll
var anotherUserTask = users.Select(async (user) => await UpdateUserAsync(user));
await Task.WhenAll(anotherUserTask);

//ForEachAsync
await Parallel.ForEachAsync(users, new ParallelOptions
{
MaxDegreeOfParallelism = -1
}, async (user, _) =>
{
await UpdateUserAsync(user);
});

async Task<int> UpdateUserAsync(GitHubUser user)
{
Console.WriteLine("1");
return await Task.FromResult(1);
}

public class GitHubUser
{
    public string Name { get; set; }
    public string Bio { get; set; }
}

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
