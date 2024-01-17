using BarrelHide.GameEntities.Component;
using BarrelHide.Utils;
using System.Collections;
using UnityEngine;

namespace BarrelHide.GameEntities.PatrolSystem
{
    internal class PatrolComponent : BaseComponent
    {
        [SerializeField] private float minDistance = .5f;

        public ObservVariable<Transform> FollowPoint = new();

        private PatrolService patrolService;
        private bool isInitialized = false;

        protected override IEnumerator Start()
        {
            yield return base.Start();
            patrolService = core.GetService<PatrolService>();

            FollowPoint.Value = patrolService.GetNextPoint(this); 
            isInitialized = FollowPoint.Value != null;
        }


        private void Update()
        {
            if (!isInitialized) return;

            var distance = Vector3.Distance(FollowPoint.Value.position, transform.position);
            if (distance <= minDistance)
            {
                FollowPoint.Value = patrolService.GetNextPoint(this);
            }
        }

    }
}
