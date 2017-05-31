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
        string gatariAddress = "";

        public MainWindow()
        {
            InitializeComponent();
            gatariAddress = GeneralHelper.GetGatariAddress(); //todo: < THIS SHOULD BE ASYNC
            CheckStatus();
        }

        private void CheckStatus()
        {
            Task.Run(async () => await CheckServerStatus());
            Task.Run(async () => await CheckCertStatus());
        }

        private async Task CheckServerStatus()
        {
            var switcher = new ServerSwitcher(gatariAddress);
            servStatus = switcher.GetCurrentServer();

            Dispatcher.Invoke(() =>
            {
                statusLabel.Content = servStatus ? "Вы играете на гатарях с кентами!" : "Вы играете на офе с чертями!";
                switchButton.Content = servStatus ? "Перейти на официальный сервер" : "Перейти на гатари";
            });
        }

        private async Task CheckCertStatus()
        {
            var manager = new CertificateManager();
            certStatus = await manager.GetStatus();

            Dispatcher.Invoke(() => certButton.Content = certStatus ? "Удалить сертификат" : "Установить сертификат");
        }

        //todo: fix this shit
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
            switchButton.IsEnabled = false;
            var switcher = new ServerSwitcher(gatariAddress);
            try
            {
                if (servStatus)
                {
                    switcher.SwitchToOfficial();
                }
                else
                {
                    switcher.SwitchToGatari();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            CheckStatus();
            switchButton.IsEnabled = true;
        }

        private void sertButton_Click(object sender, RoutedEventArgs e)
        {
            var manager = new CertificateManager();
            try
            {
                if (certStatus)
                {
                    manager.Uninstall();
                }
                else
                {
                    manager.Install();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            CheckStatus();
        }

        private void websiteText_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://osu.gatari.pw");
        }
    }
}
