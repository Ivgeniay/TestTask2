using BarrelHide.Services;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BarrelHide.GameEntities.PatrolSystem
{
    internal class PatrolService : MonoBehaviour, IService
    {
        [SerializeField] private List<PatrolModel> patrols;

        public void Initialize()
        {
            bool isPatrolInvalidData = patrols.Any(e => e.patrolPoints == null || e.patrolPoints.Count == 0);
            if (isPatrolInvalidData) throw new System.Exception("There is invalid data in the model");
        }

        public Transform GetNextPoint(PatrolComponent patrolComponent)
        {
            PatrolModel model = patrols
                .Where(e => e.character == patrolComponent.Character)
                .FirstOrDefault();

            if (model is null)
                throw new System.Exception($"You should set up patrol behaviour for this body: {patrolComponent.Character}");
            
            if (patrolComponent.FollowPoint.Value is null)
                return model.patrolPoints[0];

            int currePointIndex = model.patrolPoints.IndexOf(patrolComponent.FollowPoint.Value);
            int nextPointIndex =
                currePointIndex < model.patrolPoints.Count - 1 ? 
                currePointIndex + 1 : 
                0;

            return model.patrolPoints[nextPointIndex];
        }
    }
}
