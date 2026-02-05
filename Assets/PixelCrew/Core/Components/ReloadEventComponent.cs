using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Core.Components
{
    public class ReloadEventComponent : MonoBehaviour
    {
        public void Reload()
        {
            EnterTriggerComponent.Reset();

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}