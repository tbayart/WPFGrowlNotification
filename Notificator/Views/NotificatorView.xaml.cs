using System.Windows;
using System.Windows.Input;
using Notificator.Controls;

namespace Notificator.Views
{
    internal partial class NotificatorView
    {
        #region Ctor
        public NotificatorView(ICommand removeNotificationCommand)
        {
            RemoveNotificationCommand = removeNotificationCommand;
            AddHandler(NotificationItem.RemoveEvent, (RoutedEventHandler)RemoveNotificationHandler);
            InitializeComponent();
            Show();
        }
        #endregion Ctor

        #region Properties
        public ICommand RemoveNotificationCommand { get; private set; }
        #endregion Properties

        #region Methods
        public void RemoveNotificationHandler(object sender, RoutedEventArgs e)
        {
            NotificationItem item = e.OriginalSource as NotificationItem;
            if (item != null)
                RemoveNotificationCommand.Execute(item.DataContext);
        }

        protected override void OnStateChanged(System.EventArgs e)
        {
            base.OnStateChanged(e);

            // force maximized state
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
        }
        #endregion Methods
    }
}
