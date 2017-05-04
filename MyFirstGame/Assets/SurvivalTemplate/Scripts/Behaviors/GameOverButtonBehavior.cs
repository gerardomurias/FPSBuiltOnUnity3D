using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtonBehavior : MonoBehaviour
{
    public void Click()
    {
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
    }
}