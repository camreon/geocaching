using UnityEngine;
using System.Collections;

/// <summary>
/// Simple script that scales up the gameObject's transform that it is attached to (as shown during class)
/// </summary>
public class SuperSized : MonoBehaviour {

	public bool shouldGrow = false;

	public float growRate = 1.0f; 
	//public variables are exposed in the Inspector and can be tweaked in Play Mode!
	//but note any changes you make in Play mode are only temporary...


	//Frequent Unity Bugs seen by Maribeth 
	//(as mentioned in class, why you may run your code and see 'nothing'.):
	//
	// 1- Your objects could be huge and the camera is inside them.
	// 2- Your objects are very, very small.
	// 3- Objects can be way off in "the void" (far away from the origin of the camera).
	//		Happens frequently with Physics interactions.
	//		Check Rigidbody settings if things are "flipping out".

	// Use this for initialization
	void Start () {
		//gameObject.transform.localScale = new Vector3(3, 3, 3);
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.localScale += new Vector3(growRate, growRate, growRate);	
	}
}
