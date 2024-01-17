using BarrelHide.Core;
using BarrelHide.GameEntities.Cameras;
using BarrelHide.GameEntities.Enemies.Controllers;
using System.Collections;
using UnityEngine;

namespace BarrelHide.GameEntities.Enemies.Animators
{
    internal class ShootAnimationProxy : CoreMonoBehaviour
    {
        private GameModeService gameModeService;
        private CameraService cameraService;

        protected override IEnumerator Start()
        {
            yield return base.Start();
            gameModeService = core.GetService<GameModeService>();
            cameraService = core.GetService<CameraService>(); 
        }

        public void GameOver()
        {
            gameModeService.ChangeState(GameMode.Lose);
            cameraService.ShowMe(this.transform);
        }
    }
}
