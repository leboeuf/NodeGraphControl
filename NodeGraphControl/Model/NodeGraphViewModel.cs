using System.Collections.ObjectModel;

namespace NodeGraphControl.Model
{
    /// <summary>
    /// Defines a network of nodes and connections between the nodes.
    /// </summary>
    public class NodeGraphViewModel
    {
        /// <summary>
        /// The collection of nodes in the graph.
        /// </summary>
        private ObservableCollection<NodeViewModel> nodes = null;

        /// <summary>
        /// The collection of connections in the graph.
        /// </summary>
        private ObservableCollection<NodeConnectionViewModel> connections = null;
    }
}
