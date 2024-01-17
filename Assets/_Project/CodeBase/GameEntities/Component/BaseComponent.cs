using BarrelHide.Core;
using BarrelHide.GameEntities.Characters;
using UnityEngine;

namespace BarrelHide.GameEntities.Component
{
    [RequireComponent(typeof(Character))]
    public class BaseComponent : CoreMonoBehaviour
    {
        internal Character Character { get; private set; }

        protected virtual void Awake()
        {
            Character = GetComponentInChildren<Character>();
        }
    }
}
