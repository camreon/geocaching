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
	GameObject[] caches;
	GameObject targetCache;
	int cacheI = 0;
	int NUM_CACHES = 2;
	bool glassOn = true; 

	// Use this for initialization
	void Start () {
		leftMost = Screen.width - width;
		topMost = 0;
		caches = new GameObject[NUM_CACHES];
		caches[0] = cacheA;
		caches[1] = cacheB;
		targetCache = caches[0];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			cacheI = (cacheI + 1) % NUM_CACHES;
			targetCache = caches[cacheI];
		}
		if (Input.GetKeyDown (KeyCode.O)) {
			glassOn = !glassOn;
		}
	}

	// Updates once per frame also
	void OnGUI () {
		if (glassOn) {
			GUI.Box (new Rect (leftMost, topMost, width, height), "\"A\": Switch target cache \n \"O\": Toggle display");

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
