using BarrelHide.Utils;
using BarrelHide.Utils.AnimBehaviour;
using UnityEngine;

namespace BarrelHide.GameEntities.Characters
{
    [RequireComponent(typeof(Player))]
    internal class PlayerAnimationControllerComponent : CharacterAnimationController
    { 
        public static readonly int P_IsBarrel = Animator.StringToHash("IsBarrel");

        private static readonly int TakeBarrel = Animator.StringToHash("TakeBarrel");
        private static readonly int TakeOffBarrel = Animator.StringToHash("TakeOffBarrel");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int WithBarrelTree = Animator.StringToHash("WithBarrelTree");
        private static readonly int NoBarrelTree = Animator.StringToHash("NoBarrelTree");
        private static readonly int HandsUp = Animator.StringToHash("HandsUp");

        internal ObservVariable<AnimState> StateObserver = new();
        internal virtual AnimState CurrentAnimationState { get => StateObserver == null ? AnimState.NoBarrel : StateObserver.Value; }
        internal AnimMixerState CurrentWalkingMixerState { get => animator.GetFloat(P_SPEED) > Constants.MIN_STEP_WALK ? AnimMixerState.Walking : AnimMixerState.Idle; }

        public void ToBarrelOff() => animator.SetTrigger(TakeOffBarrel);
        public void ToBarrelOn() => animator.SetTrigger(TakeBarrel);
        public void ToIsBarrelTrue() => animator.SetBool(P_IsBarrel, true);
        public void ToIsBarrelFalse() => animator.SetBool(P_IsBarrel, false);
        public bool IsBarreled() => animator.GetBool(P_IsBarrel);

        protected override void Callbacks_OnSMEvent(SMBehaviour type, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            switch (type)
            {
                case SMBehaviour.Enter:

                    if (stateInfo.shortNameHash == Attack) StateObserver.Value = AnimState.Attack;
                    else if (stateInfo.shortNameHash == TakeBarrel) StateObserver.Value = AnimState.TakeBarrel;
                    else if (stateInfo.shortNameHash == TakeOffBarrel) StateObserver.Value = AnimState.TakeOffBarrel;
                    else if (stateInfo.shortNameHash == NoBarrelTree) StateObserver.Value = AnimState.NoBarrel;
                    else if (stateInfo.shortNameHash == WithBarrelTree) StateObserver.Value = AnimState.IsBarrel;
                    else if (stateInfo.shortNameHash == HandsUp) StateObserver.Value = AnimState.Dead;

                    break;
            }
        }


        internal enum AnimState { NoBarrel, IsBarrel, Attack, Dead, TakeBarrel, TakeOffBarrel }
        internal enum AnimMixerState { Idle, Walking }
    }
}
