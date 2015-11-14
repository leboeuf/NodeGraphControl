using System;
using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using NodeGraphControl.Controls;
using NodeGraphControl.Utils;

namespace NodeGraphControl
{
    public class NodeGraph : Control
    {
        private static readonly DependencyPropertyKey NodesPropertyKey =
            DependencyProperty.RegisterReadOnly("Nodes", typeof(ImpObservableCollection<object>), typeof(NodeGraph),
                new FrameworkPropertyMetadata());
        public static readonly DependencyProperty NodesProperty = NodesPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey ConnectionsPropertyKey =
            DependencyProperty.RegisterReadOnly("Connections", typeof(ImpObservableCollection<object>), typeof(NodeGraph),
                new FrameworkPropertyMetadata());
        public static readonly DependencyProperty ConnectionsProperty = ConnectionsPropertyKey.DependencyProperty;

        public static readonly DependencyProperty NodesSourceProperty =
            DependencyProperty.Register("NodesSource", typeof(IEnumerable), typeof(NodeGraph),
                new FrameworkPropertyMetadata(NodesSource_PropertyChanged));

        public static readonly DependencyProperty ConnectionsSourceProperty =
            DependencyProperty.Register("ConnectionsSource", typeof(IEnumerable), typeof(NodeGraph),
                new FrameworkPropertyMetadata(ConnectionsSource_PropertyChanged));

        public static readonly DependencyProperty NodeTemplateProperty =
            DependencyProperty.Register("NodeTemplate", typeof(DataTemplate), typeof(NodeGraph));

        public static readonly DependencyProperty NodeTemplateSelectorProperty =
            DependencyProperty.Register("NodeTemplateSelector", typeof(DataTemplateSelector), typeof(NodeGraph));

        public static readonly DependencyProperty NodeContainerStyleProperty =
            DependencyProperty.Register("NodeContainerStyle", typeof(Style), typeof(NodeGraph));


        static NodeGraph()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodeGraph), new FrameworkPropertyMetadata(typeof(NodeGraph)));
        }

        public NodeGraph()
        {
            Nodes = new ImpObservableCollection<object>();
            Connections = new ImpObservableCollection<object>();
        }

        /// <summary>
        /// Collection of nodes in the graph.
        /// </summary>
        public ImpObservableCollection<object> Nodes
        {
            get
            {
                return (ImpObservableCollection<object>)GetValue(NodesProperty);
            }
            private set
            {
                SetValue(NodesPropertyKey, value);
            }
        }

        /// <summary>
        /// Collection of connections in the graph.
        /// </summary>
        public ImpObservableCollection<object> Connections
        {
            get
            {
                return (ImpObservableCollection<object>)GetValue(ConnectionsProperty);
            }
            private set
            {
                SetValue(ConnectionsPropertyKey, value);
            }
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
        /// Gets or sets the DataTemplate used to display each node item.
        /// This is the equivalent to 'ItemTemplate' for ItemsControl.
        /// </summary>
        public DataTemplate NodeTemplate
        {
            get
            {
                return (DataTemplate)GetValue(NodeTemplateProperty);
            }
            set
            {
                SetValue(NodeTemplateProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets custom style-selection logic for a style that can be applied to each generated container element. 
        /// This is the equivalent to 'ItemTemplateSelector' for ItemsControl.
        /// </summary>
        public DataTemplateSelector NodeTemplateSelector
        {
            get
            {
                return (DataTemplateSelector)GetValue(NodeTemplateSelectorProperty);
            }
            set
            {
                SetValue(NodeTemplateSelectorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the Style that is applied to the item container for each node item.
        /// This is the equivalent to 'ItemContainerStyle' for ItemsControl.
        /// </summary>
        public Style NodeContainerStyle
        {
            get
            {
                return (Style)GetValue(NodeContainerStyleProperty);
            }
            set
            {
                SetValue(NodeContainerStyleProperty, value);
            }
        }

        /// <summary>
        /// Event raised when a new collection has been assigned to the 'NodesSource' property.
        /// </summary>
        private static void NodesSource_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var nodeGraph = (NodeGraph) d;
            nodeGraph.Nodes.Clear();

            if (e.OldValue != null)
            {
                var notifyCollectionChanged = e.OldValue as INotifyCollectionChanged;
                if (notifyCollectionChanged != null)
                {
                    //
                    // Unhook events from previous collection.
                    //
                    notifyCollectionChanged.CollectionChanged -= nodeGraph.NodesSource_CollectionChanged;
                }
            }

            if (e.NewValue != null)
            {
                var enumerable = e.NewValue as IEnumerable;
                if (enumerable != null)
                {
                    //
                    // Populate 'Nodes' from 'NodesSource'.
                    //
                    foreach (object obj in enumerable)
                    {
                        nodeGraph.Nodes.Add(obj);
                    }
                }

                var notifyCollectionChanged = e.NewValue as INotifyCollectionChanged;
                if (notifyCollectionChanged != null)
                {
                    //
                    // Hook events in new collection.
                    //
                    notifyCollectionChanged.CollectionChanged += nodeGraph.NodesSource_CollectionChanged;
                }
            }
        }

        /// <summary>
        /// Event raised when a node has been added to or removed from the collection assigned to 'NodesSource'.
        /// </summary>
        private void NodesSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                //
                // 'NodesSource' has been cleared, also clear 'Nodes'.
                //
                Nodes.Clear();
            }
            else
            {
                if (e.OldItems != null)
                {
                    //
                    // For each item that has been removed from 'NodesSource' also remove it from 'Nodes'.
                    //
                    foreach (object obj in e.OldItems)
                    {
                        Nodes.Remove(obj);
                    }
                }

                if (e.NewItems != null)
                {
                    //
                    // For each item that has been added to 'NodesSource' also add it to 'Nodes'.
                    //
                    foreach (object obj in e.NewItems)
                    {
                        Nodes.Add(obj);
                    }
                }
            }
        }

        /// <summary>
        /// Cached reference to the NodeControl in the visual-tree.
        /// </summary>
        private NodeControl nodeControl = null;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            nodeControl = (NodeControl)this.Template.FindName("PART_NodeControl", this);
            if (nodeControl == null)
            {
                throw new ApplicationException("Failed to find 'PART_NodeControl' in the visual tree for 'NodeGraph'");
            }
        }

        private static void ConnectionsSource_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }
    }
}
