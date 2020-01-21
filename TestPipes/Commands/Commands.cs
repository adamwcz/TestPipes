using System.Linq;

namespace TestPipeServer.Commands
{
    abstract class Command
    {
        protected World World => World.Instance;
        protected string[] Args { get; private set; }
        internal Command(params string[] theArgs)
        {
            Args = theArgs;
        }
        internal abstract void Run(Client theActor);
        internal static Command MakeCommand(params string[] args)
        {
            if (args[0] is string type)
            {
                args = args.Skip(1).ToArray();
                switch (type)
                {
                    default:
                        break;
                    case "e":
                    case "emote":
                        return new CommandCustomEmote(args);
                    case "roll":
                        return new CommandRoll(args);
                    case "rps":
                    case "rockpaperscissors":
                        return new CommandRPS(args);
                    case "rename":
                        return new CommandRename(args);
                }
            }
            return null;
        }
    }
    class CommandRoll : Command
    {
        internal int NumDice { get; private set; } = 1;
        internal int Sides { get; private set; } = 6;
        internal CommandRoll(params string[] args) : base(args)
        {
            if (args.Length >= 1 && int.TryParse(args[0], out int sides))
            {
                Sides = sides;
                if (args.Length >= 2 && int.TryParse(args[1], out int dice))
                {
                    NumDice = Sides;
                    Sides = dice;
                }
            }
        }
        internal override void Run(Client theActor)
        {
            int total = 0;

            for (int i = 0; i < NumDice; i++)
            {
                total += World.Random.Next(Sides) + 1;
            }
            World.Send($"[You rolled a {NumDice}d{Sides}: {total}]", theActor);
            World.Send($"[{theActor.Name} rolled a {NumDice}d{Sides}: {total}]", theActor.AllClientsExceptMe());
            World.Logg($"[{theActor.Name} rolled a {NumDice}d{Sides}: {total}]");
        }
    }
    class CommandCustomEmote : Command
    {
        internal string CustomEmote { get; private set; }
        internal CommandCustomEmote(params string[] args) : base(args)
        {
            if (args.Length >= 1)
                CustomEmote = args[0];
        }
        internal override void Run(Client theActor)
        {
            if (!string.IsNullOrEmpty(CustomEmote))
            {
                World.SendAll($"{theActor.Name} {CustomEmote}.");
                World.Logg($"{theActor.Name} {CustomEmote}.");
            }
        }
    }
    class CommandRename : Command
    {
        internal string NewName { get; private set; }
        internal CommandRename(params string[] args) : base(args)
        {
            if (args.Length >= 1)
                NewName = args[0];
        }
        internal override void Run(Client theActor)
        {
            World.Rename(theActor, NewName);
        }
    }
}