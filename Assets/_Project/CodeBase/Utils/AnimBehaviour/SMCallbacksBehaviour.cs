using UnityEditor;
using UnityEngine;

namespace BarrelHide.Utils.AnimBehaviour
{
    internal delegate void OnSMEvent(SMBehaviour type, Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
    internal class SMCallbacksBehaviour : StateMachineBehaviour
    {
        public event OnSMEvent OnSMEvent;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            OnSMEvent?.Invoke(SMBehaviour.Enter, animator, stateInfo, layerIndex);
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            OnSMEvent?.Invoke(SMBehaviour.Exit, animator, stateInfo, layerIndex);
        public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            OnSMEvent?.Invoke(SMBehaviour.Move, animator, stateInfo, layerIndex);
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            OnSMEvent?.Invoke(SMBehaviour.Update, animator, stateInfo, layerIndex);
        public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            OnSMEvent?.Invoke(SMBehaviour.IK, animator, stateInfo, layerIndex);

    }
}
