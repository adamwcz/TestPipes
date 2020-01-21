namespace TestPipeServer.Commands
{
    internal class CommandRPS : Command
    {
        internal class RPS
        {
            internal Client Player1 { get; set; } = null;
            internal Client Player2 { get; set; } = null;
            internal char Throw1 { get; set; } = '.';
            internal char Throw2 { get; set; } = '.';
            private static RPS itsInstance = null;
            internal static RPS Instance
            {
                get
                {
                    if (itsInstance == null)
                        itsInstance = new RPS();
                    return itsInstance;
                }
                set => itsInstance = value;
            }
        }
        internal char Throw { get; private set; } = '.';
        internal CommandRPS(params string[] args) : base(args)
        {
            if (args.Length >= 1)
            {
                if (args[0].ToLower().Equals("rock") || args[0].ToLower().Equals("r"))
                {
                    Throw = 'r';
                }
                else if (args[0].ToLower().Equals("paper") || args[0].ToLower().Equals("p"))
                {
                    Throw = 'p';
                }
                else if (args[0].ToLower().Equals("scissors") || args[0].ToLower().Equals("s"))
                {
                    Throw = 's';
                }
                else if (args[0].ToLower().Equals("lizard") || args[0].ToLower().Equals("l"))
                {
                    Throw = 'l';
                }
                else if (args[0].ToLower().Equals("spock") || args[0].ToLower().Equals("k"))
                {
                    Throw = 'k';
                }
            }
        }
        internal override void Run(Client theActor)
        {
            if (Throw == '.')
            {
                World.Send($"[Invalid selection! Choose rock, paper, or scissors!]", theActor);
                World.Logg($"[Invalid selection! Choose rock, paper, or scissors!]");
            }
            else if (RPS.Instance != null)
            {
                if (RPS.Instance.Player1 == null || !RPS.Instance.Player1.IsConnected)
                {
                    RPS.Instance.Player1 = theActor;
                    RPS.Instance.Throw1 = Throw;
                    World.Send($"You hold up your hands for a game of Rock, Paper, Scissors.", theActor);
                    World.Send($"{theActor.Name} holds up their hands for a game of Rock, Paper, Scissors.", theActor.AllClientsExceptMe());
                    World.Logg($"{theActor.Name} holds up their hands for a game of Rock, Paper, Scissors.");
                }
                else if (RPS.Instance.Player1 == theActor)
                {
                    RPS.Instance.Throw1 = Throw;
                }
                else if (RPS.Instance.Player2 == null)
                {
                    RPS.Instance.Player2 = theActor;
                    RPS.Instance.Throw2 = Throw;

                    string aRPSGameMessage = "";
                    if (RPS.Instance.Throw1 == 'r')
                    {
                        if (RPS.Instance.Throw2 == 'p')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player2.Name}'s Paper covers {RPS.Instance.Player1.Name}'s Rock!";
                        }
                        else if (RPS.Instance.Throw2 == 's')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player1.Name}'s Rock crushes {RPS.Instance.Player2.Name}'s Scissors!";
                        }
                        else if (RPS.Instance.Throw2 == 'l')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player1.Name}'s Rock crushes {RPS.Instance.Player2.Name}'s Lizard!";
                        }
                        else if (RPS.Instance.Throw2 == 'k')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player2.Name}'s Spock vaporizes {RPS.Instance.Player1.Name}'s Rock!";
                        }
                        else
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player1.Name} and {RPS.Instance.Player2.Name} both throw Rock";
                        }
                    }
                    else if (RPS.Instance.Throw1 == 'p')
                    {
                        if (RPS.Instance.Throw2 == 's')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player2.Name}'s Scissors cuts {RPS.Instance.Player1.Name}'s Paper!";
                        }
                        else if (RPS.Instance.Throw2 == 'r')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player1.Name}'s Paper covers {RPS.Instance.Player2.Name}'s Rock!";
                        }
                        else if (RPS.Instance.Throw2 == 'l')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player2.Name}'s Lizard eats {RPS.Instance.Player1.Name}'s Paper!";
                        }
                        else if (RPS.Instance.Throw2 == 'k')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player1.Name}'s Paper disproves {RPS.Instance.Player2.Name}'s Spock!";
                        }
                        else
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player1.Name} and {RPS.Instance.Player2.Name} both throw Paper";
                        }
                    }
                    else if (RPS.Instance.Throw1 == 's')
                    {
                        if (RPS.Instance.Throw2 == 'r')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player2.Name}'s Rock crushes {RPS.Instance.Player1.Name}'s Scissors!";
                        }
                        else if (RPS.Instance.Throw2 == 'p')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player1.Name}'s Scissors cuts {RPS.Instance.Player2.Name}'s Paper!";
                        }
                        else if (RPS.Instance.Throw2 == 'l')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player1.Name}'s Scissors decapitates {RPS.Instance.Player2.Name}'s Lizard!";
                        }
                        else if (RPS.Instance.Throw2 == 'k')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player2.Name}'s Spock smashes {RPS.Instance.Player1.Name}'s Scissors!";
                        }
                        else
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player1.Name} and {RPS.Instance.Player2.Name} both throw Scissors";
                        }
                    }
                    else if (RPS.Instance.Throw1 == 'l')
                    {
                        if (RPS.Instance.Throw2 == 'r')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player2.Name}'s Rock crushes {RPS.Instance.Player1.Name}'s Lizard!";
                        }
                        else if (RPS.Instance.Throw2 == 'p')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player1.Name}'s Lizard eats {RPS.Instance.Player2.Name}'s Paper!";
                        }
                        else if (RPS.Instance.Throw2 == 's')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player2.Name}'s Scissors decapitates {RPS.Instance.Player1.Name}'s Lizard!";
                        }
                        else if (RPS.Instance.Throw2 == 'k')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player1.Name}'s Lizard poisons {RPS.Instance.Player2.Name}'s Spock!";
                        }
                        else
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player1.Name} and {RPS.Instance.Player2.Name} both throw Lizard";
                        }
                    }
                    else if (RPS.Instance.Throw1 == 'k')
                    {
                        if (RPS.Instance.Throw2 == 'r')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player1.Name}'s Spock vaporizes {RPS.Instance.Player2.Name}'s Rock!";
                        }
                        else if (RPS.Instance.Throw2 == 'p')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player2.Name}'s Paper disproves {RPS.Instance.Player1.Name}'s Spock!";
                        }
                        else if (RPS.Instance.Throw2 == 's')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player1.Name}'s Spock smashes {RPS.Instance.Player2.Name}'s Scissors!";
                        }
                        else if (RPS.Instance.Throw2 == 'l')
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player2.Name}'s Lizard poisons {RPS.Instance.Player1.Name}'s Spock!";
                        }
                        else
                        {
                            aRPSGameMessage = $"{RPS.Instance.Player1.Name} and {RPS.Instance.Player2.Name} both throw Spock";
                        }
                    }
                    World.SendAll(aRPSGameMessage);
                    World.Logg(aRPSGameMessage);
                    RPS.Instance = new RPS();
                }
            }
        }
    }
}