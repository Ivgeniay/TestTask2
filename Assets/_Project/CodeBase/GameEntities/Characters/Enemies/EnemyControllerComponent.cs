using BarrelHide.Core;
using BarrelHide.GameEntities.Characters;
using BarrelHide.GameEntities.Component;
using BarrelHide.GameEntities.Enemies.Animators;
using BarrelHide.Utils; 
using UnityEngine.AI;
using UnityEngine;
using System.Collections;

namespace BarrelHide.GameEntities.Enemies.Controllers
{
    [RequireComponent(typeof(Enemy))]
    [RequireComponent(typeof(NavMeshAgent))]
    internal class EnemyControllerComponent : BaseComponent, IController, IGameModeDependence
    {
        [SerializeField] private NavMeshAgent agent { get; set; }
        [SerializeField] private EnemyAnimationControllerComponent animController;
        [SerializeField] private DetectedComponent detectedComponent;

        [field: SerializeField] public float MaxSpeed { get; private set; } = 2;

        public bool IsEnable { get; set; } = true;
        private bool IsAiming { get => animController.StateObserver.Value == EnemyAnimationControllerComponent.AnimState.Aim; }

        protected override void Awake()
        {
            base.Awake();
            if (!agent) agent = GetComponent<NavMeshAgent>();
            agent.speed = MaxSpeed;
            animController.Initalize(this);
            detectedComponent.OnDetectingStayEvent += OnDetecingStaydHandler;
        }


        protected override IEnumerator Start()
        {
            yield return base.Start();
            core.GetService<GameModeService>().Register(this);
        }

        private void Update()
        {
            if (IsEnable && !IsAiming) agent.speed = MaxSpeed;
            else agent.speed = 0;
            //agent.speed = IsEnable ? MaxSpeed : 0; 
        }

        private void OnDetecingStaydHandler(object obj)
        {
            if (!IsEnable) return;
            if (IsAiming) return;

            if (obj is PlayerControllerComponent player)
            {
                if (player)
                {
                    if (player.IsBarraled && Mathf.InverseLerp(0, player.MaxSpeed, player.GetSpeed()) < Constants.MIN_STEP_WALK)
                        return;

                    agent.speed = 0;
                    transform.LookAt(player.transform);
                    Attack();
                }
            } 
        }

        public void Action() { }
        public void Attack() => animController.ToAttack();
        public float GetSpeed() => IsEnable == true ? agent.velocity.magnitude : 0;
        public void Move(Vector2 direction) =>
            agent.SetDestination(new Vector3(direction.x, 0, direction.y));

        public void OnGameStateChange(GameMode currentGMode) =>
            IsEnable = currentGMode == GameMode.GamePlay;
    }
}
