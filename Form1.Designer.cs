﻿using pctv.controls;

namespace pctv
{
    partial class Window
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            tableLayoutPanel1 = new TableLayoutPanel();
            channelPanel = new FlowLayoutPanel();
            videoPanel = new Panel();
            statusLabel = new Label();
            videoView = new LibVLCSharp.WinForms.VideoView();
            toolPanel = new TableLayoutPanel();
            rightToolPanel = new FlowLayoutPanel();
            changeSourceButton = new ThemedButton();
            volumeBar = new TrackBar();
            leftToolPanel = new FlowLayoutPanel();
            allButton = new ThemedButton();
            favoritesButton = new ThemedButton();
            addFavoriteButton = new ThemedButton();
            channelLabel = new Label();
            toolTip = new ToolTip(components);
            tableLayoutPanel1.SuspendLayout();
            videoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)videoView).BeginInit();
            toolPanel.SuspendLayout();
            rightToolPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)volumeBar).BeginInit();
            leftToolPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(channelPanel, 0, 1);
            tableLayoutPanel1.Controls.Add(videoPanel, 1, 1);
            tableLayoutPanel1.Controls.Add(toolPanel, 1, 0);
            tableLayoutPanel1.Controls.Add(channelLabel, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(984, 511);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // channelPanel
            // 
            channelPanel.Dock = DockStyle.Fill;
            channelPanel.FlowDirection = FlowDirection.TopDown;
            channelPanel.Location = new Point(4, 35);
            channelPanel.Name = "channelPanel";
            channelPanel.Size = new Size(194, 473);
            channelPanel.TabIndex = 0;
            channelPanel.WrapContents = false;
            // 
            // videoPanel
            // 
            videoPanel.Controls.Add(statusLabel);
            videoPanel.Controls.Add(videoView);
            videoPanel.Dock = DockStyle.Fill;
            videoPanel.Location = new Point(205, 35);
            videoPanel.Name = "videoPanel";
            videoPanel.Size = new Size(775, 473);
            videoPanel.TabIndex = 4;
            // 
            // statusLabel
            // 
            statusLabel.Location = new Point(0, 0);
            statusLabel.Name = "statusLabel";
            statusLabel.Padding = new Padding(3);
            statusLabel.Size = new Size(57, 21);
            statusLabel.TabIndex = 1;
            statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            statusLabel.Visible = false;
            // 
            // videoView
            // 
            videoView.BackColor = Color.Black;
            videoView.Dock = DockStyle.Fill;
            videoView.Location = new Point(0, 0);
            videoView.MediaPlayer = null;
            videoView.Name = "videoView";
            videoView.Size = new Size(775, 473);
            videoView.TabIndex = 0;
            videoView.Text = "videoView1";
            // 
            // toolPanel
            // 
            toolPanel.ColumnCount = 2;
            toolPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            toolPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            toolPanel.Controls.Add(rightToolPanel, 1, 0);
            toolPanel.Controls.Add(leftToolPanel, 0, 0);
            toolPanel.Dock = DockStyle.Fill;
            toolPanel.Location = new Point(202, 1);
            toolPanel.Margin = new Padding(0);
            toolPanel.Name = "toolPanel";
            toolPanel.RowCount = 1;
            toolPanel.RowStyles.Add(new RowStyle());
            toolPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            toolPanel.Size = new Size(781, 30);
            toolPanel.TabIndex = 6;
            // 
            // rightToolPanel
            // 
            rightToolPanel.Controls.Add(changeSourceButton);
            rightToolPanel.Controls.Add(volumeBar);
            rightToolPanel.Dock = DockStyle.Fill;
            rightToolPanel.FlowDirection = FlowDirection.RightToLeft;
            rightToolPanel.Location = new Point(390, 0);
            rightToolPanel.Margin = new Padding(0);
            rightToolPanel.Name = "rightToolPanel";
            rightToolPanel.Size = new Size(391, 100);
            rightToolPanel.TabIndex = 0;
            rightToolPanel.WrapContents = false;
            // 
            // changeSourceButton
            // 
            changeSourceButton.BackColor = Color.LightGray;
            changeSourceButton.FlatStyle = FlatStyle.Flat;
            changeSourceButton.Location = new Point(313, 3);
            changeSourceButton.Name = "changeSourceButton";
            changeSourceButton.Size = new Size(75, 23);
            changeSourceButton.TabIndex = 0;
            changeSourceButton.Text = "Source";
            toolTip.SetToolTip(changeSourceButton, "Change playlist source");
            changeSourceButton.UseVisualStyleBackColor = true;
            changeSourceButton.Click += changeSourceButton_Click;
            // 
            // volumeBar
            // 
            volumeBar.Location = new Point(203, 3);
            volumeBar.Maximum = 100;
            volumeBar.Name = "volumeBar";
            volumeBar.Size = new Size(104, 45);
            volumeBar.TabIndex = 0;
            volumeBar.TickFrequency = 10;
            toolTip.SetToolTip(volumeBar, "Adjust volume");
            volumeBar.ValueChanged += volumeBar_ValueChanged;
            // 
            // leftToolPanel
            // 
            leftToolPanel.Controls.Add(allButton);
            leftToolPanel.Controls.Add(favoritesButton);
            leftToolPanel.Controls.Add(addFavoriteButton);
            leftToolPanel.Dock = DockStyle.Fill;
            leftToolPanel.Location = new Point(0, 0);
            leftToolPanel.Margin = new Padding(0);
            leftToolPanel.Name = "leftToolPanel";
            leftToolPanel.Size = new Size(390, 100);
            leftToolPanel.TabIndex = 1;
            leftToolPanel.WrapContents = false;
            // 
            // allButton
            // 
            allButton.BackColor = Color.LightGray;
            allButton.FlatStyle = FlatStyle.Flat;
            allButton.Location = new Point(3, 3);
            allButton.Name = "allButton";
            allButton.Size = new Size(75, 23);
            allButton.TabIndex = 2;
            allButton.Text = "All";
            toolTip.SetToolTip(allButton, "Show all channels");
            allButton.UseVisualStyleBackColor = true;
            allButton.Click += allButton_Click;
            // 
            // favoritesButton
            // 
            favoritesButton.BackColor = Color.LightGray;
            favoritesButton.FlatStyle = FlatStyle.Flat;
            favoritesButton.Location = new Point(84, 3);
            favoritesButton.Name = "favoritesButton";
            favoritesButton.Size = new Size(75, 23);
            favoritesButton.TabIndex = 1;
            favoritesButton.Text = "Favorites";
            toolTip.SetToolTip(favoritesButton, "Show favorite channels");
            favoritesButton.UseVisualStyleBackColor = true;
            favoritesButton.Click += favoritesButton_Click;
            // 
            // addFavoriteButton
            // 
            addFavoriteButton.BackColor = Color.LightGray;
            addFavoriteButton.FlatStyle = FlatStyle.Flat;
            addFavoriteButton.Location = new Point(165, 3);
            addFavoriteButton.Name = "addFavoriteButton";
            addFavoriteButton.Size = new Size(49, 23);
            addFavoriteButton.TabIndex = 3;
            addFavoriteButton.Text = "+";
            toolTip.SetToolTip(addFavoriteButton, "Favorite this channel");
            addFavoriteButton.UseVisualStyleBackColor = true;
            addFavoriteButton.Click += addFavoriteButton_Click;
            // 
            // channelLabel
            // 
            channelLabel.AutoSize = true;
            channelLabel.Dock = DockStyle.Fill;
            channelLabel.Location = new Point(4, 1);
            channelLabel.Name = "channelLabel";
            channelLabel.Size = new Size(194, 30);
            channelLabel.TabIndex = 7;
            channelLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Window
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 511);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Name = "Window";
            Text = "PCTV";
            FormClosing += Window_FormClosing;
            Shown += Window_Shown;
            KeyUp += Window_KeyUp;
            Resize += Window_Resize;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            videoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)videoView).EndInit();
            toolPanel.ResumeLayout(false);
            rightToolPanel.ResumeLayout(false);
            rightToolPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)volumeBar).EndInit();
            leftToolPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel channelPanel;
        private Panel videoPanel;
        private LibVLCSharp.WinForms.VideoView videoView;
        private Label statusLabel;
        private TrackBar volumeBar;
        private TableLayoutPanel toolPanel;
        private FlowLayoutPanel rightToolPanel;
        private FlowLayoutPanel leftToolPanel;
        private ThemedButton changeSourceButton;
        private ThemedButton favoritesButton;
        private Label channelLabel;
        private ThemedButton allButton;
        private ToolTip toolTip;
        private ThemedButton addFavoriteButton;
    }
}
