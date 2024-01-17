using BarrelHide.GameEntities.Characters;
using BarrelHide.GameEntities.PatrolSystem;
using System.Collections;

namespace BarrelHide.GameEntities.Enemies
{
    internal class Enemy : Character
    {
        private PatrolComponent patrolComponent;

        protected override void Awake()
        {
            base.Awake();
            patrolComponent = GetComponent<PatrolComponent>();
            patrolComponent.FollowPoint.OnChangeEvent += OnNextPositionChangeHandler;
        }

        private void OnNextPositionChangeHandler(UnityEngine.Transform obj) =>
            Move(new UnityEngine.Vector2(obj.position.x, obj.position.z));
        

    }
}
