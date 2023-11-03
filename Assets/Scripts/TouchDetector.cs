using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class TouchDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player 2"))
                return;

            // TODO: do stuff before loading the next level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
