using LibVLCSharp.Shared;
using pctv.controls;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace pctv
{
    public partial class Window : ThemedForm
    {
        private LibVLC libVLC;
        private MediaPlayer player;

        //constants
        private int channelButtonHeight = 24;
        private string offUrl = "pctv://off";
        private string version = "0.2.1";

        //parser may be null
        private Parser parser;

        private int scrolledChannel;
        private Channel selectedChannel;

        private bool favoritesOpen = false;

        public Window()
        {
            InitializeComponent();
            this.Text = "PCTV v" + version;
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
            switchChannel(new Channel(null, offUrl));

            toggleStatus(true, "Searching...");
            try
            {
                parser = null;
                parser = new Parser(Properties.Settings.Default.source);
                toggleStatus(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                toggleStatus(true, "No Signal!");
                MessageBox.Show("Failed to find channels! Make sure you're connected to the Internet and your source is valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            rePopulateChannels();
        }

        private void rePopulateChannels()
        {
            channelPanel.Controls.Clear();
            int maxChannels = channelPanel.Height / (channelButtonHeight + (3 * 2));
            if (!favoritesOpen)
            {
                if (parser != null)
                {
                    for (int i = scrolledChannel; i < scrolledChannel + maxChannels; i++)
                    {
                        if (i <= parser.channels.Count - 1)
                        {
                            Channel channel = parser.channels[i];
                            newChannelButton(channel, channelPanel.Width, onChannelClick);
                        }
                    }
                }
            }
            else
            {
                //favorites are open
                string[] favorites = Properties.Settings.Default.favorites.Split("\n");
                bool favoritesValid = false;

                for (int i = scrolledChannel; i < scrolledChannel + maxChannels; i++)
                {
                    if (i <= favorites.Length - 1)
                    {
                        string favorite = favorites[i];
                        string[] pair = favorite.Split(",");
                        if (pair.Length == 2)
                        {
                            favoritesValid = true;
                            Channel channel = new Channel(pair[0], pair[1]);
                            newChannelButton(channel, channelPanel.Width, onChannelClick);
                        }
                    }
                }

                if (!favoritesValid && scrolledChannel == 0)
                {
                    channelLabel.Text = "You have no favorites!";
                }
            }
        }

        private void newChannelButton(Channel channel, int width, EventHandler func)
        {
            ThemedButton newButton = new ThemedButton();
            newButton.Size = new Size(width - (channelPanel.Margin.All * 2), channelButtonHeight);
            newButton.Margin = new Padding(3);
            newButton.AutoEllipsis = true;
            newButton.TextAlign = ContentAlignment.MiddleLeft;
            newButton.Click += func;

            newButton.Tag = channel.url;
            newButton.Text = channel.title;
            channelPanel.Controls.Add(newButton);
        }

        private int findChannelIndex(string url)
        {
            for (int i = 0; i < parser.channels.Count; i++)
            {
                Channel channel = parser.channels[i];
                if (channel.url == url)
                {
                    return i;
                }
            }
            return -1;
        }

        private void addFavorite(Channel channel)
        {
            string[] favorites = Properties.Settings.Default.favorites.Split("\n");

            if (selectedChannel.url == offUrl)
            {
                return;
            }

            //make sure favorite doesn't already exist
            foreach (string favorite in favorites)
            {
                string[] pair = favorite.Split(",");
                if (pair.Length == 2)
                {
                    if (pair[1] == channel.url)
                    {
                        return;
                    }
                }
            }

            string[] newFavorites = new string[favorites.Length + 1];
            favorites.CopyTo(newFavorites, 0);
            newFavorites[newFavorites.Length - 1] = channel.title + "," + channel.url;

            Properties.Settings.Default.favorites = string.Join("\n", newFavorites);
        }

        private void toggleFavorites(bool on)
        {
            favoritesOpen = on;
            scrolledChannel = 0;
            channelLabel.Text = selectedChannel.title;
            rePopulateChannels();
        }

        private void switchChannel(Channel channel)
        {
            if (channel.title == null)
            {
                channel.title = "";
            }

            channelLabel.Text = channel.title;
            selectedChannel = channel;
            if (channel.url != offUrl)
            {
                toggleStatus(true, "Tuning...");

                player.Play(new Media(libVLC, channel.url, FromType.FromLocation));
            }
            else
            {
                playerStopAsync();
            }
        }

        private void toggleStatus(bool on, string message)
        {
            if (statusLabel.InvokeRequired)
            {
                statusLabel.Invoke(delegate
                {
                    toggleStatus(on, message);
                });
            }
            else
            {
                statusLabel.Text = message;
                centerStatusLabel();
                statusLabel.Visible = on;
            }
        }

        private void toggleStatus(bool state)
        {
            toggleStatus(state, "");
        }

        private void centerStatusLabel()
        {
            statusLabel.Location = new Point(
                statusLabel.Location.X,
                videoView.Location.Y + (videoView.Height / 2) - (statusLabel.Height / 2)
            );
            statusLabel.Width = videoView.Width;
        }

        private long getEpoch()
        {
            return (long)(DateTime.UtcNow - DateTime.MinValue).TotalMilliseconds;
        }

        private void onChannelClick(object sender, EventArgs e)
        {
            Button channel = (Button)sender;
            switchChannel(new Channel(channel.Text, (string)channel.Tag));
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
            if (dialog.Show("Type a M3U8 master playlist URL for the channel list to source from.", Properties.Settings.Default.source) == DialogResult.OK)
            {
                Properties.Settings.Default.source = dialog.input;
                toggleFavorites(false);
                reFindChannels();
            }
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            //save settings
            Properties.Settings.Default.volume = volumeBar.Value;
            Properties.Settings.Default.Save();
        }

        private void favoritesButton_Click(object sender, EventArgs e)
        {
            toggleFavorites(true);
        }

        private void allButton_Click(object sender, EventArgs e)
        {
            toggleFavorites(false);
        }

        private void addFavoriteButton_Click(object sender, EventArgs e)
        {
            addFavorite(selectedChannel);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            //handle debug menu
            if (e.KeyCode == Keys.D && e.Control && e.Shift)
            {
                InputDialog inputDialog = new InputDialog();
                DialogResult result = inputDialog.Show("Welcome to the debug menu! WARNING: Some debug commands could break your installation!", "",
                    new string[] {"reset", "crash"}
                );

                string input = inputDialog.input.ToLower();
                if (input == "reset")
                {
                    Properties.Settings.Default.Reset();
                    MessageBox.Show("Successfully reset settings!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
                else if (input == "crash")
                {
                    Process.GetCurrentProcess().Kill();
                }
            }
        }
    }
}
