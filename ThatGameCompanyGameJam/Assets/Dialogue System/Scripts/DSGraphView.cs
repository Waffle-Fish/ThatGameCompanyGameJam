using DS.Elements;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Windows
{
    using Elements;
    using Enumerations;
    using Utilities;

    public class DSGraphView : GraphView
    {
        private DSEditorWindow EditorWindow;
        private DSSearchWindow searchWindow;

        public DSGraphView(DSEditorWindow dsEditorWindow)
        {
            EditorWindow = dsEditorWindow;
            AddManipulators();
            AddGridBackground();
            AddSearchWindow();
            AddStyles();
        }

        #region Overridden Methods
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> CompatiblePorts = new List<Port>();

            ports.ForEach(port =>
            {
                if (startPort == port)
                {
                    return;
                }

                if (startPort.node == port.node)
                {
                    return;
                }

                if (startPort.direction == port.direction)
                {
                    return;
                }

                CompatiblePorts.Add(port);
            });

            return CompatiblePorts;
        }
        #endregion

        #region Manipulators
        private void AddManipulators()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(CreateNodeContextualMenu("Add Node (Single Choice)", DSDialogueType.SingleChoice));
            this.AddManipulator(CreateNodeContextualMenu("Add Node (Multiple Choice)",DSDialogueType.MultipleChoice));
            this.AddManipulator(CreateGroupContextualMenu());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }

        private IManipulator CreateNodeContextualMenu(string ActionTitle, DSDialogueType DialogueType)
        {
            ContextualMenuManipulator ContextMenu = new ContextualMenuManipulator(
                MenuEvent => MenuEvent.menu.AppendAction(ActionTitle, ActionEvent => AddElement(CreateNode(DialogueType, GetLocalMousePosition(ActionEvent.eventInfo.localMousePosition)))));

            return ContextMenu;
        }

        private IManipulator CreateGroupContextualMenu()
        {
            ContextualMenuManipulator ContextMenu = new ContextualMenuManipulator(
               MenuEvent => MenuEvent.menu.AppendAction("Add Group", ActionEvent => AddElement(CreateGroup("Dialogue Group", GetLocalMousePosition(ActionEvent.eventInfo.localMousePosition)))));

            return ContextMenu;
        }
        #endregion

        #region Element Creation
        public DSNode CreateNode(DSDialogueType DialogueType, Vector2 position)
        {
            Type NodeType = Type.GetType($"DS.Elements.DS{DialogueType}Node");
            DSNode Node = (DSNode) Activator.CreateInstance(NodeType);
            Node.Initialize(position);
            Node.Draw();

            return Node;
        }

        public Group CreateGroup(string title, Vector2 localMousePosition)
        {
            Group NodeGroup = new Group()
            {
                title = title
            };

            NodeGroup.SetPosition(new Rect(localMousePosition, Vector2.zero));

            return NodeGroup;
        }
        #endregion

        #region Element Visuals
        private void AddStyles()
        {
            this.AddStyleSheets("DSGraphViewStyles.uss", "DSNodeStyles.uss");
        }

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();

            gridBackground.StretchToParentSize();

            Insert(0, gridBackground);
        }

        private void AddSearchWindow()
        {
            if (searchWindow == null)
            {
                searchWindow = ScriptableObject.CreateInstance<DSSearchWindow>();
                searchWindow.Initialize(this);

                nodeCreationRequest = context => SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
            }
        }
        #endregion

        #region Utilities
        public Vector2 GetLocalMousePosition(Vector2 mousePosition, bool isSearchWindow = false)
        {
            Vector2 WorldMousePosition = mousePosition;

            if (isSearchWindow)
            {
                WorldMousePosition -= EditorWindow.position.position;
            }

            Vector2 LocalMousePosition = contentViewContainer.WorldToLocal(WorldMousePosition);

            return LocalMousePosition;
        }
        #endregion
    }
}