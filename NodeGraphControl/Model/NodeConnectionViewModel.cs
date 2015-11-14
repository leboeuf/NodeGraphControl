using System;
using NodeGraphControl.Utils;

namespace NodeGraphControl.Model
{
    /// <summary>
    /// Defines a connection between two connectors (connection points) of two nodes.
    /// </summary>
    public class NodeConnectionViewModel : AbstractModelBase
    {
        /// <summary>
        /// The source connector the connection is attached to.
        /// </summary>
        private NodeConnectorViewModel sourceConnector = null;

        /// <summary>
        /// The destination connector the connection is attached to.
        /// </summary>
        private NodeConnectorViewModel destinationConnector = null;

        /// <summary>
        /// The source connector the connection is attached to.
        /// </summary>
        public NodeConnectorViewModel SourceConnector
        {
            get
            {
                return sourceConnector;
            }
            set
            {
                if (sourceConnector == value)
                {
                    return;
                }

                if (sourceConnector != null)
                {
                    sourceConnector.AttachedConnections.Remove(this);
                    //sourceConnector.HotspotUpdated -= new EventHandler<EventArgs>(sourceConnector_HotspotUpdated);
                }

                sourceConnector = value;

                if (sourceConnector != null)
                {
                    sourceConnector.AttachedConnections.Add(this);
                    //sourceConnector.HotspotUpdated += new EventHandler<EventArgs>(sourceConnector_HotspotUpdated);
                    //this.SourceConnectorHotspot = sourceConnector.Hotspot;
                }

                OnPropertyChanged("SourceConnector");
                OnConnectionChanged();
            }
        }

        /// <summary>
        /// The destination connector the connection is attached to.
        /// </summary>
        public NodeConnectorViewModel DestinationConnector
        {
            get
            {
                return destinationConnector;
            }
            set
            {
                if (destinationConnector == value)
                {
                    return;
                }

                if (destinationConnector != null)
                {
                    destinationConnector.AttachedConnections.Remove(this);
                    //destinationConnector.HotspotUpdated -= new EventHandler<EventArgs>(destConnector_HotspotUpdated);
                }

                destinationConnector = value;

                if (destinationConnector != null)
                {
                    destinationConnector.AttachedConnections.Add(this);
                    //destinationConnector.HotspotUpdated += new EventHandler<EventArgs>(destConnector_HotspotUpdated);
                    //this.DestConnectorHotspot = destinationConnector.Hotspot;
                }

                OnPropertyChanged("DestinationConnector");
                OnConnectionChanged();
            }
        }

        /// <summary>
        /// Event fired when the connection has changed.
        /// </summary>
        public event EventHandler<EventArgs> ConnectionChanged;

        /// <summary>
        /// Raises the 'ConnectionChanged' event.
        /// </summary>
        private void OnConnectionChanged()
        {
            if (ConnectionChanged != null)
            {
                ConnectionChanged(this, EventArgs.Empty);
            }
        }
    }
}
