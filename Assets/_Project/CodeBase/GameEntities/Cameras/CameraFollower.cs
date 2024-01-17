using System;
using UnityEngine;

namespace BarrelHide.GameEntities.Cameras
{
    internal class CameraFollower : MonoBehaviour
    {
        public Transform TransformFolow;
        public Camera FollowCamera;

        [Space(20)]
        [SerializeField] private Vector3 RotationAngleX;
        [SerializeField] private Vector3 OffsetY;
        [SerializeField] private int Distance;

        private void LateUpdate()
        {
            if (!TransformFolow || !FollowCamera) return;

            Quaternion rotation = Quaternion.Euler(RotationAngleX);

            Vector3 folowPos = TransformFolow.position;
            folowPos += OffsetY;
            Vector3 position = rotation * new Vector3(0, 0, -Distance) + folowPos;

            FollowCamera.transform.rotation = rotation;
            FollowCamera.transform.position = position;
        }

    }
}
