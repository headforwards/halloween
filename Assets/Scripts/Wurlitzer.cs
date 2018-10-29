using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wurlitzer : MonoBehaviour {

	AudioClip[] clips = null;

	AudioSource audio = null;

	public int MinPlayTime = 60;
	public int MaxPlayTime = 120;

	float fadeRate = 0.5f;

	// Use this for initialization
	void Start () {

		var cliplist = new List<string>()
		{
			"audio/dark ambient loop",
			"audio/Halloween-JT",
			"audio/01. Curious",
			"audio/02. Dark House",
			"audio/03. Woods",
			"audio/04. Rain Forest"
		};

		clips = cliplist.Select(a=>Resources.Load<AudioClip>(a)).ToArray();

		audio = gameObject.GetComponent<AudioSource>();

		// pick a track to start
		int idx = (int) Mathf.Floor(Random.Range(0, clips.Length));
		audio.clip = clips[idx];
		audio.volume = 0;
		audio.Play();
		StartCoroutine(fadeIn());
	}

	IEnumerator fadeIn() 
	{
		while (audio.volume  < 0.5f) {
			audio.volume = Mathf.Lerp( audio.volume, 1.0f, fadeRate * Time.deltaTime);
			yield return new WaitForSeconds(0);
		}

		StartCoroutine(playForAwhile());
	 }

	 IEnumerator playForAwhile()
	 {
		int playTime = (int) Mathf.Floor(Random.Range(MinPlayTime, MaxPlayTime));

		yield return new WaitForSeconds(playTime);

		StartCoroutine(fadeOut());
	 }

	IEnumerator fadeOut() {

		while(audio.volume > 0.1){
			audio.volume = Mathf.Lerp( audio.volume, 0.0f, fadeRate * Time.deltaTime );
			yield return new WaitForSeconds(0);
		}

		audio.volume = 0.0f;

		yield return null;

		StartCoroutine(WaitToSwitchTracks());
	 }

	IEnumerator WaitToSwitchTracks()
	{

		yield return new WaitForSeconds(0);

		int idx = (int) Mathf.Floor(Random.Range(0, clips.Length));
		audio.clip = clips[idx];
		audio.volume = 0;
		audio.Play();

		StartCoroutine(fadeIn());

	}
}
