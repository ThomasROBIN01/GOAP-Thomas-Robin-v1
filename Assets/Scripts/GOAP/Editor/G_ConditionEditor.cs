using UnityEngine;
using UnityEditor;
using GOAP;

[CustomPropertyDrawer(typeof(G_Condition))]     // tells the Editor what specific class this drawer is for
public class G_ConditionEditor : PropertyDrawer     // Property Drawer: to be able to customised elements in the Unity inspector
                                                    // There is only once instance of the custom property drawer running
{
    float height = 0f;

    // GetPropertyHeight (default function from Property Drawer) for setting the height of the Custom Drawer, to place it in Editor interface
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int heightMulitplier = 1;

        SerializedProperty stateProperty = property.FindPropertyRelative("state");
        SerializedProperty active = property.FindPropertyRelative("editorActive");

        if (stateProperty.objectReferenceValue != null && active.boolValue)
        {
            heightMulitplier = (stateProperty.objectReferenceValue as G_State).GetEditorHeight();
        }
        else if (stateProperty.objectReferenceValue == null && active.boolValue)
        {
            heightMulitplier = 2;
        }

        return (base.GetPropertyHeight(property, label) + EditorGUIUtility.standardVerticalSpacing) * heightMulitplier;     // this is the basic UI object in Unity
                                                                                                                            // *heightMulitplier to get enough spacing
    }

    // OnGUI (default function from Property Drawer) for writing the contents of the Custom Drawer
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)        // SerializedProperty property actually represents a G_Condition
    {
        height = 0f;

        // base.OnGUI(position, property, label);       // default
        SerializedProperty active = property.FindPropertyRelative("editorActive");      // This is the way to find the property editorActive in the G_Condition when using Custom Drawer
                                                                                        // editorActive parameter in the G_Condition is actually a bool

        // EditorGui is a class that contains all the normal GUI functions  /  .Foldout: a label with a foldout to the left of it
        active.boolValue =  // being assigned based on return value of Foldout
            EditorGUI.Foldout(GetFormattedRect(position, property, label),  // we use here the default rectangle position
                                             active.boolValue,  // value that is drawn. it's just toggle between true / and false when we click on the label (activate/ non activate), so basically opens up / down the foldout
                                             "Condition");     // Name display in the Editor

        IncrementHeight(out height, property, label);

        int originalIndent = EditorGUI.indentLevel;
        EditorGUI.indentLevel += 1;

        if(active.boolValue)
        {
            BuildEditor(position, property, label);
        }

        EditorGUI.indentLevel = originalIndent;
    }

    void BuildEditor(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty stateProperty = property.FindPropertyRelative("state");
        // Object preStateObject = stateProperty.objectReferenceValue;     // this is just to get what is currently stored in stateProperty

        EditorGUI.ObjectField(GetFormattedRect(position, property, label), stateProperty);

        IncrementHeight(out height, property, label);

        if (stateProperty.objectReferenceValue != null)
        {
            // build custom Editor
            ((G_State)stateProperty.objectReferenceValue).Editor(this, ref height, position, property, label);
        }
    }

    public Rect GetFormattedRect (Rect position, SerializedProperty property, GUIContent label)
    {
        return new Rect(position.x, position.y + height, position.width, base.GetPropertyHeight(property, label));  
        // This is basically to keep track of the last item we place in the editor and be able to build the rectangle right after the previous one, thanks to the height parameter
    }

    /// <summary>
    /// Increases local height variable by one standard property height + one standard vertical spacing gap everytime it is called 
    /// so that we can increment the height variable to continue drwing elements without them being drawn on top of each other
    /// </summary>
    /// <param name="progressiveHeight"></param>
    /// <param name="property"></param>
    /// <param name="label"></param>
    public void IncrementHeight (out float progressiveHeight, SerializedProperty property, GUIContent label)
    {
        progressiveHeight = height;

        progressiveHeight += base.GetPropertyHeight(property, label);

        progressiveHeight += EditorGUIUtility.standardVerticalSpacing;
    }
}
