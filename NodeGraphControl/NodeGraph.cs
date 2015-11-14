using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace NodeGraphControl
{
    public class NodeGraph : Control
    {
        public static readonly DependencyProperty NodesSourceProperty =
            DependencyProperty.Register("NodesSource", typeof(IEnumerable), typeof(NodeGraph),
                new FrameworkPropertyMetadata(NodesSource_PropertyChanged));

        static NodeGraph()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodeGraph), new FrameworkPropertyMetadata(typeof(NodeGraph)));
        }

        /// <summary>
        /// A reference to the source collection used to populate 'Nodes'.
        /// Used in the same way as 'ItemsSource' in 'ItemsControl'.
        /// </summary>
        public IEnumerable NodesSource
        {
            get
            {
                return (IEnumerable)GetValue(NodesSourceProperty);
            }
            set
            {
                SetValue(NodesSourceProperty, value);
            }
        }

        /// <summary>
        /// Event raised when a new collection has been assigned to the 'NodesSource' property.
        /// </summary>
        private static void NodesSource_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }


    }
}
