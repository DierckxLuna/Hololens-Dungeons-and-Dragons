using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.DataManagement
{
    public class SharedObject<T> : ScriptableObject
    {
        public UnityAction<T> OnChange;

        private T _object;

        public T Value
        {
            get { return _object; }
            set { RaiseEvent(value); }
        }

        private void RaiseEvent(T value)
        {
            Debug.Log($"Raising events");
            _object = value;

            if (OnChange != null)
            {
                OnChange.Invoke(value);
            }
        }
    }
}