using UnityEngine;

namespace GOAP
{
    [CreateAssetMenu(fileName = "New Bool State", menuName = "GOAP/States/BoolState")]       // We've renamed the filename and chage the menuName to have something cleaner and tiny as a menu setup 
    public class G_BoolState : G_State      // Will inherit from G_State instead of Scriptable Object
    {
        bool value;

        #region Basic Controls

        public override void Construct(string name, object value)
        {
            this.name = name;
            SetValue(value);
        }

        public override object GetValue()   // overriding the function inherited from G_State
        {
            return value;
        }

        public override void SetValue(object value)
        {
            if (value is bool)      // just to make sure value is a bool type, otherwise we'll not be able to perform the operation: this.value = value
            {
                this.value = (bool)value;   // value is an "object" type which can basically be of any type. So we "force" its type to be bool by using (bool)
            }

        }

        public override G_State Clone()
        {
            G_BoolState clone = CreateInstance<G_BoolState>();
            clone.Construct(this.name, this.value);
            return clone;
        }

        #endregion


        #region Testing Controls

        #endregion

    }
}
