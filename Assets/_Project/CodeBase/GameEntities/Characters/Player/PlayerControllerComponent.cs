using BarrelHide.Core;
using BarrelHide.GameEntities.Cameras;
using BarrelHide.GameEntities.Component;
using BarrelHide.GameEntities.InteractiveObjects.Barrel;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace BarrelHide.GameEntities.Characters
{
    [RequireComponent(typeof(CharacterController))]
    internal sealed class PlayerControllerComponent : BaseComponent, IController, IGameModeDependence
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private PlayerAnimationControllerComponent animController;
        [SerializeField] private DetectedComponent detectedComponent;
        [field: SerializeField] public float MaxSpeed { get; private set; } 
        private Barrel targetBarrel;

        public bool IsEnable { get; set; } = true;
        public bool IsBarraled { get => animController.IsBarreled(); }
        private CameraService cameraService { get; set; }
         
        protected override void Awake()
        {
            base.Awake();
            animController.Initalize(this);
            detectedComponent.OnDetectedEvent += OnDetectedBarrelHandler;
            detectedComponent.OnLastedEvent += OnLastBarrelHandler; 
        }

        protected override IEnumerator Start()
        {
            yield return base.Start();
            cameraService = core.GetService<CameraService>();
            animController.StateObserver.OnChange2DEvent += OnAnimatorStateChange;
            core.GetService<GameModeService>().Register(this);
        }


        public void Action()
        {
            if (animController.CurrentWalkingMixerState != PlayerAnimationControllerComponent.AnimMixerState.Idle) return;
            if (animController.CurrentAnimationState == PlayerAnimationControllerComponent.AnimState.TakeBarrel ||
                animController.CurrentAnimationState == PlayerAnimationControllerComponent.AnimState.TakeOffBarrel) return;

            if (animController.CurrentAnimationState == PlayerAnimationControllerComponent.AnimState.NoBarrel)
            {
                if (targetBarrel && targetBarrel.IsAvailable)
                {
                    transform.LookAt(targetBarrel.transform);
                    targetBarrel.Take(this);
                    animController.ToBarrelOn();
                    detectedComponent.IsEnable = false;
                }
            } 
            else if (animController.CurrentAnimationState == PlayerAnimationControllerComponent.AnimState.IsBarrel)
            {
                if (targetBarrel && targetBarrel.IsAvailable)
                {
                    targetBarrel.Throw();
                    animController.ToBarrelOff();
                    detectedComponent.IsEnable = true;
                }
            } 
        }
        public float GetSpeed() => controller.velocity.magnitude;
        public void Attack()
        {
            if (animController.CurrentWalkingMixerState == PlayerAnimationControllerComponent.AnimMixerState.Idle &&
                animController.CurrentAnimationState == PlayerAnimationControllerComponent.AnimState.NoBarrel)
                animController.ToAttack();
        }

        public void Move(Vector2 direction)
        {
            if (!IsEnable) return;

            Vector3 movementVector = Vector3.zero;
            if (direction != Vector2.zero)
            {
                movementVector = cameraService.CurrentCamera.transform.TransformDirection(direction);
                movementVector.y = 0;
                movementVector.Normalize();
                transform.forward = movementVector;
            }
            var speed = IsBarraled ? MaxSpeed/2 : MaxSpeed;
            movementVector += Physics.gravity;
            Vector3 resultMoveVector = movementVector * speed * Time.deltaTime;
            controller.Move(resultMoveVector);
        }

        private void OnAnimatorStateChange(PlayerAnimationControllerComponent.AnimState current, PlayerAnimationControllerComponent.AnimState prev)
        {
            IsEnable =
                (current == PlayerAnimationControllerComponent.AnimState.NoBarrel || current == PlayerAnimationControllerComponent.AnimState.IsBarrel) ?
                true : false;
        }

        private void OnLastBarrelHandler(object obj)
        {
            if (obj is Barrel barrel)
            {
                if (targetBarrel != null && targetBarrel == barrel)
                    targetBarrel = null;
            }
        }

        private void OnDetectedBarrelHandler(object obj)
        {
            if (obj is Barrel barrel)
            {
                if (barrel.IsAvailable)
                    targetBarrel = barrel;
            }
        }

        public void OnGameStateChange(GameMode currentGMode) =>
            IsEnable = currentGMode == GameMode.GamePlay;
        
    }
}
