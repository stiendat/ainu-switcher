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
        string gatariAddress = "173.212.240.174";

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                string newAddress = GeneralHelper.GetGatariAddress();
                gatariAddress = newAddress;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось получить адрес сервера:\r\n" + ex.Message);
            }
            CheckStatus();
        }

        private void CheckStatus()
        {
            CheckCertStatus();
            CheckServerStatus();
        }

        private void CheckServerStatus()
        {
            var switcher = new ServerSwitcher(gatariAddress);
            servStatus = false;
            try
            {
                servStatus = switcher.GetCurrentServer();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка получения текущего сервера:\r\n" + ex.Message);
            }
            statusLabel.Content = servStatus ? "Вы играете на гатарях с кентами!" : "Вы играете на офе с чертями!";
            switchButton.Content = servStatus ? "Перейти на официальный сервер" : "Перейти на гатари";
        }

        private void CheckCertStatus()
        {
            var manager = new CertificateManager();
            certStatus = manager.GetStatus();

            certButton.Content = certStatus ? "Удалить сертификат" : "Установить сертификат";
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
