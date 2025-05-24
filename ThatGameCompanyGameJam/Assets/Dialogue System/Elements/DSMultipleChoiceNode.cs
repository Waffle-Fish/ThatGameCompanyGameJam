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

            AddChoiceButton.AddToClassList("ds-node__button");

            mainContainer.Insert(1, AddChoiceButton);

            // Output Container
            foreach (string Choice in Choices)
            {
                Port ChoicePort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));

                ChoicePort.portName = "";

                Button DeleteChoiceButton = new Button()
                {
                    text = "X"
                };

                DeleteChoiceButton.AddToClassList("ds-node__button");

                TextField ChoiceTextField = new TextField()
                {
                    value = Choice
                };

                ChoiceTextField.AddToClassList("ds-node__textfield");
                ChoiceTextField.AddToClassList("ds-node__choice-textfield");
                ChoiceTextField.AddToClassList("ds-node__textfield__hidden");

                ChoicePort.Add(ChoiceTextField);
            }

            RefreshExpandedState();
        }
    }
}

