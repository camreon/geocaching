
using UnityEngine;
using System.Collections;

public class changeRadarByDistance : MonoBehaviour
{
	public AudioClip radar, ding, easy_hint, difficult_hint;
	public float cacheProximity = 30.0f;
	float blipPause = 3.0f;
	float distance = 80.0f;
	float newVolume = .5f;

	public GameObject cacheA;
	public GameObject cacheB;
	GameObject[] caches;
	GameObject targetCache;
	int cacheI = 0;
	int NUM_CACHES = 2;
	bool isNear = false; 
	
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
			audio.volume = newVolume;
			if (!isNear)
				audio.PlayOneShot(radar);

			yield return new WaitForSeconds(blipPause);
		}
	}

	void Update()
	{
		// blip frequency changes based on distance 
		distance = Vector3.Distance(this.transform.position, targetCache.transform.position);

		// enter cache area
		if (distance < cacheProximity && !isNear) {
			audio.PlayOneShot(ding);
			isNear = true;
			new WaitForSeconds(8); // ???

			// read out hints
			if (targetCache == cacheA)
				audio.PlayOneShot(easy_hint);
			else
				audio.PlayOneShot(difficult_hint);
		} 
		// exit cache area
		else if (distance > cacheProximity && isNear) {
			isNear = false;
		}
		// on the way to cache area
		else {
			blipPause = 4.0f * Mathf.InverseLerp(20, 150, distance);
		}

		// blip volume increases if player is looking in the direction the cache
		newVolume = Vector3.Angle(this.transform.forward, targetCache.transform.position - this.transform.position);
		newVolume = 1.0f - Mathf.InverseLerp(0, 200, newVolume);
	
		// change target cache
		if (Input.GetKeyDown (KeyCode.P)) {
			cacheI = (cacheI + 1) % NUM_CACHES;
			targetCache = caches[cacheI];

			// TODO: play sonified difficulty and distance info about the selected cache
			// 	   : distance could be length of the tone
			//     : difficulty could be pitch
		}
	}
	
}