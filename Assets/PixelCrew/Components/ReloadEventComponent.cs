using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew
{
    public class ReloadEventComponent : MonoBehaviour
    {
        public void Reload()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
