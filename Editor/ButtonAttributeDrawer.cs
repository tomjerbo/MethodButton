using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Jerbo.Inspector
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class ButtonAttributeDrawer : UnityEditor.Editor
    {
        BindingFlags BINDING_FLAGS = BindingFlags.Default | 
                                     BindingFlags.Instance | 
                                     BindingFlags.Public |
                                     BindingFlags.NonPublic | 
                                     BindingFlags.Static;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            MonoBehaviour mono = (MonoBehaviour)target;
            MethodInfo[] methods = mono.GetType().GetMethods(BINDING_FLAGS);
            
            foreach (MethodInfo method in methods)
            {
                ButtonAttribute buttonAttribute = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));
                if (buttonAttribute == null) continue;
                
                string displayText = buttonAttribute.displayText;
                if (string.IsNullOrEmpty(displayText)) displayText = method.Name;
                
                if (GUILayout.Button(displayText, GUILayout.Height((float)buttonAttribute.buttonSize)))
                {
                    method.Invoke(mono, null);
                }
            }
        }
    }
}