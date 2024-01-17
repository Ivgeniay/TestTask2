using BarrelHide.GameEntities.Cameras;
using BarrelHide.GameEntities.PatrolSystem;
using BarrelHide.Input;
using BarrelHide.Services;
using MoreMountains.Tools;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace BarrelHide.Core
{
    public class Bootstraper : MonoBehaviour
    {
        [SerializeField] private MMTouchJoystick joystick;
        [SerializeField] private Button actionBtn;
        [SerializeField] private Button attackBtn;
        [SerializeField] private CameraFollower cameraFollower;
        [SerializeField] private PatrolService patrolService;

        public static Bootstraper instance = null;
        public static Bootstraper Instance { get
            {
                if (instance == null)
                {
                    instance = FindAnyObjectByType<Bootstraper>();
                    if (instance == null)
                        throw new NullReferenceException("You shoud to put and setup bootstraper on the scene");
                }
                return instance;
            }
        }

        private ServiceProvider serviceProvider;
        [HideInInspector] public bool IsLoaded = false;

        private void Awake()
        {
            instance = this;
            serviceProvider = new();
            serviceProvider
                .RegisterService(new InputService(joystick, actionBtn, attackBtn))
                .RegisterService(new GameModeService())
                .RegisterService(new CameraService(cameraFollower))
                .RegisterService(patrolService);

            serviceProvider.Initialize();
            IsLoaded = true;
        }

        public T GetService<T>() =>
            serviceProvider.GetService<T>();

    }
}
