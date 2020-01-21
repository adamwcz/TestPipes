using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TestPipesDLL;

namespace TestPipesClient
{
    public partial class ImageSharingForm : PipeGameBaseForm
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="thePipeClient">The local InternalPipeClient</param>
        public ImageSharingForm(FormClient theFormClient, InternalPipeClient thePipeClient) : base(theFormClient, thePipeClient)
        {
            InitializeComponent();
            this.itsLabelFileName.Text = string.Empty;
            this.itsLabelSenderName.Text = string.Empty;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Event: Triggered when the Choose Image button is clicked
        /// </summary>
        private void itsButtonChoose_Click(object sender, EventArgs e)
        {
            OpenFileDialog anOpenDialog = new OpenFileDialog()
            {
                Filter = "PNG (*.png)|*.png",
                Title = "Choose An Image To Send",
            };

            if (anOpenDialog.ShowDialog() == DialogResult.OK && File.Exists(anOpenDialog.FileName))
            {
                Image anImageToSend = Image.FromFile(anOpenDialog.FileName);
                string aFileSize = Utilities.GetFileSizeBytesReadable(anOpenDialog.FileName);
                PipeMessageImageShare aPipeMessageImageShare = new PipeMessageImageShare(anImageToSend, PipeClient.Username, Path.GetFileName(anOpenDialog.FileName), aFileSize);
                PipeClient.Send(aPipeMessageImageShare);

                HandleMessage(aPipeMessageImageShare);
            }
        }

        /// <summary>
        /// Event: Triggered when the Save As button is clicked
        private void itsButtonSaveAs_Click(object sender, EventArgs e)
        {
            if (itsPictureBox.Image is Image aCurrentImage)
            {
                SaveFileDialog aSaveDialog = new SaveFileDialog();
                aSaveDialog.Filter = "PNG (*.png)|*.png";
                if (aSaveDialog.ShowDialog() == DialogResult.OK)
                {
                    itsPictureBox.Image.Save(aSaveDialog.FileName);
                }
                else
                {
                    MessageBox.Show("cancel");
                }
            }
            else
            {
                MessageBox.Show("!!");
            }
        }

        /// <summary>
        /// Handles a received PipeMessage
        /// </summary>
        /// <param name="thePipeMessage">The received PipeMessage</param>
        public override void HandleMessage(PipeMessage thePipeMessage)
        {
            if (thePipeMessage is PipeMessageImageShare thePipeMessageImageShare)
            {
                this?.Invoke(new MethodInvoker(delegate
                {
                    // Set the text under the image
                    itsLabelSenderName.Text = "Shared by: " + thePipeMessageImageShare.ImageSenderName;
                    itsLabelFileName.Text = "Title: " + thePipeMessageImageShare.ImageName;
                    itsLabelFileSize.Text = thePipeMessageImageShare.FileSize;

                    // Set the SizeMode to Center if the image is smaller than the picture box, otherwise Zoom in order to shrink the image to fit
                    itsPictureBox.SizeMode = thePipeMessageImageShare.SharedImage.Width > itsPictureBox.Width || thePipeMessageImageShare.SharedImage.Height > itsPictureBox.Height ? PictureBoxSizeMode.Zoom : PictureBoxSizeMode.CenterImage;
                    itsPictureBox.Image = thePipeMessageImageShare.SharedImage;
                    if (!itsButtonSaveAs.Enabled)
                        itsButtonSaveAs.Enabled = true;
                }));
            }
        }
    }
}