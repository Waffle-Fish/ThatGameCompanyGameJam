using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Elements
{
    using Enumerations;
    using Utilities;
    using Elements;
    using DS.Windows;

    public class DSNode : Node
    {
        public string DialogueName { get; set; }
        public List<string> Choices { get; set; }
        public string Text { get; set; }
        public DSDialogueType DialogueType { get; set; }
        private DSGraphView graphView;
        public DSGroup group { get; set; }

        private Color defaultBackgroundColor;

        public virtual void Initialize(DSGraphView dsGraphView, Vector2 position)
        {
            DialogueName = "DialogueName";
            Choices = new List<string>();
            Text = "Dialogue text.";
            graphView = dsGraphView;
            defaultBackgroundColor = new Color(29f / 255f, 29f / 255f, 30f / 255f);

            SetPosition(new Rect(position, Vector2.zero));

            mainContainer.AddToClassList("ds-node__main-container");
            extensionContainer.AddToClassList("ds-node__extension-container");
        }

        #region Overrides
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Disconnect Input Ports", actionEvent => DisconnectInputPorts());
            evt.menu.AppendAction("Disconnect Output Ports", actionEvent => DisconnectOutputPorts());
            base.BuildContextualMenu(evt);
        }
        #endregion

        public virtual void Draw()
        {
            // Title Container
            TextField DialogueNameTextField = DSElementUtility.CreateTextField(DialogueName, null, callback =>
            {
                if (group ==null)
                {
                    graphView.RemoveUngroupedNode(this);

                    DialogueName = callback.newValue;

                    graphView.AddUngroupedNode(this);
                    
                    return;
                }

                DSGroup currentGroup = group;

                graphView.RemoveGroupedNode(this, group);
                DialogueName = callback.newValue;

                graphView.AddGroupedNode(this, currentGroup);
            });

            DialogueNameTextField.AddClasses(
                "ds-node__textfield", 
                "ds-node__filename-textfield", 
                "ds-node__textfield__hidden");

            titleContainer.Insert(0, DialogueNameTextField);

            // Input Container
            Port InputPort = this.CreatePort("Dialogue Connection", Orientation.Horizontal, Direction.Input, Port.Capacity.Multi);

            inputContainer.Add(InputPort);

            // Extensions Container

            VisualElement CustomDataContainer = new VisualElement();

            CustomDataContainer.AddToClassList("DS-node__custom-data-container");

            Foldout TextFoldout = DSElementUtility.CreateFoldout("Dialogue Text");

            TextField MainTextField = DSElementUtility.CreateTextArea(Text, null);

            MainTextField.AddClasses(
               "ds-node__textfield",
               "ds-node__quote-textfield"
                );

            TextFoldout.Add(MainTextField);
            CustomDataContainer.Add(TextFoldout);
            extensionContainer.Add(CustomDataContainer);
        }

        #region Utility Methods
        public void DisconnectAllPorts()
        {
            DisconnectInputPorts();
            DisconnectOutputPorts();
        }

        private void DisconnectPorts(VisualElement container)
        {
            foreach (Port port in container.Children())
            {
                if (!port.connected)
                {
                    continue;
                }

                graphView.DeleteElements(port.connections);
            }
        }

        private void DisconnectInputPorts()
        {
            DisconnectPorts(inputContainer);
        }

        private void DisconnectOutputPorts()
        {
            DisconnectPorts(outputContainer);
        }

        public void SetErrorStyle(Color color)
        {
            mainContainer.style.backgroundColor = color;
        }

        public void ResetStyle()
        {
            mainContainer.style.backgroundColor = defaultBackgroundColor;
        }
        #endregion
    }
}

