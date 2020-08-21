using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLooper : MonoBehaviour
{
	public AudioSource intro;
	public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (!intro.isPlaying && !music.isPlaying)
		{
			music.Play();
		}
		if (music.time > music.clip.length * 0.99)
		{
			music.Play();
		}
    }
}
