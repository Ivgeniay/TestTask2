using System;
using System.Collections.Generic;

namespace BarrelHide.Utils
{
    [Serializable]
    internal class ObservVariable<T>
    {
        public event Action<T> OnChangeEvent;
        public event Action<T, T> OnChange2DEvent;

        private T value;
        public T Value
        {
            get => value;
            set
            {
                if (!value.Equals(this.value))
                {
                    T prevValue = this.value;
                    this.value = value;
                    OnChangeEvent?.Invoke(this.value);
                    OnChange2DEvent?.Invoke(this.value, prevValue);
                }
            }
        }

        public static bool operator ==(ObservVariable<T> a, T b) =>
            a?.Value.Equals(b) ?? false;

        public static bool operator !=(ObservVariable<T> a, T b) =>
            !(a == b);

        public static ObservVariable<T> operator +(ObservVariable<T> a, Action<T> handler)
        {
            a.OnChangeEvent += handler;
            return a;
        }
        public static ObservVariable<T> operator -(ObservVariable<T> a, Action<T> handler)
        {
            a.OnChangeEvent -= handler;
            return a;
        }

        public override bool Equals(object obj)
        {
            if (obj is ObservVariable<T> other)
                return EqualityComparer<T>.Default.Equals(this.Value, other.Value);
            return false;
        }
        public override int GetHashCode() =>
            EqualityComparer<T>.Default.GetHashCode(this.Value);

    }
}