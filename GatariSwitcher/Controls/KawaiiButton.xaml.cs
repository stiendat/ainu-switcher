using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GatariSwitcher.Controls
{
    public partial class KawaiiButton : UserControl
    {
        public KawaiiButton()
        {
            InitializeComponent();
        }

        static DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(object), typeof(KawaiiButton));

        public object Caption
        {
            get
            {
                return GetValue(CaptionProperty);
            }
            set
            {
                SetValue(CaptionProperty, value);
            }
        }


        private void layoutGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            backgroundRectangle.Fill = new SolidColorBrush(Color.FromArgb(0xFF, 0x1A, 0x1A, 0x1A));
            textLabel.Foreground = Brushes.White;
        }

        private void layoutGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            backgroundRectangle.Fill = Brushes.White;
            textLabel.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1A, 0x1A, 0x1A));
        }
    }
}
