using UnityEngine;
using System.Collections;

public class GlassDisplay : MonoBehaviour {

	public int leftMost = Screen.width - 200;
	public int topMost = 0;
	public int width = 200;
	public int height = 130;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Updates once per frame also
	void OnGUI () {
		GUI.Box (new Rect (leftMost, topMost, width, height), "I am a Glass");
	}
}
