using DS.Elements;
using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Windows
{
    using Elements;
    using Enumerations;
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

            this.AddManipulator(CreateNodeContextualMenu("Add Node (Single Choice)", DSDialogueType.SingleChoice));
            this.AddManipulator(CreateNodeContextualMenu("Add Node (Multiple Choice)",DSDialogueType.MultipleChoice));
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

        private DSNode CreateNode(DSDialogueType DialogueType, Vector2 position)
        {
            Type NodeType = Type.GetType($"DS.Elements.DS{DialogueType}Node");
            DSNode Node = (DSNode) Activator.CreateInstance(NodeType);
            Node.Initialize(position);
            Node.Draw();

            return Node;
        }

        private void AddStyles()
        {
            StyleSheet GraphViewStyleSheet = (StyleSheet)EditorGUIUtility.Load("DSGraphViewStyles.uss");
            StyleSheet NodeStyleSheet = (StyleSheet)EditorGUIUtility.Load("DSNodeStyles.uss");
            styleSheets.Add(GraphViewStyleSheet);
            styleSheets.Add(NodeStyleSheet);

        }

        private IManipulator CreateNodeContextualMenu(string ActionTitle, DSDialogueType DialogueType)
        {
            ContextualMenuManipulator ContextMenu = new ContextualMenuManipulator(
                MenuEvent => MenuEvent.menu.AppendAction(ActionTitle, ActionEvent => AddElement(CreateNode(DialogueType, ActionEvent.eventInfo.localMousePosition))));

            return ContextMenu;
        }
    }
}