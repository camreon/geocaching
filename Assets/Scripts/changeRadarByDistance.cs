
using UnityEngine;
using System.Collections;

public class changeRadarByDistance : MonoBehaviour
{
	public AudioClip radar;
	float blipPause = 3.0f;
	float distance = 80.0f;

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

			yield return new WaitForSeconds(blipPause);
		}
	}

	void Update()
	{
		// get distance between cache and player
		distance = Vector3.Distance(this.transform.position, targetCache.transform.position);
		
		// blip frequency changes based on distance 
		blipPause = 5.0f * Mathf.InverseLerp(10, 150, distance);

		// change target cache
		if (Input.GetKeyDown (KeyCode.A)) {
			cacheI = (cacheI + 1) % NUM_CACHES;
			targetCache = caches[cacheI];
		}
	}
	
}