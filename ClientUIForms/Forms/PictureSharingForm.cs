using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TestPipesDLL;

namespace ClientUIForms.Forms
{
   public partial class ImageSharingForm : PipeGameBaseForm
   {
      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="theChatWindow">The chat window to print info to</param>
      /// <param name="thePipeMessageSender">The PipeMessage sending interface</param>
      public ImageSharingForm(IClientChatWindow theChatWindow, IPipeMessageSender thePipeMessageSender) : base(theChatWindow, thePipeMessageSender)
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
            PipeMessageImageShare aPipeMessageImageShare = new PipeMessageImageShare(anImageToSend, Path.GetFileName(anOpenDialog.FileName), aFileSize);
            //OnSend(this, new PipeMessageEventArgs(aPipeMessageImageShare));
            PipeSender.SendPipeMessage(aPipeMessageImageShare);
            aPipeMessageImageShare.ImageSenderName = "Me";
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

            }
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