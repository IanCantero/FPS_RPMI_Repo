using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SecuenciaAscensor : MonoBehaviour
{
    [Header("Referencias UI")]
    public Image imagenNegra;
    public GameObject cajaDeDialogos;

    [Header("Ajustes de Tiempo")]
    public float tiempoTotal = 6.0f; // AQUÍ PONES LOS 6 SEGUNDOS
    public float tiempoEnNegroPuro = 3.0f; // Cuánto tiempo está 100% oscuro

    [Header("Configuración Audio")]
    public int indiceAscensor = 5;
    public int indiceFinal = 6;

    void Start()
    {
        if (imagenNegra != null) StartCoroutine(RutinaAscensor());
    }

    IEnumerator RutinaAscensor()
    {
        // 1. Inicio
        if (cajaDeDialogos != null) cajaDeDialogos.SetActive(false);
        imagenNegra.gameObject.SetActive(true);
        imagenNegra.color = Color.black;

        if (AudioManager.Instance != null) AudioManager.Instance.PlaySFX(indiceAscensor);

        // 2. Espera en negro puro
        yield return new WaitForSeconds(tiempoEnNegroPuro);

        // 3. Fundido de IMAGEN y AUDIO a la vez
        float tiempoRestante = tiempoTotal - tiempoEnNegroPuro;
        float timer = 0;
        float volumenInicial = AudioManager.Instance.sfxSource.volume;

        while (timer < tiempoRestante)
        {
            timer += Time.deltaTime;
            float progreso = timer / tiempoRestante;

            // Aclara la imagen
            imagenNegra.color = new Color(0, 0, 0, 1 - progreso);

            // Baja el volumen suavemente (Fade out del audio)
            AudioManager.Instance.sfxSource.volume = Mathf.Lerp(volumenInicial, 0, progreso);

            yield return null;
        }

        // 4. Final de secuencia
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.sfxSource.Stop();
            AudioManager.Instance.sfxSource.volume = volumenInicial; // Restaurar volumen
            AudioManager.Instance.PlaySFX(indiceFinal); // El sonido "Ding" o puerta
        }

        imagenNegra.gameObject.SetActive(false);
        if (cajaDeDialogos != null) cajaDeDialogos.SetActive(true);
    }
}

