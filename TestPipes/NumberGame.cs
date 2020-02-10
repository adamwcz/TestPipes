using TestPipesDLL;

namespace TestPipeServer
{
   class NumberGame
   {
      private static NumberGame itsInstance = null;
      internal static NumberGame Instance
      {
         get
         {
            if (itsInstance == null)
               itsInstance = new NumberGame();
            return itsInstance;
         }
         set => itsInstance = value;
      }

      public int Number = 0;
      public NumberGame() { }

      public void HandleInput(Client theSender, PipeMessage theMessage)
      {
         switch (theMessage)
         {
            case PipeMessageNumberGameAdd aPipeMessageNumberGameAdd:
               Number += aPipeMessageNumberGameAdd.NumberAdd;
               PipeMessageNumberGameUpdate aPipeMessageNumberGameUpdate = new PipeMessageNumberGameUpdate(Number);
               World.Instance.SendAll(aPipeMessageNumberGameUpdate/*theSender.AllClientsExceptMe()*/);
               break;

            default:
               break;
         }
      }
   }
}