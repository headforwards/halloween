using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wurlitzer : MonoBehaviour
{

    Dictionary<string, float> cliplist = new Dictionary<string, float>();

    AudioSource audio = null;

    public int MinPlayTime = 60;
    public int MaxPlayTime = 120;

    float fadeRate = 0.5f;
    float maxVolume = 0.5f;

    // Use this for initialization
    void Start()
    {
        cliplist.Add("audio/dark ambient loop", 0.5f);
        cliplist.Add("audio/Halloween-JT", 0.2f);
        cliplist.Add("audio/01. Curious", 0.5f);
        cliplist.Add("audio/02. Dark House", 0.99f);
        cliplist.Add("audio/dead so dead", 0.2f);

        audio = gameObject.GetComponent<AudioSource>();

        StartRandomTrack();

        StartCoroutine(FadeIn());
    }

    void StartRandomTrack()
    {
        int idx = (int)Mathf.Floor(Random.Range(0, cliplist.Count()));
        var key = cliplist.Keys.ToArray()[idx];
        audio.clip = Resources.Load<AudioClip>(key);
        maxVolume = cliplist[key];
        audio.volume = 0;
        audio.loop = true;
        audio.Play();

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        while (audio.volume < maxVolume)
        {
            audio.volume = Mathf.Lerp(audio.volume, 1.0f, fadeRate * Time.deltaTime);
            yield return new WaitForSeconds(0);
        }

        StartCoroutine(PlayForAwhile());
    }

    IEnumerator PlayForAwhile()
    {
        int playTime = (int)Mathf.Floor(Random.Range(MinPlayTime, MaxPlayTime));

        yield return new WaitForSeconds(playTime);

       StartCoroutine( FadeOut());
    }

    IEnumerator FadeOut()
    {
        while (audio.volume > 0.01)
        {
            audio.volume = Mathf.Lerp(audio.volume, 0.0f, fadeRate * Time.deltaTime);
            yield return new WaitForSeconds(0);
        }

        StartRandomTrack();
    }

}
