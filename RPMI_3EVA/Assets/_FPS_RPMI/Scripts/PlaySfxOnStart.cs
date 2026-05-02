using UnityEngine;

public class PlaySfxOnStart : MonoBehaviour
{
    public int indiceSfx = 5; // El número que ocupe el sonido del ascensor en tu lista

    void Start()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(indiceSfx);
        }
    }
}
