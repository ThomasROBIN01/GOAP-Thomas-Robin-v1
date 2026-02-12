using UnityEngine;

// [CreateAssetMenu(fileName = "G_State", menuName = "Scriptable Objects/G_State")]     // Removed because we don't want to it instantiable in the Editor
public class G_State : ScriptableObject
{
    // the value we are storing
    object value;   // object type can contain any type of variable

    public virtual void Construct (string name, object value)       // virtual type to be overwritten by inheriting classes
    {
        this.name = name;   // we didn't declare this name variable at the beginning, because the object type contain by default a name, which we are referencing here.
        SetValue (value);
    }

    public virtual void SetValue (object value)     // function to set the value externally
    {
        this.value = value; 
    }

    public virtual object GetValue ()
    {
        return value;
    }
}
