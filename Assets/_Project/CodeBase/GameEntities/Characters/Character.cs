using System.Collections; 
using BarrelHide.Core;
using UnityEngine;

namespace BarrelHide.GameEntities.Characters
{
    internal abstract class Character : CoreMonoBehaviour
    {
        protected IController controller;

        protected virtual void Awake() =>
            controller = GetComponentInChildren<IController>();
        

        public virtual void Move(Vector2 direction) => controller?.Move(direction);
        public virtual void Attack() => controller?.Attack();
        public virtual void Action() => controller?.Action();
    }
}
