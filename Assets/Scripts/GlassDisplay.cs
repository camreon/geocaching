using UnityEngine;
using System.Collections;

public class GlassDisplay : MonoBehaviour {

	public int width = 200;
	public int height = 130;
	int leftMost;
	int topMost;
	
	public Texture2D dot;
	public GameObject cacheA;
	public GameObject cacheB;
	public GameObject cacheC;
	public GameObject cacheD;
	public GameObject cacheE;
	public AudioClip very_easy_difficulty;
	public AudioClip easy_difficulty;
	public AudioClip medium_difficulty;
	public AudioClip hard_difficulty;
	public AudioClip very_hard_difficulty;
	GameObject[] caches;
	GameObject targetCache;
	int cacheI = 0;
	int NUM_CACHES = 5;
	bool glassOn = true;
	public int size = 5;

	// Use this for initialization
	void Start () {
		leftMost = Screen.width - width;
		topMost = 0;
		caches = new GameObject[NUM_CACHES];
		caches[0] = cacheA;
		caches[1] = cacheB;
		caches[2] = cacheC;
		caches[3] = cacheD;
		caches[4] = cacheE;
		targetCache = caches[0];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			cacheI = (cacheI + 1) % NUM_CACHES;
			targetCache = caches[cacheI];
			if(targetCache == cacheB) //hardcoding cacheB as a 'very hard' cache
				audio.PlayOneShot(very_hard_difficulty);
			else if(targetCache == cacheA) //very easy cache
				audio.PlayOneShot(very_easy_difficulty);
			else if(targetCache == cacheC)
				audio.PlayOneShot(medium_difficulty);
			else if(targetCache == cacheD)
				audio.PlayOneShot(hard_difficulty);
			else if(targetCache == cacheE)
				audio.PlayOneShot(easy_difficulty);
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			glassOn = !glassOn;
		}
	}

	// Updates once per frame also
	void OnGUI () {
		if (glassOn) {
			GUI.Label(new Rect(0, topMost, width, height), "<size=12> \"P\": Switch Target Cache \n \"O\": Toggle Display \n \"T\": Pick Up Cache \n \"H\": Play Hint</size>");
			GUI.Box (new Rect (leftMost, topMost, width, height), " ");

			// draw player and target cache on radar
			float rot = this.transform.eulerAngles.y * 2 * Mathf.PI / 360;
			int myX = Screen.width - (width / 2 + 5);
			int myY = height / 2 - 5;
			GUI.DrawTexture (new Rect (myX, myY, 10, 10), dot);
			float cacheXoff = (targetCache.transform.position.x - this.transform.position.x) * 0.5f;
			float cacheYoff = (targetCache.transform.position.z - this.transform.position.z) * 0.5f;
			float rotCacheXoff = cacheXoff * Mathf.Cos (rot) - cacheYoff * Mathf.Sin (rot);
			float rotCacheYoff = cacheXoff * Mathf.Sin (rot) + cacheYoff * Mathf.Cos (rot);
			if (myX + rotCacheXoff > Screen.width - width && myY - rotCacheYoff < height - 10) {
				GUI.DrawTexture (new Rect (myX + rotCacheXoff, myY - rotCacheYoff, 10, 10), dot);
			}
		}
	}
}
