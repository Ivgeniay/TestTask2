using System.Collections; 
using BarrelHide.Core;
using BarrelHide.GameEntities.Characters;

namespace BarrelHide.GameEntities
{
    internal class Finish : CoreMonoBehaviour
    {
        private GameModeService gameModeService;
        protected override IEnumerator Start()
        {
            yield return base.Start();
            gameModeService = core.GetService<GameModeService>(); 
        }

        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player)
            {
                gameModeService.ChangeState(GameMode.Win);
            }
        }
    }
}
