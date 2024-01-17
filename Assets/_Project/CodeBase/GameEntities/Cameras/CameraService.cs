using BarrelHide.Services;
using System;
using UnityEngine;

namespace BarrelHide.GameEntities.Cameras
{
    internal class CameraService : IService
    { 
        public Camera CurrentCamera { get; private set; }
        public CameraFollower CamFolower { get; private set; }

        public CameraService(CameraFollower camFolower)
        {
            this.CamFolower = camFolower;
        }

        public void Initialize()
        {
            CurrentCamera = Camera.main;
            if (CamFolower.FollowCamera == null) 
                CamFolower.FollowCamera = CurrentCamera;
        }

        internal void ShowMe(Transform transform)
        {
            
        }
    }
}
