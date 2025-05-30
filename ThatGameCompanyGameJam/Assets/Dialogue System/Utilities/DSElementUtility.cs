using DS.Elements;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;


namespace DS.Utilities
{
    using Elements;
    public static class DSElementUtility
    {
        public static Button CreateButton(string text, Action onClick = null)
        {
            Button button = new Button(onClick)
            {
                text = text
            };

            return button;
        }

        public static Foldout CreateFoldout(string title, bool collapsed = false)
        {
            Foldout foldout = new Foldout()
            {
                text = title,
                value = !collapsed
            };

            return foldout;
        }

        public static Port CreatePort(this DSNode node, string portName = "", Orientation orientation = Orientation.Horizontal, Direction direction = Direction.Output, Port.Capacity capacity = Port.Capacity.Single)
        {
            Port port = node.InstantiatePort(orientation, direction, capacity, typeof(bool));
            port.portName = portName;

            return port;
        }

        public static TextField CreateTextField(string value = null, EventCallback<ChangeEvent<string>> OnValueChanged = null)
        {
            TextField NodeTextField = new TextField()
            {
                value = value
            };

            if (OnValueChanged != null)
            {
                NodeTextField.RegisterValueChangedCallback(OnValueChanged);
            }

            return NodeTextField;
        }

        public static TextField CreateTextArea(string value = null, EventCallback<ChangeEvent<string>> OnValueChanged = null)
        {
            TextField NodeTextArea = CreateTextField(value, OnValueChanged);
            NodeTextArea.multiline = true;

            return NodeTextArea;
        }
    }
}
