using Assets.PixelCrew.Gameplay.Coins;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PixelCrew.Core.Components
{
    public class ReloadEventComponent : MonoBehaviour
    {
        public void Reload()
        {
            TotalScore.Reset();

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}