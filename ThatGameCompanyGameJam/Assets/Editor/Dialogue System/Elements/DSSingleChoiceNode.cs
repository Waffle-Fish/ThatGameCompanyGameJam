using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace DS.Elements
{
    using Enumerations;
    using Utilities;
    using Elements;
    using Windows;
    public class DSSingleChoiceNode : DSNode
    {
        public override void Initialize(DSGraphView dsGraphView, Vector2 position)
        {
            base.Initialize(dsGraphView, position);

            DialogueType = DSDialogueType.SingleChoice;

            Choices.Add("Next Dialogue");
        }

        public override void Draw()
        {
            base.Draw();

            // Output Container
            foreach (string Choice in Choices)
            {
                Port ChoicePort = this.CreatePort(Choice);
            }

            RefreshExpandedState();
        }
    }
}