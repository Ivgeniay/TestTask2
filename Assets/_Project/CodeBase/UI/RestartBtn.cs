using UnityEngine;
using UnityEngine.SceneManagement;

namespace BarrelHide.UI
{
    internal class RestartBtn : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
