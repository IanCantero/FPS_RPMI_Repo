using UnityEngine;

public class MusicSceneStarter : MonoBehaviour
{
    public int indiceDeCancion; // El número de canción en tu lista del AudioManager

    void Start()
    {
        // Llama a la función de tu script original
        AudioManager.Instance.PlayMusic(indiceDeCancion);
    }
}
