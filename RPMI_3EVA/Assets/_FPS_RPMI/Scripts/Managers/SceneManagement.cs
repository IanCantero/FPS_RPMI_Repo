using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void LoadScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
        //AudioManager.Instance.PlaySFX(0);
    }

    public void ExitGame()
    {
        Debug.Log("Has cerrado el juego.");
        //AudioManager.Instance.PlaySFX(0);
        Application.Quit();
    }
}