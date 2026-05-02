using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    //Declaración del Singleton
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null) Debug.Log("No hay AudioManager");
            return instance;
        }

    }
    //Fin del Singleton

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip[] musicLibrary;
    public AudioClip[] sfxLibrary;

    private void Awake()
    {

        if (instance == null)
        {
            //Si no hay GameManager lo referenciamos y hacemos que perdure entre escenas
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Si ya hay GameManager el duplicado se destruye
            Destroy(gameObject);
        }
    }

    public void PlayMusic(int musicToPlay)
    {
        musicSource.clip = musicLibrary[musicToPlay];
        musicSource.Play();  //Reproducir la musica
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfxSource.PlayOneShot(sfxLibrary[sfxToPlay]);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void UnPauseMusic()
    {
        musicSource.UnPause();
    }

    public void FadeOutPasos(float tiempo)
    {
        StopAllCoroutines();
        StartCoroutine(DoFadeOut(tiempo));
    }

    private IEnumerator DoFadeOut(float duration)
    {
        float startVolume = sfxSource.volume;
        while (sfxSource.volume > 0)
        {
            sfxSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }
        sfxSource.Stop();
        sfxSource.volume = startVolume;
    }

}
