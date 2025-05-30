using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DS.Windows
{
    using Elements;
    using Enumerations;

    public class DSSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private DSGraphView View;
        private Texture2D IndentationIcon;

        public void Initialize(DSGraphView dsGraphView)
        {
            View = dsGraphView;
            IndentationIcon = new Texture2D(1, 1);
            IndentationIcon.SetPixel(0, 0, Color.clear);
            IndentationIcon.Apply();
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> SearchTreeEntries = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Create Element", IndentationIcon)),
                new SearchTreeGroupEntry(new GUIContent("New Dialogue Node", IndentationIcon), 1),
                new SearchTreeEntry(new GUIContent("Single Choice", IndentationIcon))
                {
                    level = 2,
                    userData = DSDialogueType.SingleChoice
                },
                new SearchTreeEntry(new GUIContent("Multiple Choice", IndentationIcon))
                {
                    level = 2,
                    userData = DSDialogueType.MultipleChoice
                },
                new SearchTreeGroupEntry(new GUIContent("Group Dialogue"), 1),
                new SearchTreeEntry(new GUIContent("Single Group", IndentationIcon))
                {
                    level = 2,
                    userData = new DSGroup("New Group", context.screenMousePosition)
                }
            };

            return SearchTreeEntries;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            Vector2 LocalMousePosition = View.GetLocalMousePosition(context.screenMousePosition, this);

            switch (SearchTreeEntry.userData)
            {
                case DSDialogueType.SingleChoice:
                    {
                        DSSingleChoiceNode SingleChoiceNode = (DSSingleChoiceNode)View.CreateNode(DSDialogueType.SingleChoice, LocalMousePosition);
                        View.AddElement(SingleChoiceNode);

                        return true;
                    }
                case DSDialogueType.MultipleChoice:
                    {
                        DSMultipleChoiceNode MultipleChoiceNode = (DSMultipleChoiceNode)View.CreateNode(DSDialogueType.MultipleChoice, LocalMousePosition);
                        View.AddElement(MultipleChoiceNode);

                        return true;
                    }
                case Group _:
                    {
                        View.CreateGroup("DialogueGroup", LocalMousePosition);

                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }
    }
}