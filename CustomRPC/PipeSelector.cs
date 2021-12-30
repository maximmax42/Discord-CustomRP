using DiscordRPC;
using System;
using System.Windows.Forms;

namespace CustomRPC
{
    public partial class PipeSelector : Form
    {
        DiscordRpcClient client;

        public PipeSelector()
        {
            InitializeComponent();

            TestConnection();
        }

        /// <summary>
        /// Tests the connection with the chosen pipe.
        /// </summary>
        private void TestConnection()
        {
            if (client != null && !client.IsDisposed)
                client.Dispose();

            client = new DiscordRpcClient("896771305108553788", (int)numericUpDownPipe.Value);
            client.OnReady += ConnectionSuccessful;
            client.OnConnectionFailed += ConnectionFailed;
            client.Initialize();
        }

        /// <summary>
        /// Called on successful connection to Discord.
        /// </summary>
        private void ConnectionSuccessful(object sender, DiscordRPC.Message.ReadyMessage args)
        {
            try
            {
                Invoke(new MethodInvoker(() =>
                {
                    pictureBoxAvatar.ImageLocation = client.CurrentUser.GetAvatarURL(User.AvatarFormat.PNG);
                    labelUsername.Text = client.CurrentUser.ToString().Replace("#", "\n#");
                }));
            }
            catch
            {
                // Form has been closed, just do nothing
            }

            client.Dispose();
        }

        /// <summary>
        /// Called on unsuccessful connection to Discord.
        /// </summary>
        private void ConnectionFailed(object sender, DiscordRPC.Message.ConnectionFailedMessage args)
        {
            try
            {
                Invoke(new MethodInvoker(() =>
                {
                    pictureBoxAvatar.ImageLocation = "https://cdn.discordapp.com/embed/avatars/4.png";
                    labelUsername.Text = "Can't connect.";
                }));
            }
            catch
            {
                // Form has been closed, just do nothing
            }

            client.Dispose();
        }

        /// <summary>
        /// Called when pipe number is changed via <see cref="numericUpDownPipe"/>.
        /// </summary>
        private void PipeChanged(object sender, EventArgs e)
        {
            TestConnection();
            pictureBoxAvatar.ImageLocation = "https://cdn.discordapp.com/embed/avatars/1.png";
            labelUsername.Text = "Connecting...";
        }

    }
}