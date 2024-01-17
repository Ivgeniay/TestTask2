using BarrelHide.Core;
using BarrelHide.GameEntities.Component;
using BarrelHide.Utils.AnimBehaviour;
using System.Collections;
using UnityEngine;

namespace BarrelHide.GameEntities
{
    internal abstract class BaseAnimatorController : BaseComponent, IGameModeDependence
    {
        [SerializeField] protected Animator animator;
        protected SMCallbacksBehaviour stateBehaviour;

        private float animatorSpeedBuffer = 0f;

        public void OnGameStateChange(GameMode currentGMode)
        {
            if (currentGMode != GameMode.GamePlay) animator.speed = 0;
            else animator.speed = animatorSpeedBuffer;
        }

        protected override void Awake()
        {
            base.Awake();
            animatorSpeedBuffer = animator.speed;
            stateBehaviour = animator.GetBehaviour<SMCallbacksBehaviour>();
            if (stateBehaviour != null) stateBehaviour.OnSMEvent += Callbacks_OnSMEvent;
        }

        protected override IEnumerator Start()
        {
            yield return base.Start();
            core.GetService<GameModeService>().Register(this);
        }

        protected abstract void Callbacks_OnSMEvent(SMBehaviour type, Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
    }

}
