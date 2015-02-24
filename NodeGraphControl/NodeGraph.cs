using System.Windows;
using System.Windows.Controls;

namespace NodeGraphControl
{
    public class NodeGraph : Control
    {
        static NodeGraph()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodeGraph), new FrameworkPropertyMetadata(typeof(NodeGraph)));
        }
    }
}
