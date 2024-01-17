using BarrelHide.GameEntities.Characters;
using BarrelHide.Utils;
using BarrelHide.Utils.AnimBehaviour;
using UnityEngine; 

namespace BarrelHide.GameEntities.InteractiveObjects.Animators
{
    internal class BarrelAnimationController : BaseAnimatorController
    {
        private static readonly int P_TAKEBARREL = Animator.StringToHash("TakeBarrel");
        private static readonly int P_TAKEOFFBARREL = Animator.StringToHash("TakeOffBarrel");
        private static readonly int P_USED = Animator.StringToHash("Used");
        private static readonly int P_RUN = Animator.StringToHash("Run");

        private static readonly int TakeBarrel = Animator.StringToHash("TakeBarrelState");
        private static readonly int TakeOffBarrel = Animator.StringToHash("TakeOffBarrelState");
        private static readonly int Idle = Animator.StringToHash("IdleState");
        private static readonly int Run = Animator.StringToHash("RunState");

        internal ObservVariable<AnimState> StateObserver = new();

        public bool IsUsed() => animator.GetBool(P_USED);
        public void ToTakeBarrel()
        {
            if (!IsUsed())
            {
                animator.SetTrigger(P_TAKEBARREL);
                animator.SetBool(P_USED, true); 
            }
        }

        public void ToTakeOffBarrel()
        {
            if (IsUsed())
                animator.SetTrigger(P_TAKEOFFBARREL); 
        }

        public void SetRun(bool value) => animator.SetBool(P_RUN, value); 
        public void DisableUse() => animator.speed = 0;
        

        protected override void Callbacks_OnSMEvent(SMBehaviour type, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            switch (type)
            {
                case SMBehaviour.Enter:

                    if (stateInfo.shortNameHash == TakeBarrel) StateObserver.Value = AnimState.TakeBarrel;
                    else if (stateInfo.shortNameHash == TakeOffBarrel) StateObserver.Value = AnimState.TakeOffBarrel;
                    else if (stateInfo.shortNameHash == Idle) StateObserver.Value = AnimState.Idle;
                    else if (stateInfo.shortNameHash == Run) StateObserver.Value = AnimState.Run;

                    break;
            }
        }

        internal enum AnimState { TakeBarrel, TakeOffBarrel, Idle, Run } 
    }
}
