using BarrelHide.GameEntities.InteractiveObjects.Animators;
using BarrelHide.GameEntities.Characters;
using BarrelHide.Utils; 
using UnityEngine;
using BarrelHide.GameEntities.Component;
using UnityEngine.Events;

namespace BarrelHide.GameEntities.InteractiveObjects.Barrel
{
    internal class Barrel : MonoBehaviour
    {
        public UnityEvent OnTakeEvent;

        [SerializeField] private Transform barrelRoot; 

        private IController ownerController;
        private Transform ownerTransform;
        private Vector3 offset;

        [SerializeField] private BarrelAnimationController animationController;
        public bool IsAvailable = true;

        private void Awake()
        {
            animationController.StateObserver.OnChangeEvent += OnAnimationStateChangeHandler;
        }

        private void Update()
        {
            if (!ownerTransform) return;

            //barrelRoot.position = ownerTransform.position + offset;
            animationController.SetRun(Mathf.InverseLerp(0, ownerController.GetSpeed(), ownerController.MaxSpeed) > Constants.MIN_STEP_WALK);
        }

        private void OnAnimationStateChangeHandler(BarrelAnimationController.AnimState state)
        {
            if (state == BarrelAnimationController.AnimState.TakeOffBarrel)
                IsAvailable = false;
        }

        public void RotateBarrel(Transform transform)
        {
            barrelRoot.LookAt(transform, Vector3.up);
            barrelRoot.Rotate(0f, 180f, 0f);
        }

        public void Take(IController owner)
        {
            OnTakeEvent?.Invoke();
            this.ownerController = owner;
            if (owner is BaseComponent com)
            {
                this.ownerTransform = com.Character.transform;
                this.barrelRoot.SetParent(ownerTransform);
            }
            RotateBarrel(this.ownerTransform);
            animationController.ToTakeBarrel();
            offset = barrelRoot.position - this.ownerTransform.position;
        }

        public void Throw()
        {
            if (ownerTransform != null)
            {
                animationController.ToTakeOffBarrel();
                ownerTransform = null;
                ownerController = null;
                this.barrelRoot.SetParent(null);
            }
        } 
    }
}
