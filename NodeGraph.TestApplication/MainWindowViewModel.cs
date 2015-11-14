using System.Windows;
using NodeGraphControl.Model;
using NodeGraphControl.Utils;

namespace NodeGraph.TestApplication
{
    public class MainWindowViewModel : AbstractModelBase
    {
        /// <summary>
        /// This is the Graph that is displayed in the window.
        /// It is the main part of the view-model.
        /// </summary>
        private NodeGraphViewModel graph = null;

        public NodeGraphViewModel Graph
        {
            get
            {
                return graph;
            }
            set
            {
                graph = value;

                OnPropertyChanged("Graph");
            }
        }

        public MainWindowViewModel()
        {
            // test data
            Graph = new NodeGraphViewModel();

            var name = "Test node";
            var nodeLocation = new Point(100, 100);
            var node = new NodeViewModel(name);
            node.X = nodeLocation.X;
            node.Y = nodeLocation.Y;

            node.InputConnectors.Add(new NodeConnectorViewModel("In1"));
            node.InputConnectors.Add(new NodeConnectorViewModel("In2"));
            node.OutputConnectors.Add(new NodeConnectorViewModel("Out1"));
            node.OutputConnectors.Add(new NodeConnectorViewModel("Out2"));

            Graph.Nodes.Add(node);
        }
    }
}
