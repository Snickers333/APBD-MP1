using System.Text.RegularExpressions;

if (args.Length < 1) throw new ArgumentNullException();

var url = args[0];
if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
{
    throw new ArgumentException();
}

var httpClient = new HttpClient();
HttpResponseMessage response = await httpClient.GetAsync(url);
if (!response.IsSuccessStatusCode)
{
    throw new Exception("Błąd w czasie pobierania strony");
}

string content = await response.Content.ReadAsStringAsync();
var regex = new Regex(@"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+");
MatchCollection matches = regex.Matches(content);

if (matches.Count < 1)
{
    throw new Exception("Nie znaleziono adresów email");
}

var distinctMatches = new HashSet<string>();
foreach (Match match in matches)
{
    distinctMatches.Add(match.ToString());
}

foreach (string s in distinctMatches)
{
    Console.WriteLine(s);
}