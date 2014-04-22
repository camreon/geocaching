
using UnityEngine;
using System.Collections;

public class changeRadarByDistance : MonoBehaviour
{
	public AudioClip radar;
	float blipPause = 3.0f;
	float distance = 80.0f;
	float newVolume = .5f;

	public GameObject cacheA;
	public GameObject cacheB;
	GameObject[] caches;
	GameObject targetCache;
	int cacheI = 0;
	int NUM_CACHES = 2;
	
	void Start() 
	{
		StartCoroutine(SoundOut());
		
		caches = new GameObject[NUM_CACHES];
		caches[0] = cacheA;
		caches[1] = cacheB;
		targetCache = caches[0];
	}
	
	// radar blips every n seconds	
	IEnumerator SoundOut() {
		while (true) {
			// start playing at a start time
			audio.PlayOneShot(radar);
			audio.volume = newVolume;

			yield return new WaitForSeconds(blipPause);
		}
	}

	void Update()
	{
		// blip frequency changes based on distance 
		distance = Vector3.Distance(this.transform.position, targetCache.transform.position);
		blipPause = 4.0f * Mathf.InverseLerp(0, 150, distance);
		// TODO set minimum pause OR notify & disable when close enough

		// blip volume increases if player is looking in the direction the cache
		newVolume = Vector3.Angle(this.transform.forward, targetCache.transform.position - this.transform.position);
		newVolume = 1.0f - Mathf.InverseLerp(0, 160, newVolume);
	
		// change target cache
		if (Input.GetKeyDown (KeyCode.A)) {
			cacheI = (cacheI + 1) % NUM_CACHES;
			targetCache = caches[cacheI];
		}
	}
	
}