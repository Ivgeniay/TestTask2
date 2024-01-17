using System.Collections.Generic; 
using System.Collections;
using BarrelHide.Core; 
using UnityEngine;
using TMPro;

namespace BarrelHide.CodeBase.UI
{
    internal class ResultGame : CoreMonoBehaviour, IGameModeDependence
    {
        [SerializeField] private TextMeshProUGUI ResultText;
        [SerializeField] private List<GameObject> appearObject;

        private GameModeService gameModeService;

        protected override IEnumerator Start()
        {
            yield return base.Start();
            gameModeService = core.GetService<GameModeService>();
            gameModeService.Register(this);
        }

        public void OnGameStateChange(GameMode currentGMode)
        {
            if (currentGMode == GameMode.Win)
            {
                Appear();
                ResultText.text = "Congratulations!"; 
            }
            else if (currentGMode == GameMode.Lose)
            {
                Appear();
                ResultText.text = "Try again!";
            }
        }

        private void Appear() =>
            appearObject.ForEach(e => e.SetActive(true));
    }
}
