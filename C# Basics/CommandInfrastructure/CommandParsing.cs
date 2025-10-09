using System.Text.RegularExpressions;

namespace Basics.Command;
public static class CommandParsing
{
    static readonly Regex Rx = new(@"[\""].+?[\""]|[^ ]+", RegexOptions.Compiled);
    public static IEnumerable<string> ParseArgs(string input)
    {
        foreach (Match match in Rx.Matches(input))
        {
            yield return match.Value.Trim('"');
        }
    }
}

