using System.Windows;
using NodeGraphControl.Model;
using NodeGraphControl.Utils;

namespace NodeGraph.TestApplication
{
    public class MainWindowViewModel : AbstractModelBase
    {
        /// <summary>
        /// This is the network that is displayed in the window.
        /// It is the main part of the view-model.
        /// </summary>
        private NodeGraphViewModel network = null;

        public NodeGraphViewModel Network
        {
            get
            {
                return network;
            }
            set
            {
                network = value;

                OnPropertyChanged("Network");
            }
        }

        public MainWindowViewModel()
        {
            // test data
            var name = "Test node";
            var nodeLocation = new Point(100, 100);
            var node = new NodeViewModel(name);
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;

            node.InputConnectors.Add(new NodeConnectorViewModel("In1"));
            node.InputConnectors.Add(new NodeConnectorViewModel("In2"));
            node.OutputConnectors.Add(new NodeConnectorViewModel("Out1"));
            node.OutputConnectors.Add(new NodeConnectorViewModel("Out2"));

            Network.Nodes.Add(node);

        }
    }
}
