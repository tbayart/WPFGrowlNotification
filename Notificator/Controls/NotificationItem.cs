using System.Windows;
using System.Windows.Controls;

namespace Notificator.Controls
{
    /// <summary>
    /// The notification visual element
    /// </summary>
    public class NotificationItem : ContentControl
    {
        #region DependencyProperties
        // Using a DependencyProperty as the backing store for Remove.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExpiredProperty =
            DependencyProperty.Register("Expired", typeof(bool), typeof(NotificationItem),
                new PropertyMetadata(false, OnExpiredPropertyChanged));

        private static void OnExpiredPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
                ((NotificationItem)sender).RaiseEvent(new RoutedEventArgs(RemoveEvent));
        }

        public bool Expired
        {
            get { return (bool)GetValue(ExpiredProperty); }
            set { SetValue(ExpiredProperty, value); }
        }
        #endregion DependencyProperties

        #region RoutedEvents
        // Create a custom routed event by first registering a RoutedEventID 
        // This event uses the bubbling routing strategy 
        public static readonly RoutedEvent RemoveEvent = EventManager.RegisterRoutedEvent(
            "Remove", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NotificationItem));

        // Provide CLR accessors for the event 
        public event RoutedEventHandler Remove
        {
            add { AddHandler(RemoveEvent, value); }
            remove { RemoveHandler(RemoveEvent, value); }
        }
        #endregion RoutedEvents

        #region Ctor
        public NotificationItem()
        {
            // ClickEvent is handled to close a notification by seeking animation storyboard
            AddHandler(NotificationCloseButton.ClickEvent, (RoutedEventHandler)HandleRoutedEvents);
        }
        #endregion Ctor

        #region Methods
        /// <summary>
        /// All routed events are handled by this unique method
        /// </summary>
        private void HandleRoutedEvents(object sender, RoutedEventArgs e)
        {
            if (e.RoutedEvent == Button.ClickEvent && e.OriginalSource is NotificationCloseButton)
                IsHitTestVisible = false;
        }
        #endregion Methods
    }
}
