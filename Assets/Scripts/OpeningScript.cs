using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class OpeningScript : MonoBehaviour
{
    public PlayableDirector timeline;
	public AudioSource music;
    

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0)){
            timeline.Play();

			StartCoroutine(FadeAudioSource.StartFade(music, 1.5f, 0));
		}
    }
}
