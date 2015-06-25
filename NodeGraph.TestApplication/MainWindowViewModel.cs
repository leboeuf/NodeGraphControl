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
        private NodeGraphViewModel nodes = null;

        public NodeGraphViewModel Nodes
        {
            get
            {
                return nodes;
            }
            set
            {
                nodes = value;

                OnPropertyChanged("Nodes");
            }
        }
    }
}
