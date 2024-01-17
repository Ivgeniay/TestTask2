using BarrelHide.Core;
using BarrelHide.GameEntities.Component;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BarrelHide.GameEntities.Characters
{
    public sealed class DetectedComponent : BaseComponent, IGameModeDependence
    {
        public event Action<object> OnDetectedEvent;
        public event Action<object> OnLastedEvent;
        public event Action<object> OnDetectingStayEvent;

        [SerializeField] public bool IsEnable = true; 
        [SerializeField] private float detectionRadius = 3f;
        [SerializeField] private LayerMask detectionLayer;
        [SerializeField][HideInInspector] private string detectedComponent;

        private Type detectingType;
        private List<object> detectedObjects = new(5);
        private List<object> buffer = new(5);
        List<object> except = new(5);

        protected override void Awake()
        {
            base.Awake();
            detectingType = Type.GetType(detectedComponent);
        }

        protected override IEnumerator Start()
        {
            yield return base.Start();
            core.GetService<GameModeService>().Register(this);
        }

        private void Update() => Detecting();

        private void Detecting()
        {
            if (!IsEnable) return;

            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer); 
            DetectAction(colliders);
        }

        private void DetectAction(Collider[] colliders)
        {
            buffer.Clear();
            foreach (Collider collider in colliders)
            {
                if (collider == null || collider.gameObject == null) continue;

                var detected = collider.gameObject.GetComponent(detectingType);
                if (detected)
                {
                    buffer.Add(detected);
                    if (!detectedObjects.Contains(detected))
                    {
                        detectedObjects.Add(detected);
                        OnDetectedEvent?.Invoke(detected);
                    }
                }
            }
            CheckingBuffer(buffer);
            if (buffer.Count > 0)
            {
                foreach (var detectedObject in detectedObjects)
                    OnDetectingStayEvent?.Invoke(detectedObject);
            }
        } 

        private void CheckingBuffer(List<object> buffer)
        {
            if (buffer.Count > 0)
            {
                except = detectedObjects.Except(buffer).ToList();
                if (except.Count > 0)
                {
                    foreach (object obj in except)
                        OnLastedEvent?.Invoke(obj);
                
                    detectedObjects = detectedObjects.Except(except).ToList();
                }
                except.Clear();
            }
            else
            {
                foreach (object obj in detectedObjects)
                    OnLastedEvent?.Invoke(obj);
                
                detectedObjects.Clear();
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!IsEnable) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }

        void IGameModeDependence.OnGameStateChange(GameMode currentGMode) => 
            IsEnable = currentGMode == GameMode.GamePlay;
    }
}
