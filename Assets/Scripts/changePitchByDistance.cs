
using UnityEngine;
using System.Collections;

public class kidPitchShifter : MonoBehaviour
{
	public float speed = 1.0f;
	public Transform start;
	public Transform end;
	Transform copy;
	public AudioClip voice;
	public GameObject player;
	
	void Start() 
	{
		StartCoroutine(SoundOut());
	}
	
	// NPC speaks every 10 seconds	
	IEnumerator SoundOut() {
		while (true) {
			// get distance between kid and player
			float distance = Vector3.Distance(this.transform.position, player.transform.position);

			// base pitch change on distance magnitude
			distance /= 10.0f;
			if (distance >= 3f) 	 	// far away then pitch stays normal
				audio.pitch = 1f;
			else if (distance <= 1f) 	// closer and pitch increases
				audio.pitch = 3f;
			else
				audio.pitch = 3f - distance;
			
			// start playing at a start time
			audio.PlayOneShot(voice);
			
//			Debug.Log(this.name + " is talking");
			yield return new WaitForSeconds(8);
		}
	}
	
	// continuous movement
	void Update()
	{
		// Moving forward
		transform.position = Vector3.Lerp(transform.position, end.position, Time.deltaTime * speed);
		
		// Check if position is reached
		float dist = Vector3.Distance(transform.position, end.position);
		
		if (dist <= 1) {
			//switch positions
			copy = start;
			start = end;
			end = copy;
		}
	}
	
}