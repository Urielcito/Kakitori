using KakitoriApp.Services;
using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows;

namespace KakitoriApp.View
{
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private bool _forceClose = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTrayIcon();
            this.Visibility = Visibility.Hidden;
        }

        private void InitializeTrayIcon()
        {
            _notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Text = "Kakitori",
                Icon = new System.Drawing.Icon("TheAppIcon.ico"),
                Visible = true
            };

            var contextMenu = new System.Windows.Forms.ContextMenuStrip();

            var menuItemOpen = new System.Windows.Forms.ToolStripMenuItem("Open");
            menuItemOpen.Image = Image.FromFile("TheAppIcon.ico");

            var menuItemClose = new System.Windows.Forms.ToolStripMenuItem("Close");
            menuItemClose.Image = Image.FromFile("TheAppIcon.ico");

            menuItemOpen.Click += NotifyIcon_Open;
            menuItemClose.Click += NotifyIcon_Close;

            contextMenu.Items.Add(menuItemOpen);
            contextMenu.Items.Add(menuItemClose);

            _notifyIcon.ContextMenuStrip = contextMenu;
            _notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!_forceClose)
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                _notifyIcon.Dispose();
                _notifyIcon = null;
            }

            base.OnClosing(e);
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                this.Hide();
            }

            base.OnStateChanged(e);
        }

        private void OpenFromTray()
        {
            this.Show();
            this.WindowState = WindowState.Normal;
            this.Activate();
        }

        private void NotifyIcon_Open(object sender, EventArgs e)
        {
            OpenFromTray();
        }

        private void NotifyIcon_Close(object sender, EventArgs e)
        {
            _forceClose = true;
            this.Close();
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            OpenFromTray();
        }
    }
}
