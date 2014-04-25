using UnityEngine; 
using System.Collections;

public class PickUpSound : MonoBehaviour 
{
	private bool enter;
	public AudioClip pickUpSound;
		
	void Start()
	{
		enter = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
			enter = true;
	}
	
	void OnTriggerExit(Collider other)
	{ 
		if(other.gameObject.CompareTag("Player"))
			enter = false;
	}
	
	void Update()
	{
		if(Input.GetKeyUp(KeyCode.T))
			if (enter)
				audio.PlayOneShot(pickUpSound);
	}
}