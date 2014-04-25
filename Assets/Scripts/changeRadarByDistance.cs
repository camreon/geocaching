
using UnityEngine;
using System.Collections;

public class changeRadarByDistance : MonoBehaviour
{
	public AudioClip radar, very_easy_hint, easy_hint, medium_hint, hard_hint, very_hard_hint;
	public float cacheProximity = 30.0f;
	float blipPause = 3.0f;
	float distance = 80.0f;
	float newVolume = .5f;

	public GameObject cacheA;
	public GameObject cacheB;
	public GameObject cacheC;
	public GameObject cacheD;
	public GameObject cacheE;
	GameObject[] caches;
	GameObject targetCache;
	int cacheI = 0;
	int NUM_CACHES = 5;
	bool isNear = false; 
	
	void Start() 
	{
		StartCoroutine(SoundOut());
		
		caches = new GameObject[NUM_CACHES];
		caches[0] = cacheA;
		caches[1] = cacheB;
		caches[2] = cacheC;
		caches[3] = cacheD;
		caches[4] = cacheE;

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
//		print (distance);

		// enter cache area
		if (distance < cacheProximity && !isNear) {
			isNear = true;

			// read out hints
			playHint();
			new WaitForSeconds(7);
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

		// repeat hint if needed with "H"
		if (Input.GetKeyDown (KeyCode.H)) {
			playHint();
		}
	}
	
	void playHint() {
		if(targetCache == cacheB) //'very hard' cache
			audio.PlayOneShot(very_hard_hint);
		else if(targetCache == cacheA) //very easy cache
			audio.PlayOneShot(very_easy_hint);
		else if(targetCache == cacheC)
			audio.PlayOneShot(medium_hint);
		else if(targetCache == cacheD)
			audio.PlayOneShot(hard_hint);
		else if(targetCache == cacheE)
			audio.PlayOneShot(easy_hint);
	}
}