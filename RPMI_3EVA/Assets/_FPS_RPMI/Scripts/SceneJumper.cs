using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJumper : MonoBehaviour
{
    [SerializeField] int sceneToLoad;

void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Cambia "NombreDeLaEscena" por el nombre de la escena a la que quieres saltar
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
