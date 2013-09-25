using System.Windows;
using System.Windows.Controls;

namespace Notificator.Controls
{
    /// <summary>
    /// The notifications list control
    /// </summary>
    public partial class NotificationsControl : ItemsControl
    {
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
