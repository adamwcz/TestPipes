using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TestPipesDLL;

namespace TestPipesClient.Forms
{
    public partial class UserListForm : PipeGameBaseForm
    {
        private List<User> OnlineUsers = new List<User>();
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="thePipeClient">The local InternalPipeClient</param>
        public UserListForm(FormClient theFormClient, InternalPipeClient thePipeClient) : base(theFormClient, thePipeClient)
        {
            InitializeComponent();
        }
        /// <summary>
        /// Handles a received PipeMessage
        /// </summary>
        /// <param name="thePipeMessage">The received PipeMessage</param>
        public override void HandleMessage(PipeMessage thePipeMessage)
        {
            switch (thePipeMessage)
            {
                case PipeMessageUserList aPipeMessageUserList:
                    SetList(aPipeMessageUserList.UserList);
                    break;

                case PipeMessageUserEntersWorld aPipeMessageUserEnters:
                    FormClient.AddChatWindowLine(string.Format("[{0} Connected]", aPipeMessageUserEnters.User.Name));
                    AddUser(aPipeMessageUserEnters.User);
                    break;

                case PipeMessageUserDisconnection aPipeMessageDisconnection:
                    FormClient.AddChatWindowLine(string.Format("[{0} Disconnected]", aPipeMessageDisconnection.Name));
                    RemoveUser(new User(aPipeMessageDisconnection.ID, aPipeMessageDisconnection.Name));
                    break;

                case PipeMessageUserRenamed aPipeMessageUserRenamed:
                    RenameUser(new User(aPipeMessageUserRenamed.ID, aPipeMessageUserRenamed.NewName));
                    // If this is the user being renamed, update their local name
                    if (aPipeMessageUserRenamed.ID == PipeClient.ID)
                    {
                        PipeClient.Username = aPipeMessageUserRenamed.NewName;
                        FormClient.SetName(aPipeMessageUserRenamed.NewName);
                    }
                    break;
            }
        }
        /// <summary>
        /// Add a user to the user list
        /// </summary>
        /// <param name="theUser">User to add</param>
        private void AddUser(User theUser)
        {
            OnlineUsers.Add(theUser);
            this?.Invoke(new MethodInvoker(delegate
            {
                this.UserListBox.Items.Add(theUser.Name);
            }));
        }
        /// <summary>
        /// Remove a user from the user list based on their ID
        /// </summary>
        /// <param name="theUser">User to remove (matches by ID)</param>
        private void RemoveUser(User theUser)
        {
            // If a user with the given ID exists in the list, remove it
            if (OnlineUsers.FirstOrDefault(x => x.ID == theUser.ID) is User aRemovingUser)
            {
                OnlineUsers.Remove(aRemovingUser);
                this?.Invoke(new MethodInvoker(delegate
                {
                    this.UserListBox.Items.Remove(aRemovingUser.Name);
                }));
            }
        }
        /// <summary>
        /// Rename a user from the list. 
        /// </summary>
        /// <param name="theUser">New name and ID of user to rename.</param>
        private void RenameUser(User theUser)
        {
            this?.Invoke(new MethodInvoker(delegate
            {
                RemoveUser(theUser);
                AddUser(theUser);
            }));
        }
        /// <summary>
        /// Initializes the user list when joining for the first time
        /// </summary>
        /// <param name="theUserList">List of users received from server</param>
        private void SetList(List<User> theUserList)
        {
            this?.Invoke(new MethodInvoker(delegate
            {
                OnlineUsers.Clear();
                this.UserListBox.Items.Clear();
            }));
            foreach (User user in theUserList)
                AddUser(user);
        }
    }
}