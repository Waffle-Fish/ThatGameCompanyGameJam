using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace DS.Elements
{
    using Enumerations;


    public class DSMultipleChoiceNode : DSNode
    {
        public override void Initialize(Vector2 position)
        {
            base.Initialize(position);

            DialogueType = DSDialogueType.MultipleChoice;

            Choices.Add("New Choice");
        }

        public override void Draw()
        {
            base.Draw();

            // Main Container
            Button AddChoiceButton = new Button()
            {
                text = "Add Choice"
            };

            mainContainer.Insert(1, AddChoiceButton);

            // Output Container
            foreach (string Choice in Choices)
            {
                Port ChoicePort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

                ChoicePort.portName = "";

                Button ChoicePortButton = new Button()
                {
                    text = "X"
                };

                TextField ChoiceTextField = new TextField()
                {
                    value = Choice
                };

                ChoicePort.Add(ChoiceTextField);
            }

            RefreshExpandedState();
        }
    }
}

