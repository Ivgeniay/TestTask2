using BarrelHide.GameEntities.Characters;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BarrelHide.GameEntities.PatrolSystem
{
    [Serializable]
    internal class PatrolModel
    {
        [SerializeField] public Character character;
        [SerializeField] public List<Transform> patrolPoints;
    }
}
