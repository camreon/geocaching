using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(AudioSource))]
//require the script to have a certain type of Unity component
//the only component all Unity GameObjects have is a Transform (containing position, rotation and scale)

//in this case, we are relying on messages being passed by the CharacterMotor script to trigger sound playback
//if you do a search in CharacterMotor.cs, you will find several "SendMessage" functions that will call
//the functions OnJump(), OnFall() and OnLand()

public class CharacterSounds : MonoBehaviour {
	
	public AudioClip jump;
	public AudioClip land;
	public AudioClip fall;
	
	CharacterMotor motor; //our character motor
	
	// Use this for initialization
	void Start () {
		//grab the CharacterController on start so we can reference it later if necessary
		
		//"GetComponent" calls can get expensive, so if you are using a Component frequently, 
		//(especially in the Update loop!) store it in a variable
		motor = GetComponent<CharacterMotor>();
	}

	// Update is called once per frame
	void Update() {

	}

	void OnJump() //from CharacterMotor SendMessage("OnJump")
	{
		//Debug.Log ("OnJump");
		audio.PlayOneShot(jump);
	}
	
	void OnFall() //from CharacterMotor SendMessage("OnFall")
	{
		//Debug.Log ("OnFall");

		//we can lower the volume of individual sounds used with the same source
		//if we pass in a "volumeScale" factor

		float volumeScale = 0.2f;

		audio.PlayOneShot(fall, volumeScale);
	}

	void OnLand() //from CharacterMotor SendMessage("OnLand")
	{
		//Debug.Log ("OnLand");
		
		//Debug.Log (motor.maximumYVelocityInAir); 
		//gave us values from ~-0f to -9.8f when inspected in the Console
		
		//you need the included ExtensionMethods.cs to use 'Remap'
		float volumeScale = motor.maximumYVelocityInAir.Remap(0, -9.8f, 0, 1);
		
		audio.PlayOneShot(land, volumeScale);
		
	}
}
