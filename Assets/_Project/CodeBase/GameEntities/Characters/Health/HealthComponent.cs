using BarrelHide.GameEntities.Component;
using UnityEngine;

namespace BarrelHide.GameEntities.Characters.Health
{
    internal class HealthComponent : BaseComponent
    {
        [SerializeField] protected float maxHealth = 3;
        protected float current;

        protected override void Awake()
        {
            base.Awake();
            current = maxHealth;
        }


    }
}
