using UnityEngine;

namespace BarrelHide.GameEntities.Characters
{
    internal abstract class CharacterAnimationController : BaseAnimatorController
    {
        public static readonly int P_SPEED = Animator.StringToHash("Speed");
        public static readonly int P_ATTACK = Animator.StringToHash("Attack");
        public static readonly int P_DEAD = Animator.StringToHash("Dead");

        protected IController controller;
        public void Initalize(IController controller) => this.controller = controller;

        protected virtual void Update() =>
                ToMove(
                    Mathf.InverseLerp(0, controller.MaxSpeed, controller == null ? 0 : controller.GetSpeed()),
                    0.1f,
                    Time.deltaTime
                    );

        public void ToMove(float value, float damp = 0.1f, float deltaTime = 0) =>
            animator.SetFloat(P_SPEED, value, damp, deltaTime == 0 ? Time.deltaTime : deltaTime);
        public void ToAttack() => animator.SetTrigger(P_ATTACK);
        public void ToDead() => animator.SetTrigger(P_DEAD);
    }
}
