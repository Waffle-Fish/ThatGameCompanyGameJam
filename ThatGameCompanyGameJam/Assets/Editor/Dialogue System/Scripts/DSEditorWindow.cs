using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace DS.Windows
{
    using Utilities;
    public class DSEditorWindow : EditorWindow
    {
        private readonly string defaultFileName = "DialogueFile";
        [MenuItem("Window/DS/Dialogue Graph")]
        public static void ShowExample()
        {
            GetWindow<DSEditorWindow>("Dialogue Graph");
        }

        private void OnEnable()
        {
            AddGraphView();
            AddToolbar();

            AddStyles();
        }

        #region Element Visuals
        private void AddGraphView()
        {
            DSGraphView graphView = new DSGraphView(this);

            graphView.StretchToParentSize();

            rootVisualElement.Add(graphView);
        }

        private void AddToolbar()
        {
            Toolbar toolbar = new Toolbar();

            TextField fileNameTextfield = DSElementUtility.CreateTextField(defaultFileName, "File Name");

            Button saveButton = DSElementUtility.CreateButton("Save");

            toolbar.Add(fileNameTextfield);
            toolbar.Add(saveButton);

            rootVisualElement.Add(toolbar);
        }

        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets("DSVariables.uss");
        }
        #endregion
    }
}


