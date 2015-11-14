using System;
using NodeGraphControl.Model.Enums;
using NodeGraphControl.Utils;

namespace NodeGraphControl.Model
{
    /// <summary>
    /// Defines a connector (connection point) that can be attached to a node and is used to link the node to another node via a connection.
    /// </summary>
    public class NodeConnectorViewModel : AbstractModelBase
    {
        public NodeConnectorViewModel(string name)
        {
            this.Name = name;
            this.Type = ConnectorType.Undefined;
        }

        /// <summary>
        /// The name of the connector.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Defines the type of the connector.
        /// </summary>
        public ConnectorType Type
        {
            get;
            internal set;
        }

        /// <summary>
        /// The parent node that the connector is attached to, or null if the connector is not attached to any node.
        /// </summary>
        public NodeViewModel ParentNode
        {
            get;
            internal set;
        }

        /// <summary>
        /// The connections that are attached to this connector, or null if no connections are attached.
        /// </summary>
        private ImpObservableCollection<NodeConnectionViewModel> attachedConnections = null;

        /// <summary>
        /// The connections that are attached to this connector, or null if no connections are attached.
        /// </summary>
        public ImpObservableCollection<NodeConnectionViewModel> AttachedConnections
        {
            get
            {
                if (attachedConnections == null)
                {
                    attachedConnections = new ImpObservableCollection<NodeConnectionViewModel>();
                    attachedConnections.ItemsAdded += attachedConnections_ItemsAdded;
                    attachedConnections.ItemsRemoved += attachedConnections_ItemsRemoved;
                }

                return attachedConnections;
            }
        }

        /// <summary>
        /// Event raised to ensure that no connection is added to the list twice.
        /// </summary>
        private void attachedConnections_ItemsAdded(object sender, CollectionItemsChangedEventArgs e)
        {
            foreach (NodeConnectionViewModel connection in e.Items)
            {
                connection.ConnectionChanged += connection_ConnectionChanged;
            }

            if ((AttachedConnections.Count - e.Items.Count) == 0)
            {
                // The first connection has been added
                OnPropertyChanged("IsConnectionAttached");
                OnPropertyChanged("IsConnected");
            }
        }

        /// <summary>
        /// Event raised when connections have been removed from the connector.
        /// </summary>
        private void attachedConnections_ItemsRemoved(object sender, CollectionItemsChangedEventArgs e)
        {
            foreach (NodeConnectionViewModel connection in e.Items)
            {
                connection.ConnectionChanged -= connection_ConnectionChanged;
            }

            if (AttachedConnections.Count == 0)
            {
                // No longer connected to anything
                OnPropertyChanged("IsConnectionAttached");
                OnPropertyChanged("IsConnected");
            }
        }

        /// <summary>
        /// Event raised when a connection attached to the connector has changed.
        /// </summary>
        private void connection_ConnectionChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("IsConnectionAttached");
            OnPropertyChanged("IsConnected");
        }
    }
}
