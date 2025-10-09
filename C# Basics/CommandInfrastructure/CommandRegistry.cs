
namespace Basics.Command;

public class CommandRegistry
{
    private Dictionary<string, ICommand> Commands { get; } = new();
    public IEnumerable<ICommand> AllCommands => Commands.Values;
    public CommandRegistry Add(ICommand command)
    {
        Commands[command.Name] = command;
        return this;
    }
    public bool TryGetCommand(string name, out ICommand? command)
    {
        return Commands.TryGetValue(name, out command);
    }

}
