using UnityEngine;
using UnityEditor;
using GOAP;

[CustomPropertyDrawer(typeof(G_Condition))]     // tells the Editor what specific class this drawer is for
public class G_ConditionEditor : PropertyDrawer     // Property Drawer: to be able to customised elements in the Unity inspector
                                                    // There is only once instance of the custom property drawer running
{
    bool active = false;

    // GetPropertyHeight (default function from Property Drawer) for setting the height of the Custom Drawer, to place it in Editor interface
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label);     // this is the basic UI object in Unity
    }

    // OnGUI (default function from Property Drawer) for writing the contents of the Custom Drawer
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // base.OnGUI(position, property, label);       // default
        SerializedProperty active = property.FindPropertyRelative("editorActive");      // This is the way to find the property editorActive in the G_Condition when using Custom Drawer

        // EditorGui is a class that contains all the normal GUI functions  /  .Foldout: a label with a foldout to the left of it
        active.boolValue = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, GetPropertyHeight(property, label)),  // we use here the default rectangle position
                                             active.boolValue,      // it's just toggle between true / and false when we click on the label (activate/ non activate), so basically opens up / down the foldout
                                             "Condition");     // Name display in the Editor
    }
}
