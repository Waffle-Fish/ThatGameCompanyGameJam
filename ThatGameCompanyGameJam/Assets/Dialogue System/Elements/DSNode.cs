using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace DS.Elements
{
    using Enumerations;


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
        }

        public virtual void Draw()
        {
            // Title Container
            TextField DialogueNameTextField = new TextField()
            {
                value = DialogueName
            };

            titleContainer.Insert(0, DialogueNameTextField);

            // Input Container
            Port InputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));

            InputPort.portName = "Dialogue Connection";

            inputContainer.Add(InputPort);

            // Extensions Container

            VisualElement CustomDataContainer = new VisualElement();

            Foldout TextFoldout = new Foldout()
            {
                text = "Dialogue Text"
            };

            TextField MainTextField = new TextField()
            {
                value = Text
            };

            TextFoldout.Add(MainTextField);
            CustomDataContainer.Add(TextFoldout);
            extensionContainer.Add(CustomDataContainer);
        }
    }
}

