using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace DS.Elements
{
    using Utilities;
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
            Button AddChoiceButton = DSElementUtility.CreateButton("Add Choice", () =>
            {
                Port ChoicePort = CreateChoicePort("New Choice");

                Choices.Add("New Choice");

                outputContainer.Add(ChoicePort);
            });

            AddChoiceButton.AddToClassList("ds-node__button");

            mainContainer.Insert(1, AddChoiceButton);

            // Output Container
            foreach (string Choice in Choices)
            {
                Port ChoicePort = CreateChoicePort(Choice);

                outputContainer.Add(ChoicePort);
            }

            RefreshExpandedState();
        }

        #region Element Creation
        private Port CreateChoicePort(string choice)
        {
            Port ChoicePort = this.CreatePort();

            Button DeleteChoiceButton = DSElementUtility.CreateButton("X");

            DeleteChoiceButton.AddToClassList("ds-node__button");

            TextField ChoiceTextField = DSElementUtility.CreateTextField(choice);

            ChoiceTextField.AddClasses(
                "ds-node__textfield",
                "ds-node__choice-textfield",
                "ds-node__textfield__hidden"
                );

            ChoicePort.Add(ChoiceTextField);
            return ChoicePort;
        }
        #endregion
    }
}

