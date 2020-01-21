using System.Windows.Forms;
using TestPipesDLL;

namespace TestPipesClient
{
    public abstract class PipeGameBaseForm : Form, IAcceptsPipeMessages
    {
        /// <summary>
        /// The FormClient parent of this form
        /// </summary>
        protected FormClient FormClient { get; set; } = null;
        /// <summary>
        /// The local InternalPipeClient
        /// </summary>
        protected InternalPipeClient PipeClient { get; set; } = null;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="thePipeClient">The local InternalPipeClient</param>
        public PipeGameBaseForm(FormClient theFormClient, InternalPipeClient thePipeClient)
        {
            FormClient = theFormClient;
            PipeClient = thePipeClient;
            // Create the handle so we can make changes to the form before it is shown
            CreateHandle();
            this.FormClosing += OnFormClosing;
        }
        /// <summary>
        /// Event: Triggered when the form is closed (by clicking X button etc)
        /// </summary>
        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            // Hide the form and cancel the close event instead. This way it can still be updated and re-opened after "closing"
            this.Hide();
            e.Cancel = true;
        }

        public abstract void HandleMessage(PipeMessage thePipeMessage);
    }
}