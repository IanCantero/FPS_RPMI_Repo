using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneJumperCoroutine : MonoBehaviour
{
    [SerializeField] int sceneToLoad;
    [SerializeField] float delay;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}
