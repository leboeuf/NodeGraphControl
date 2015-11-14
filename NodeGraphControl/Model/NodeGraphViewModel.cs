using NodeGraphControl.Utils;

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
        private ImpObservableCollection<NodeViewModel> nodes = null;

        /// <summary>
        /// The collection of connections in the graph.
        /// </summary>
        private ImpObservableCollection<NodeConnectionViewModel> connections = null;

        /// <summary>
        /// The collection of nodes in the network.
        /// </summary>
        public ImpObservableCollection<NodeViewModel> Nodes
        {
            get
            {
                if (nodes == null)
                {
                    nodes = new ImpObservableCollection<NodeViewModel>();
                }

                return nodes;
            }
        }

        /// <summary>
        /// The collection of connections in the network.
        /// </summary>
        public ImpObservableCollection<NodeConnectionViewModel> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new ImpObservableCollection<NodeConnectionViewModel>();
                    connections.ItemsRemoved += connections_ItemsRemoved;
                }

                return connections;
            }
        }

        /// <summary>
        /// Event raised then Connections have been removed.
        /// </summary>
        private void connections_ItemsRemoved(object sender, CollectionItemsChangedEventArgs e)
        {
            foreach (NodeConnectionViewModel connection in e.Items)
            {
                connection.SourceConnector = null;
                connection.DestinationConnector = null;
            }
        }
    }
}
