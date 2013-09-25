using System.Windows;
using WPFGrowlNotification.Models;

namespace WPFGrowlNotification
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Notificator.Notificator _notificator;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _notificator = new Notificator.Notificator();
            _notificator.Owner = this;
        }

        private void ButtonClick1(object sender, RoutedEventArgs e)
        {
            _notificator.AddNotification(new Notificator.Models.Notification
            {
                Data = "Une simple notification ne contient qu'un message."
            });
        }

        private void ButtonClick2(object sender, RoutedEventArgs e)
        {
            _notificator.AddNotification(new NotificationTitle
            {
                Title = "Titre de la notification",
                Data = "Une notification avec un titre et un message."
            });
        }

        private void ButtonClick3(object sender, RoutedEventArgs e)
        {
            _notificator.AddNotification(new NotificationImage
            {
                ImageUrl = "pack://application:,,,/Resources/Radiation_warning_symbol.png"
            });
        }

        private void ButtonClick4(object sender, RoutedEventArgs e)
        {
            _notificator.AddNotification(new NotificationTitleImage
            {
                Title = "Notification avec titre et icône",
                ImageUrl = "pack://application:,,,/Resources/notification-icon.png",
                Data = "Exemple de notification plus complète. Cette fois-ci la notification possède un titre et une icône."
            });
        }
    }
}
