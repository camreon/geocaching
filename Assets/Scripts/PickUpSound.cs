using UnityEngine; 
using System.Collections;

public class PickUpSound : MonoBehaviour 
{
	private bool enter;
		
	void Start()
	{
		enter = false;
	}

	void OnTriggerEnter()
	{
		enter = true;
	}
	
	void OnTriggerExit()
	{ 
		enter = false;
	}
	
	public AudioClip pickUpSound;
	
	void Update()
	{
		if(Input.GetKeyUp(KeyCode.T))
			if (enter)
				audio.PlayOneShot(pickUpSound);
	}
}