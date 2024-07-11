using LibVLCSharp.Shared;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace pctv
{
    public partial class Window : Form
    {
        private LibVLC libVLC;
        private MediaPlayer player;

        private Parser parser;
        private int channelButtonHeight = 24;
        private string offId = "PCTV.OFF";

        private int scrolledChannel;
        private string selectedChannelId;

        private long channelLastSwitched = 0;

        public Window()
        {
            InitializeComponent();
            libVLC = new LibVLC();
            player = new MediaPlayer(libVLC);
            videoView.MediaPlayer = player;

            player.Playing += Player_Playing;
            player.Stopped += Player_Stopped;
            player.EncounteredError += Player_EncounteredError;

            channelPanel.MouseWheel += new MouseEventHandler(onMouseWheel);

            volumeBar.Value = Properties.Settings.Default.volume;
            changeVolume(volumeBar.Value);
        }

        private void onPlayerStop()
        {
            toggleStatus(true, "No Signal!");
        }

        private void playerStopAsync()
        {
            Task.Run(() =>
            {
                player.Stop();
            });
        }

        private void Player_Stopped(object? sender, EventArgs e)
        {
            onPlayerStop();
        }

        private void Player_EncounteredError(object? sender, EventArgs e)
        {
            onPlayerStop();
        }

        private void Player_Playing(object? sender, EventArgs e)
        {
            toggleStatus(false);
        }

        private void reFindChannels()
        {
            scrolledChannel = 0;
            selectedChannelId = offId;
            switchChannel(selectedChannelId);

            toggleStatus(true, "Searching...");
            try
            {
                parser = new Parser(Properties.Settings.Default.source);
                toggleStatus(false);
                rePopulateChannels();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                toggleStatus(true, "No Signal!");
                MessageBox.Show("Failed to find channels! Make sure you're connected to the Internet and your source is valid.", "Error");
            }
        }

        private void rePopulateChannels()
        {
            channelPanel.Controls.Clear();
            int maxChannels = channelPanel.Height / (channelButtonHeight + (3 * 2));
            for (int i = scrolledChannel; i < scrolledChannel + maxChannels; i++)
            {
                if (i < parser.channels.Count - 1)
                {
                    Channel channel = parser.channels[i];
                    Button newButton = new Button();
                    newButton.Size = new Size(channelPanel.Width - (channelPanel.Margin.All * 2), channelButtonHeight);
                    newButton.Margin = new Padding(3);
                    newButton.AutoEllipsis = true;
                    newButton.TextAlign = ContentAlignment.MiddleLeft;
                    newButton.Click += onChannelClick;

                    newButton.Tag = channel.id;
                    newButton.Text = formatChannelTitle(channel);

                    channelPanel.Controls.Add(newButton);
                }
            }
        }

        private int findChannelIndex(string id)
        {
            for (int i=0; i < parser.channels.Count; i++)
            {
                Channel channel= parser.channels[i];
                if (channel.id == id)
                {
                    return i;
                }
            }
            return -1;
        }

        private string formatChannelTitle(Channel channel)
        {
            return "[" + channel.id + "] " + channel.title;
        }

        private void switchChannel(string id)
        {
            if (id != offId)
            {
                int index = findChannelIndex(id);
                channelLabel.Text = parser.channels[index].title;
                channelIdBox.Text = parser.channels[index].id;
                selectedChannelId = id;
                toggleStatus(true, "Tuning...");

                channelLastSwitched = getEpoch();

                player.Play(new Media(libVLC, parser.channels[index].url, FromType.FromLocation));
            }
            else
            {
                channelLabel.Text = "";
                channelIdBox.Text = "";
                selectedChannelId = id;
                playerStopAsync();
            }
        }

        private void toggleStatus(bool state, string message)
        {
            if (statusLabel.InvokeRequired)
            {
                statusLabel.Invoke(delegate
                {
                    toggleStatus(state, message);
                });
            }
            else
            {
                statusLabel.Text = message;
                centerStatusLabel();
                statusLabel.Visible = state;
            }
        }

        private void toggleStatus(bool state)
        {
            toggleStatus(state, "");
        }

        private void centerStatusLabel()
        {
            statusLabel.Location = new Point(
                videoView.Location.X + (videoView.Width / 2) - (statusLabel.Width / 2),
                videoView.Location.Y + (videoView.Height / 2) - (statusLabel.Height / 2)
            );
        }

        private long getEpoch()
        {
            return (long) (DateTime.UtcNow - DateTime.MinValue).TotalMilliseconds;
        }

        private void onChannelClick(object sender, EventArgs e)
        {
            Button channel = (Button)sender;
            switchChannel((string) channel.Tag);
        }

        private void Window_Resize(object sender, EventArgs e)
        {
            if (statusLabel.Visible)
            {
                centerStatusLabel();
            }
            rePopulateChannels();
        }

        private void Window_Shown(object sender, EventArgs e)
        {
            //find channels after window shows for snappiness
            reFindChannels();
        }

        private void onMouseWheel(object sender, MouseEventArgs e)
        {
            //up
            if (e.Delta > 0)
            {
                if (scrolledChannel > 0)
                {
                    scrolledChannel--;
                }
            }
            else
            {
                //down
                if (scrolledChannel < parser.channels.Count - 1)
                {
                    scrolledChannel++;
                }
            }
            rePopulateChannels();
        }

        private void volumeBar_ValueChanged(object sender, EventArgs e)
        {
            changeVolume(volumeBar.Value);
        }

        private void changeVolume(int value)
        {
            player.Volume = value;
        }

        private void changeSourceButton_Click(object sender, EventArgs e)
        {
            InputDialog dialog = new InputDialog();
            if (dialog.Show("Type the full master M3U8 URL to source from.", Properties.Settings.Default.source) == DialogResult.OK)
            {
                Properties.Settings.Default.source = dialog.input;
                reFindChannels();
            }
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            //save settings
            Properties.Settings.Default.volume = volumeBar.Value;
            Properties.Settings.Default.Save();
        }
    }
}
