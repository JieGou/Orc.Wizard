namespace Orc.Wizard;

using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

public class ScrollViewerMouseWheelBehavior : Behavior<ScrollViewer>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        if (AssociatedObject is not null)
        {
            AssociatedObject.PreviewMouseWheel += OnPreviewMouseWheel;
        }
    }

    protected override void OnDetaching()
    {
        if (AssociatedObject is not null)
        {
            AssociatedObject.PreviewMouseWheel -= OnPreviewMouseWheel;
        }

        base.OnDetaching();
    }

    private static void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        var scrollViewer = (ScrollViewer)sender;
        scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
        e.Handled = true;
    }
}
