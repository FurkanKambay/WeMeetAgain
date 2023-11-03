using UnityEngine;

namespace Game
{
    public class UIManager : MonoBehaviour
    {
        public void StartGame() => UnityEngine.SceneManagement.SceneManager.LoadScene(1);

        public void Quit()
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
