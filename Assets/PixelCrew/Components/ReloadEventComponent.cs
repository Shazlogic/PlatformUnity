using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Components
{
    public class ReloadEventComponent : MonoBehaviour
    {
        public void Reload()
        {
            EnterTriggerComponent.TotalScore = 0;

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
