
using UnityEngine;
using System.Collections;

public class changeRadarByDistance : MonoBehaviour
{
	public AudioClip radar;
	public GameObject cache;
	float blipPause = 3.0f;
	float distance = 80.0f;
	
	void Start() 
	{
		StartCoroutine(SoundOut());
	}
	
	// radar blips every n seconds	
	IEnumerator SoundOut() {
		while (true) {
			// start playing at a start time
			audio.PlayOneShot(radar);

			yield return new WaitForSeconds(blipPause);
		}
	}

	void Update()
	{
		// get distance between cache and player
		distance = Vector3.Distance(this.transform.position, cache.transform.position);
		
		// blip frequency changes based on distance 
		blipPause = 5.0f * Mathf.InverseLerp(10, 150, distance);
	}
	
}