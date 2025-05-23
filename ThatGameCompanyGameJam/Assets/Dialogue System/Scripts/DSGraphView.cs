using DS.Elements;
using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Windows
{
    using Elements;
    public class DSGraphView : GraphView
    {
        public DSGraphView()
        {
            AddManipulators();

            AddGridBackground();

            AddStyles();
        }

        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(CreateNodeContextualMenu());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();

            gridBackground.StretchToParentSize();

            Insert(0, gridBackground);
        }

        private DSNode CreateNode(Vector2 position)
        {
            DSNode Node = new DSNode();
            Node.Initialize(position);
            Node.Draw();

            return Node;
        }

        private void AddStyles()
        {
            StyleSheet styleSheet = (StyleSheet)EditorGUIUtility.Load("DSGraphViewStyles.uss");
            styleSheets.Add(styleSheet);
        }

        private IManipulator CreateNodeContextualMenu()
        {
            ContextualMenuManipulator ContextMenu = new ContextualMenuManipulator(
                MenuEvent => MenuEvent.menu.AppendAction("Add Node", ActionEvent => AddElement(CreateNode(ActionEvent.eventInfo.localMousePosition))));

            return ContextMenu;
        }
    }
}