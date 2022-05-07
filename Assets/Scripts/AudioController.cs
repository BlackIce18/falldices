using System;
using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [HideInInspector] public static AudioController AudioControllerSingleton;

    [SerializeField] private AudioClip[] _audioList;


    private int _currentMusicNumber = -1;
    private Coroutine _coroutine;
    private float timeToMusicEnd;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;

    private void Start()
    {
        AudioControllerSingleton = this;
        _coroutine = StartCoroutine(StartAudio());
    }

    public IEnumerator StartAudio()
    {
        int randomMusic = UnityEngine.Random.Range(0, _audioList.Length);

        if(_audioList.Length > 1)
        {
            while (_currentMusicNumber == randomMusic)
            {
                randomMusic = UnityEngine.Random.Range(0, _audioList.Length);
            }
        }

        _currentMusicNumber = randomMusic;
        _musicSource.clip = _audioList[randomMusic];
        _musicSource.Play();
        yield return new WaitForSeconds(_musicSource.clip.length);
        _coroutine = StartCoroutine(StartAudio());
    }

    public IEnumerator WaitForMusicEnd(float time)
    {
        yield return new WaitForSeconds(time);
        _coroutine = StartCoroutine(StartAudio());
    }
    public static void StopMusic()
    {
        AudioControllerSingleton._musicSource.Stop();
        AudioControllerSingleton.StopAllCoroutines();
    }

    public static void PauseMusic()
    {
        AudioControllerSingleton._musicSource.Pause();
        AudioControllerSingleton.StopAllCoroutines();
        AudioControllerSingleton.timeToMusicEnd = AudioControllerSingleton._musicSource.clip.length - AudioControllerSingleton._musicSource.time;
    }

    public static void ResumeMusic()
    {
        AudioControllerSingleton._musicSource.Play();
        
        AudioControllerSingleton.StartCoroutine(AudioControllerSingleton.WaitForMusicEnd(AudioControllerSingleton.timeToMusicEnd));
    }

    public static void PlayMusic()
    {
        AudioControllerSingleton.StartCoroutine(AudioControllerSingleton.StartAudio());
    }

    public static void PlaySound(AudioClip audioClip)
    {
        AudioControllerSingleton._soundSource.PlayOneShot(audioClip);
    }
}
