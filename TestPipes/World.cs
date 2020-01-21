using System;
using System.Linq;
using TestPipesDLL;
using TestPipeServer.Commands;

namespace TestPipeServer
{
    internal class World : ServerManager
    {
        #region Fields
        public static World Instance { get; private set; }
        #endregion

        #region Properties
        public static Random Random { get; } = new Random(DateTime.Now.Millisecond);
        #endregion

        #region Constructor
        public World() : base()
        {
            Instance = this;
        }
        #endregion

        #region Private

        #endregion

        #region Protected
        protected override void HandleInput(Client theSender, PipeMessage theMessage)
        {
            switch (theMessage)
            {
                case PipeMessageImageShare aPipeMessageImageShare:
                    Logg($"{theSender.ID}:{theSender.Name} shared an image: {aPipeMessageImageShare.ImageName}");
                    Send(aPipeMessageImageShare, theSender.AllClientsExceptMe());
                    break;

                case PipeMessageCommand aPipeMessageCommand:
                    Logg($"[{theSender.ID}:{theSender.Name}] {aPipeMessageCommand.Text}");
                    if (Command.MakeCommand(aPipeMessageCommand.CommandArgs) is Command aCommand)
                    {
                        aCommand.Run(theSender);
                    }
                    break;

                case PipeMessageText aPipeMessageText:
                    if (!string.IsNullOrEmpty(aPipeMessageText.Text))
                    {
                        Logg($"[{theSender.ID}:{theSender.Name}] -> {aPipeMessageText.Text}");
                        SendAll($"{theSender.Name}: {aPipeMessageText.Text}");
                    }
                    break;

                case PipeMessageNumberGame aPipeMessageNumberGame:
                    NumberGame.Instance.HandleInput(theSender, aPipeMessageNumberGame);
                    break;

                default:
                    base.HandleInput(theSender, theMessage);
                    break;
            }
        }
        #endregion

        #region Public
        public void Rename(Client theActor, string theNewName)
        {
            if (Clients.Any(x => x.Name == theNewName))
            {
                Send("[Name \"" + theNewName + "\"is already in use. Try another name.]", theActor);
                Logg($"[{theActor.Name} renamed failed, \"{theNewName}\" is in use.]");
            }
            else if (string.IsNullOrWhiteSpace(theNewName))
            {
                Send("[Your name can't be blank. Try it like this: \"/rename Timmy\"]", theActor);
                Logg($"[{theActor.Name} tried to rename to a blank name]");
            }
            else
            {
                string anOldName = theActor.Name;
                theActor.Name = theNewName;
                Send(anOldName + " renamed to " + theNewName, theActor.AllClientsExceptMe());
                Send("Your name is now " + theNewName, theActor);
                Logg($"[{anOldName} renamed to {theNewName}]");

                itsFormServer.RemoveUser(anOldName);
                itsFormServer.AddUser(theNewName);

                PipeMessageUserRenamed aPipeMessageUserRenamed = new PipeMessageUserRenamed(theActor.ID, theActor.Name);
                SendAll(aPipeMessageUserRenamed);
            }
        }
        #endregion

        #region Events

        #endregion
    }

    public static class Extensions
    {
        internal static Client Find(this World theWorld, string theName)
           => theWorld.Clients.FirstOrDefault(x => x.Name == theName);
        //public static Client[] NearbyClients(this Client theClient)
        //   => World.Instance.Clients.Where(x => x.Location == theClient.Location).ToArray();
        //public static Client[] NearbyClientsExceptMe(this Client theClient)
        //   => World.Instance.Clients.Where(x => x.Location == theClient.Location && x != theClient).ToArray();
        internal static Client[] AllClientsExceptMe(this Client theClient)
           => World.Instance.Clients.Where(x => x != theClient).ToArray();
    }
}