using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace TestPipesDLL
{
    [Serializable]
    public class PipeMessage : ISerializable
    {
        /// <summary>
        /// ID # of the client
        /// </summary>
        public int ID { get; set; } = -1;
        new public string ToString() => ID + " | " + GetType();
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeMessage()
        {
        }
        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public PipeMessage(SerializationInfo info, StreamingContext context)
        {
            ID = (int)info.GetValue("id", typeof(int));
        }
        /// <summary>
        /// Serialization function
        /// </summary>
        public virtual void GetObjectData(SerializationInfo theInfo, StreamingContext theContext)
        {
            theInfo.AddValue("id", ID, typeof(int));
        }
    }
    [Serializable]
    public class PipeMessageText : PipeMessage
    {
        /// <summary>
        /// Message Text
        /// </summary>
        public string Text { get; set; } = string.Empty;
        /// <summary>
        /// Name of the user who is sending the text
        /// </summary>
        public string Name { get; set; } = null;
        new public string ToString() => ID + " | " + GetType() + " | " + Text;
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeMessageText(string theText, string theName) : base()
        {
            Text = theText;
            Name = theName;
        }
        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public PipeMessageText(SerializationInfo theInfo, StreamingContext theContext) : base(theInfo, theContext)
        {
            Text = (string)theInfo.GetValue("text", typeof(string));
            Name = (string)theInfo.GetValue("name", typeof(string));
        }
        /// <summary>
        /// Serialization function
        /// </summary>
        public override void GetObjectData(SerializationInfo theInfo, StreamingContext theContext)
        {
            base.GetObjectData(theInfo, theContext);
            theInfo.AddValue("text", Text, typeof(string));
            theInfo.AddValue("name", Name, typeof(string));
        }
    }
    [Serializable]
    public class PipeMessageCommand : PipeMessageText
    {
        public string[] CommandArgs { get; set; } = null;
        new public string ToString() => ID + " | " + string.Join(",", CommandArgs) + " | " + Text;
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeMessageCommand(string theText, string theUserName, params string[] theCommandArgs) : base(theText, theUserName)
        {
            CommandArgs = theCommandArgs;
        }
        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public PipeMessageCommand(SerializationInfo theInfo, StreamingContext theContext) : base(theInfo, theContext)
        {
            CommandArgs = (string[])theInfo.GetValue("commandargs", typeof(string[]));
        }
        /// <summary>
        /// Serialization function
        /// </summary>
        public override void GetObjectData(SerializationInfo theInfo, StreamingContext theContext)
        {
            base.GetObjectData(theInfo, theContext);
            theInfo.AddValue("commandargs", CommandArgs, typeof(string[]));
        }
    }

    [Serializable]
    public class PipeMessageUserList : PipeMessage
    {
        public List<User> UserList { get; set; } = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeMessageUserList(List<User> theUserList) : base()
        {
            UserList = theUserList;
        }
        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public PipeMessageUserList(SerializationInfo theInfo, StreamingContext theContext) : base(theInfo, theContext)
        {
            UserList = (List<User>)theInfo.GetValue("userlist", typeof(List<User>));
        }
        /// <summary>
        /// Serialization function
        /// </summary>
        public override void GetObjectData(SerializationInfo theInfo, StreamingContext theContext)
        {
            base.GetObjectData(theInfo, theContext);
            theInfo.AddValue("userlist", UserList, typeof(List<User>));
        }
    }

    [Serializable]
    public class PipeMessageImageShare : PipeMessage
    {
        public Image SharedImage { get; set; } = null;
        public string ImageSenderName { get; set; } = null;
        public string ImageName { get; set; } = null;
        public string FileSize { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeMessageImageShare(Image theImage, string theSenderName, string theFileName, string theFileSize) : base()
        {
            SharedImage = theImage;
            ImageSenderName = theSenderName;
            ImageName = theFileName;
            FileSize = theFileSize;
        }
        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public PipeMessageImageShare(SerializationInfo theInfo, StreamingContext theContext) : base(theInfo, theContext)
        {
            SharedImage = (Image)theInfo.GetValue("sharedimage", typeof(Image));
            ImageSenderName = (string)theInfo.GetValue("imagesendername", typeof(string));
            ImageName = (string)theInfo.GetValue("imagename", typeof(string));
            FileSize = (string)theInfo.GetValue("filesize", typeof(string));
        }
        /// <summary>
        /// Serialization function
        /// </summary>
        public override void GetObjectData(SerializationInfo theInfo, StreamingContext theContext)
        {
            base.GetObjectData(theInfo, theContext);
            theInfo.AddValue("sharedimage", SharedImage, typeof(Image));
            theInfo.AddValue("imagesendername", ImageSenderName, typeof(string));
            theInfo.AddValue("imagename", ImageName, typeof(string));
            theInfo.AddValue("filesize", FileSize, typeof(string));
        }
    }

    [Serializable]
    public class PipeMessageRenameUser : PipeMessage
    {
        public Image SharedImage { get; set; } = null;
        public string ImageSenderName { get; set; } = null;
        public string ImageName { get; set; } = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeMessageRenameUser(Image theImage, string theSenderName, string theFileName) : base()
        {
            SharedImage = theImage;
            ImageSenderName = theSenderName;
            ImageName = theFileName;
        }
        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public PipeMessageRenameUser(SerializationInfo theInfo, StreamingContext theContext) : base(theInfo, theContext)
        {
            SharedImage = (Image)theInfo.GetValue("sharedimage", typeof(Image));

        }
        /// <summary>
        /// Serialization function
        /// </summary>
        public override void GetObjectData(SerializationInfo theInfo, StreamingContext theContext)
        {
            base.GetObjectData(theInfo, theContext);
            theInfo.AddValue("sharedimage", SharedImage, typeof(Image));

        }
    }

    [Serializable]
    public class PipeMessageKicked : PipeMessage
    {
        /// <summary>
        /// Reason for being kicked
        /// </summary>
        public string Reason { get; set; } = "None";
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeMessageKicked(string theReason) : base()
        {
            Reason = theReason;
        }
        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public PipeMessageKicked(SerializationInfo theInfo, StreamingContext theContext) : base(theInfo, theContext)
        {
            Reason = (string)theInfo.GetValue("reason", typeof(string));
        }
        /// <summary>
        /// Serialization function
        /// </summary>
        public override void GetObjectData(SerializationInfo theInfo, StreamingContext theContext)
        {
            base.GetObjectData(theInfo, theContext);
            theInfo.AddValue("reason", Reason, typeof(string));
        }
    }

    [Serializable]
    public class PipeMessageUserConnection : PipeMessage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeMessageUserConnection() : base()
        {
        }
        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public PipeMessageUserConnection(SerializationInfo theInfo, StreamingContext theContext) : base(theInfo, theContext)
        {
        }
        /// <summary>
        /// Serialization function
        /// </summary>
        public override void GetObjectData(SerializationInfo theInfo, StreamingContext theContext)
        {
            base.GetObjectData(theInfo, theContext);
        }
    }

    [Serializable]
    public class PipeMessageUserDisconnection : PipeMessage
    {
        /// <summary>
        /// Name of the user who is disconnecting
        /// </summary>
        public string Name { get; set; } = null;
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeMessageUserDisconnection(string theName) : base()
        {
            Name = theName;
        }
        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public PipeMessageUserDisconnection(SerializationInfo theInfo, StreamingContext theContext) : base(theInfo, theContext)
        {
            Name = (string)theInfo.GetValue("name", typeof(string));
        }
        /// <summary>
        /// Serialization function
        /// </summary>
        public override void GetObjectData(SerializationInfo theInfo, StreamingContext theContext)
        {
            base.GetObjectData(theInfo, theContext);
            theInfo.AddValue("name", Name, typeof(string));
        }
    }

    [Serializable]
    public class PipeMessageAssignID : PipeMessage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeMessageAssignID(int theID) : base()
        {
            ID = theID;
        }
        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public PipeMessageAssignID(SerializationInfo theInfo, StreamingContext theContext) : base(theInfo, theContext)
        {
        }
        /// <summary>
        /// Serialization function
        /// </summary>
        public override void GetObjectData(SerializationInfo theInfo, StreamingContext theContext)
        {
            base.GetObjectData(theInfo, theContext);
        }
    }

    [Serializable]
    public class PipeMessageUserRenamed : PipeMessage
    {
        /// <summary>
        /// New name for the user
        /// </summary>
        public string NewName { get; set; } = string.Empty;
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeMessageUserRenamed(int theID, string theNewName) : base()
        {
            ID = theID;
            NewName = theNewName;
        }
        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public PipeMessageUserRenamed(SerializationInfo theInfo, StreamingContext theContext) : base(theInfo, theContext)
        {
            NewName = (string)theInfo.GetValue("newname", typeof(string));
        }
        /// <summary>
        /// Serialization function
        /// </summary>
        public override void GetObjectData(SerializationInfo theInfo, StreamingContext theContext)
        {
            base.GetObjectData(theInfo, theContext);
            theInfo.AddValue("newname", NewName, typeof(string));
        }
    }

    [Serializable]
    public class PipeMessageUserLogin : PipeMessage
    {
        public string Name { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeMessageUserLogin(string theName) : base()
        {
            Name = theName;
        }
        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public PipeMessageUserLogin(SerializationInfo theInfo, StreamingContext theContext) : base(theInfo, theContext)
        {
            Name = (string)theInfo.GetValue("name", typeof(string));
            Success = (bool)theInfo.GetValue("success", typeof(bool));
        }
        /// <summary>
        /// Serialization function
        /// </summary>
        public override void GetObjectData(SerializationInfo theInfo, StreamingContext theContext)
        {
            base.GetObjectData(theInfo, theContext);
            theInfo.AddValue("name", Name, typeof(string));
            theInfo.AddValue("success", Success, typeof(bool));
        }
    }

    [Serializable]
    public class PipeMessageUserEntersWorld : PipeMessage
    {
        public User User { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeMessageUserEntersWorld(User theUser) : base()
        {
            User = theUser;
        }
        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public PipeMessageUserEntersWorld(SerializationInfo theInfo, StreamingContext theContext) : base(theInfo, theContext)
        {
            User = (User)theInfo.GetValue("user", typeof(User));
        }
        /// <summary>
        /// Serialization function
        /// </summary>
        public override void GetObjectData(SerializationInfo theInfo, StreamingContext theContext)
        {
            base.GetObjectData(theInfo, theContext);
            theInfo.AddValue("user", User, typeof(User));
        }
    }

    [Serializable]
    public class PipeMessageNumberGame : PipeMessage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeMessageNumberGame() : base()
        {
        }
        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public PipeMessageNumberGame(SerializationInfo theInfo, StreamingContext theContext) : base(theInfo, theContext)
        {
        }
    }
    [Serializable]
    public class PipeMessageNumberGameAdd : PipeMessageNumberGame
    {
        public int NumberAdd { get; set; } = 0;
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeMessageNumberGameAdd(int theNumberToAdd) : base()
        {
            NumberAdd = theNumberToAdd;
        }
        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public PipeMessageNumberGameAdd(SerializationInfo theInfo, StreamingContext theContext) : base(theInfo, theContext)
        {
            NumberAdd = (int)theInfo.GetValue("numberadd", typeof(int));
        }
        /// <summary>
        /// Serialization function
        /// </summary>
        public override void GetObjectData(SerializationInfo theInfo, StreamingContext theContext)
        {
            base.GetObjectData(theInfo, theContext);
            theInfo.AddValue("numberadd", NumberAdd, typeof(int));
        }
    }
    [Serializable]
    public class PipeMessageNumberGameUpdate : PipeMessageNumberGame
    {
        public int NumberUpdate { get; set; } = 0;
        public PipeMessageNumberGameUpdate(int theNumberUpdate) : base()
        {
            NumberUpdate = theNumberUpdate;
        }
        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public PipeMessageNumberGameUpdate(SerializationInfo theInfo, StreamingContext theContext) : base(theInfo, theContext)
        {
            NumberUpdate = (int)theInfo.GetValue("numberupdate", typeof(int));
        }
        /// <summary>
        /// Serialization function
        /// </summary>
        public override void GetObjectData(SerializationInfo theInfo, StreamingContext theContext)
        {
            base.GetObjectData(theInfo, theContext);
            theInfo.AddValue("numberupdate", NumberUpdate, typeof(int));
        }
    }






    [Serializable]
    public class PipeMessageAAA : PipeMessage
    {
        int AAA = 0;
        /// <summary>
        /// Constructor
        /// </summary>
        public PipeMessageAAA(int theAAA) : base()
        {
            AAA = theAAA;
        }
        /// <summary>
        /// Deserialization constructor
        /// </summary>
        public PipeMessageAAA(SerializationInfo theInfo, StreamingContext theContext) : base(theInfo, theContext)
        {
            AAA = (int)theInfo.GetValue("aaa", typeof(int));
        }
        /// <summary>
        /// Serialization function
        /// </summary>
        public override void GetObjectData(SerializationInfo theInfo, StreamingContext theContext)
        {
            base.GetObjectData(theInfo, theContext);
            theInfo.AddValue("aaa", AAA, typeof(int));
        }
    }


    [Serializable]
    public struct User
    {
        public int ID;
        public string Name;
        public User(int theID, string theName)
        {
            this.ID = theID;
            this.Name = theName;
        }
    }

    public class PipeMessageEventArgs : EventArgs
    {
        new public string ToString() => PipeMessage.ToString();
        public PipeMessage PipeMessage { get; set; }
        public PipeMessageEventArgs(PipeMessage thePipeMessage)
        {
            PipeMessage = thePipeMessage;
        }
    }

    public interface IAcceptsPipeMessages
    {
        void HandleMessage(PipeMessage thePipeMessage);
    }
}