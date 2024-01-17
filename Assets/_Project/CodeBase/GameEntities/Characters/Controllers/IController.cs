using UnityEngine;

namespace BarrelHide.GameEntities.Characters
{
    internal interface IController
    {
        public float MaxSpeed { get; }
        public float GetSpeed();
        public bool IsEnable { get; set; }
        public void Move(Vector2 direction);
        public void Attack();
        public void Action();
    }
}
