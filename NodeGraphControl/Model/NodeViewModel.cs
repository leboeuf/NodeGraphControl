using NodeGraphControl.Model.Enums;
using NodeGraphControl.Utils;

namespace NodeGraphControl.Model
{
    public class NodeViewModel : AbstractModelBase
    {
        /// <summary>
        /// The name of the node.
        /// </summary>
        private string name = string.Empty;

        /// <summary>
        /// The name of the node.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name == value)
                {
                    return;
                }

                name = value;

                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// The X coordinate for the position of the node.
        /// </summary>
        private double x = 0;

        /// <summary>
        /// The Y coordinate for the position of the node.
        /// </summary>
        private double y = 0;

        /// <summary>
        /// The Z index of the node.
        /// </summary>
        private int zIndex = 0;

        /// <summary>
        /// List of input connectors (connections points) attached to the node.
        /// </summary>
        private ImpObservableCollection<NodeConnectorViewModel> inputConnectors = null;

        /// <summary>
        /// List of output connectors (connections points) attached to the node.
        /// </summary>
        private ImpObservableCollection<NodeConnectorViewModel> outputConnectors = null;

        public NodeViewModel()
        {
            
        }

        public NodeViewModel(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// List of input connectors (connections points) attached to the node.
        /// </summary>
        public ImpObservableCollection<NodeConnectorViewModel> InputConnectors
        {
            get
            {
                if (inputConnectors == null)
                {
                    inputConnectors = new ImpObservableCollection<NodeConnectorViewModel>();
                    inputConnectors.ItemsAdded += inputConnectors_ItemsAdded;
                    inputConnectors.ItemsRemoved += inputConnectors_ItemsRemoved;
                }

                return inputConnectors;
            }
        }

        /// <summary>
        /// List of output connectors (connections points) attached to the node.
        /// </summary>
        public ImpObservableCollection<NodeConnectorViewModel> OutputConnectors
        {
            get
            {
                if (outputConnectors == null)
                {
                    outputConnectors = new ImpObservableCollection<NodeConnectorViewModel>();
                    outputConnectors.ItemsAdded += outputConnectors_ItemsAdded;
                    outputConnectors.ItemsRemoved += outputConnectors_ItemsRemoved;
                }

                return outputConnectors;
            }
        }

        /// <summary>
        /// The X coordinate for the position of the node.
        /// </summary>
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                if (x == value)
                {
                    return;
                }

                x = value;

                OnPropertyChanged("X");
            }
        }

        /// <summary>
        /// The Y coordinate for the position of the node.
        /// </summary>
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                if (y == value)
                {
                    return;
                }

                y = value;

                OnPropertyChanged("Y");
            }
        }

        /// <summary>
        /// The Z index of the node.
        /// </summary>
        public int ZIndex
        {
            get
            {
                return zIndex;
            }
            set
            {
                if (zIndex == value)
                {
                    return;
                }

                zIndex = value;

                OnPropertyChanged("ZIndex");
            }
        }

        /// <summary>
        /// Event raised when connectors are added to the node.
        /// </summary>
        private void inputConnectors_ItemsAdded(object sender, CollectionItemsChangedEventArgs e)
        {
            foreach (NodeConnectorViewModel connector in e.Items)
            {
                connector.ParentNode = this;
                connector.Type = ConnectorType.Input;
            }
        }

        /// <summary>
        /// Event raised when connectors are removed from the node.
        /// </summary>
        private void inputConnectors_ItemsRemoved(object sender, CollectionItemsChangedEventArgs e)
        {
            foreach (NodeConnectorViewModel connector in e.Items)
            {
                connector.ParentNode = null;
                connector.Type = ConnectorType.Undefined;
            }
        }

        /// <summary>
        /// Event raised when connectors are added to the node.
        /// </summary>
        private void outputConnectors_ItemsAdded(object sender, CollectionItemsChangedEventArgs e)
        {
            foreach (NodeConnectorViewModel connector in e.Items)
            {
                connector.ParentNode = this;
                connector.Type = ConnectorType.Output;
            }
        }

        /// <summary>
        /// Event raised when connectors are removed from the node.
        /// </summary>
        private void outputConnectors_ItemsRemoved(object sender, CollectionItemsChangedEventArgs e)
        {
            foreach (NodeConnectorViewModel connector in e.Items)
            {
                connector.ParentNode = null;
                connector.Type = ConnectorType.Undefined;
            }
        }
    }
}
