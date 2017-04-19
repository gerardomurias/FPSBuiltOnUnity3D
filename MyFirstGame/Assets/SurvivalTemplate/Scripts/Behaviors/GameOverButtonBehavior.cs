using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtonBehavior : MonoBehaviour
{
    public void Click()
    {
        SceneManager.LoadSceneAsync("SecondScene", LoadSceneMode.Single);
    }
}