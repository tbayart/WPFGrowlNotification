using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notificator.Controls
{
    /// <summary>
    /// Interaction logic for NotificationsControl.xaml
    /// </summary>
    public partial class NotificationsControl : ItemsControl
    {
        public NotificationsControl()
        {
            InitializeComponent();
        }

        // Summary:
        //     Creates or identifies the element used to display a specified item.
        //
        // Returns:
        //     A System.Windows.Controls.ListBoxItem.
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NotificationItem();
        }

        //
        // Summary:
        //     Determines if the specified item is (or is eligible to be) its own ItemContainer.
        //
        // Parameters:
        //   item:
        //     Specified item.
        //
        // Returns:
        //     true if the item is its own ItemContainer; otherwise, false.
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is NotificationItem;
        }
    }
}
