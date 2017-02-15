using System;
using System.Windows;
using System.Windows.Input;

namespace GatariSwitcher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ServerSwitcher switcher = new ServerSwitcher();
            modeButton.Caption = (switcher.GetCurrentServer()) ? "Перейти на оф с чертями" : "Перейти на гатари с кентами";
            titleLabel.Content = (switcher.GetCurrentServer()) ? "Вы играете на гатари с кентами" : "Вы играете на офе с чертями";

            CertificateManager cert = new CertificateManager();
            certificateButton.Caption = (cert.Status()) ? "Удалить сертификат" : "Установить сертификат";
        }

        private void title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void closeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void urlBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://osu.gatari.pw");
        }

        private void modeButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ServerSwitcher switcher = new ServerSwitcher();
            if (switcher.GetCurrentServer())
            {
                try
                {
                    switcher.SwitchToOfficial();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка!\n" + ex.Message);
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
                    MessageBox.Show("Произошла ошибка!\n" + ex.Message);
                }
            }

            bool status = switcher.GetCurrentServer();
            modeButton.Caption = (status) ? "Перейти на оф с чертями" : "Перейти на гатари с кентами";
            titleLabel.Content = (status) ? "Вы играете на гатари с кентами" : "Вы играете на офе с чертями";
        }

        private void certificateButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CertificateManager cert = new CertificateManager();
            if (cert.Status())
            {
                try
                {
                    cert.Uninstall();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!\n" + ex.Message);
                }
            }
            else
            {
                try
                {
                    cert.Install();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка!\n" + ex.Message);
                }
            }
            certificateButton.Caption = (cert.Status()) ? "Удалить сертификат" : "Установить сертификат";
        }
    }
}
