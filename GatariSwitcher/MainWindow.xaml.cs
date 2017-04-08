using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GatariSwitcher
{
    public partial class MainWindow : Window
    {
        bool certStatus = false;
        bool servStatus = false;

        public MainWindow()
        {
            InitializeComponent();
            CheckStatus();
        }

        private async void CheckStatus()
        {
            var manager = new CertificateManager();
            certStatus = await manager.GetStatus();
            var switcher = new ServerSwitcher();
            servStatus = switcher.GetCurrentServer();

            statusLabel.Content = servStatus ? "Вы играете на гатарях с кентами!" : "Вы играете на офе с чертями!";
            switchButton.Content = servStatus ? "Перейти на официальный сервер" : "Перейти на гатари";
            certButton.Content = certStatus ? "Удалить сертификат" : "Установить сертификат";
        }

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void switchButton_Click(object sender, RoutedEventArgs e)
        {
            var switcher = new ServerSwitcher();
            if (servStatus)
            {
                try
                {
                    switcher.SwitchToOfficial();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    switcher.SwitchToGatari();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            CheckStatus();
        }

        private void sertButton_Click(object sender, RoutedEventArgs e)
        {
            var manager = new CertificateManager();
            if (certStatus)
            {
                try
                {
                    manager.Uninstall();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    manager.Install();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            CheckStatus();
        }

        private void websiteText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://osu.gatari.pw");
        }
    }
}
