using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DS.Utilities
{
    public static class DSStyleUtility
    {
        public static VisualElement AddStyleSheets(this VisualElement element, params string[] StyleSheetNames)
        {
            foreach (string StyleSheetName in StyleSheetNames)
            {
                StyleSheet NodeStyleSheet = (StyleSheet)EditorGUIUtility.Load(StyleSheetName);
                element.styleSheets.Add(NodeStyleSheet);
            }

            return element;
        }

        public static VisualElement AddClasses(this VisualElement element, params string[] ClassNames)
        {
            foreach (string ClassName in ClassNames)
            {
                element.AddToClassList(ClassName);
            }

            return element;
        }
    }
}