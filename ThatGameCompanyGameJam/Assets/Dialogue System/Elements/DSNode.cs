using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Elements
{
    using Enumerations;
    using Utilities;
    using Elements;

    public class DSNode : Node
    {
        public string DialogueName { get; set; }
        public List<string> Choices { get; set; }
        public string Text { get; set; }
        public DSDialogueType DialogueType { get; set; }

        public virtual void Initialize(Vector2 position)
        {
            DialogueName = "DialogueName";
            Choices = new List<string>();
            Text = "Dialogue text.";

            SetPosition(new Rect(position, Vector2.zero));

            mainContainer.AddToClassList("ds-node__main-container");
            extensionContainer.AddToClassList("ds-node__extension-container");
        }

        public virtual void Draw()
        {
            // Title Container
            TextField DialogueNameTextField = DSElementUtility.CreateTextField(DialogueName);

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

            TextField MainTextField = DSElementUtility.CreateTextArea(Text);

            MainTextField.AddClasses(
               "ds-node__textfield",
               "ds-node__quote-textfield"
                );

            TextFoldout.Add(MainTextField);
            CustomDataContainer.Add(TextFoldout);
            extensionContainer.Add(CustomDataContainer);
        }
    }
}

