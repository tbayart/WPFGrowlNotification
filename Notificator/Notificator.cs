using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Framework.Commands;
using Notificator.Models;
using Notificator.Views;

namespace Notificator
{
    public class Notificator : DependencyObject
    {
        #region Fields
        private string _debugInfo = "Notifications:{0} Buffer:{1}";
        private static int DefaultMaxVisibleNotifications = 4;
        private static double DefaultWidth = 300;
        private static HorizontalAlignment DefaultHorizontalAlignment = HorizontalAlignment.Right;
        private static VerticalAlignment DefaultVerticalAlignment = VerticalAlignment.Bottom;
        private static Thickness DefaultMargin = new Thickness(0);

        private NotificatorView _notificationsView;
        private readonly ObservableCollection<Notification> _notifications = new ObservableCollection<Notification>();
        private Action<Notification> _addNotification;
        private readonly Queue<Notification> _buffer = new Queue<Notification>();
        #endregion Fields

        #region Ctor
        public Notificator()
        {
            // this will update _addNotification action
            OnVerticalAlignmentChanged();
            // create a command to allow NotificatorView removing a notification
            ICommand removeCommand = new DelegateCommand<Notification>(RemoveNotification);
            _notificationsView = new NotificatorView(removeCommand) { DataContext = this };
        }
        #endregion Ctor

        #region Properties
        // The list of visible notifications
        public IEnumerable<object> Notifications { get { return _notifications; } }

        public Window Owner
        {
            get { return _notificationsView.Owner; }
            set { _notificationsView.Owner = value; }
        }
        #endregion Properties

        #region Dependency Properties
        // Using a DependencyProperty as the backing store for MaxVisibleNotifications.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxVisibleNotificationsProperty =
            DependencyProperty.Register("MaxVisibleNotifications", typeof(int), typeof(Notificator),
                new PropertyMetadata(DefaultMaxVisibleNotifications));

        public int MaxVisibleNotifications
        {
            get { return (int)GetValue(MaxVisibleNotificationsProperty); }
            set { SetValue(MaxVisibleNotificationsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Width.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(Notificator),
                new PropertyMetadata(DefaultWidth));

        public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HorizontalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalAlignmentProperty =
            DependencyProperty.Register("HorizontalAlignment", typeof(HorizontalAlignment), typeof(Notificator),
                new PropertyMetadata(DefaultHorizontalAlignment));

        public HorizontalAlignment HorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalAlignmentProperty); }
            set { SetValue(HorizontalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VerticalAlignment.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalAlignmentProperty =
            DependencyProperty.Register("VerticalAlignment", typeof(VerticalAlignment), typeof(Notificator),
                new PropertyMetadata(DefaultVerticalAlignment, OnVerticalAlignmentPropertyChanged));

        private static void OnVerticalAlignmentPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((Notificator)sender).OnVerticalAlignmentChanged();
        }

        public VerticalAlignment VerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalAlignmentProperty); }
            set { SetValue(VerticalAlignmentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Margin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.Register("Margin", typeof(Thickness), typeof(Notificator),
                new PropertyMetadata(DefaultMargin));

        public Thickness Margin
        {
            get { return (Thickness)GetValue(MarginProperty); }
            set { SetValue(MarginProperty, value); }
        }
        #endregion Dependency Properties

        #region Methods
        /// <summary>
        /// If VerticalAlignment changes, we could have to change the notification add behavior
        /// </summary>
        private void OnVerticalAlignmentChanged()
        {
            if (VerticalAlignment == VerticalAlignment.Top)
                // with a top alignment, we just add a new notification at the bottom
                _addNotification = notif => _notifications.Add(notif);
            else
                // with a center or a bottom alignment, we add a new notification at the top (insert at 0) of the list
                _addNotification = notif => _notifications.Insert(0, notif);
        }

        /// <summary>
        /// Check the buffer for a notification to add
        /// </summary>
        private void CheckBuffer()
        {
            if (_buffer.Any() && _notifications.Count < MaxVisibleNotifications)
                _addNotification(_buffer.Dequeue());
        }

        /// <summary>
        /// Add a notification to the list of the buffer
        /// </summary>
        /// <param name="notification">The notification to add</param>
        public void AddNotification(Notification notification)
        {
            if (_notifications.Count < MaxVisibleNotifications)
                _addNotification(notification);
            else
                _buffer.Enqueue(notification);

            Console.WriteLine(string.Format(_debugInfo, _notifications.Count, _buffer.Count));
        }

        /// <summary>
        /// Remove a notification and check the buffer
        /// </summary>
        /// <param name="notification">The notification to remove</param>
        public void RemoveNotification(Notification notification)
        {
            if (Notifications.Contains(notification))
                _notifications.Remove(notification);

            CheckBuffer();

            Console.WriteLine(string.Format(_debugInfo, _notifications.Count, _buffer.Count));
        }

        public void Shutdown()
        {
            _notificationsView.Close();
        }
        #endregion Methods
    }
}
