using IceBlink.GameJamToolkit.DamageSystem.Defenses;
using UnityEditor;
using UnityEngine;

namespace IceBlink.GameJamToolkit.DamageSystem.Editor
{
    [CustomPropertyDrawer(typeof(Resistance))]
    public class ResistancesCustomDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Indent the content within the drawer
            position = EditorGUI.IndentedRect(position);

            // Calculate the height of each property
            var height = position.height / 2f;
            var propertyRect = new Rect(position.x, position.y, position.width, height);

            // Get serialized properties for the two properties of the custom class
            var property1 = property.FindPropertyRelative("damageType");
            var property2 = property.FindPropertyRelative("amount");

            // Draw the first property
            EditorGUI.PropertyField(propertyRect, property1);

            // Adjust the position for the second property
            propertyRect.y += height;

            // Draw the second property
            EditorGUI.PropertyField(propertyRect, property2);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("damageType")) +
                   EditorGUI.GetPropertyHeight(property.FindPropertyRelative("amount"));
        }
    }
}