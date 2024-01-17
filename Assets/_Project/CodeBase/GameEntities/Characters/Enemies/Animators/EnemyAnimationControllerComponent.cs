using BarrelHide.GameEntities.Characters;
using BarrelHide.Utils;
using BarrelHide.Utils.AnimBehaviour;
using UnityEngine;

namespace BarrelHide.GameEntities.Enemies.Animators
{
    internal class EnemyAnimationControllerComponent : CharacterAnimationController
    {
        private static readonly int Aim = Animator.StringToHash("Aim");
        private static readonly int Shoot = Animator.StringToHash("Shoot");
        private static readonly int Cnockout = Animator.StringToHash("Cnockout");

        internal ObservVariable<AnimState> StateObserver = new();

        protected override void Callbacks_OnSMEvent(SMBehaviour type, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            switch (type)
            {
                case SMBehaviour.Enter:

                    if (stateInfo.shortNameHash == Aim) StateObserver.Value = AnimState.Aim;
                    else if (stateInfo.shortNameHash == Shoot) StateObserver.Value = AnimState.Shoot;
                    else if (stateInfo.shortNameHash == Cnockout) StateObserver.Value = AnimState.Cnockout; 
                    else StateObserver.Value = AnimState.Other;

                    break;
            }
        }

        internal enum AnimState { Other, Aim, Shoot, Cnockout }
    }
}
