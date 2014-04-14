
using UnityEngine;
using System.Collections;

public class changeSoundByDistance : MonoBehaviour
{
	public float speed = 1.0f;
	public Transform start;
	public Transform end;
	Transform copy;
	public AudioClip nearVoice;
	public GameObject player;
	public AudioClip[] vocals = new AudioClip[4];
	
	void Start() 
	{
		StartCoroutine(SoundOut());
	}
	
	// NPC speaks when the player is near
	IEnumerator SoundOut() {
		while (true) {
			// get distance between npc and player
			float distance = Vector3.Distance(this.transform.position, player.transform.position);
			Debug.Log ("dist = " + distance);

			// if close enough then play sound
			if (distance < 60f) {
				audio.PlayOneShot(nearVoice);
				Debug.Log(this.name + " got hit and said it");
			}
			// loop through others if far away
			else {
				if (vocals.Length != 0) {
					int i = Random.Range (0,vocals.Length);
					audio.PlayOneShot(vocals[i]);
					Debug.Log(this.name + " is talking");
				}
			}
			yield return new WaitForSeconds(10);
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
