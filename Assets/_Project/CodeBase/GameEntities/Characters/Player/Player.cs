using BarrelHide.GameEntities.Cameras;
using BarrelHide.Input; 
using System.Collections; 

namespace BarrelHide.GameEntities.Characters
{
    internal class Player : Character
    {
        private InputService inputService;
        private CameraService cameraService;

        protected override IEnumerator Start()
        {
            yield return base.Start();

            cameraService = core.GetService<CameraService>();
            cameraService.CamFolower.TransformFolow = this.transform;

            inputService = core.GetService<InputService>();
            inputService.OnJoystickInputEvent += Move;
            inputService.OnAttackBtnPressed += Attack;
            inputService.OnActionBtnPressed += Action;
        }
    }
}
