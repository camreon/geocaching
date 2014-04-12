using UnityEngine;
using System.Collections;


/// <summary>
/// This is just for fun... a script that when added to your character controller allows you to shoot prefabs.
/// If something below is not clear, use the Unity docs! They are pretty good at giving you code examples
/// for most functions / member variables.
/// </summary>
public class CharacterObjectGun : MonoBehaviour {

	public GameObject projectile;
	//whatever GameObject prefab you put here becomes our "bullet"

	public float gunForce = 1000; 
	//try different values and see what happens

	public AudioClip gunSFX; //audio file to play for gun firing

	public float soundVolume = 1.0f;

	GameObject projectileParent; 
	//so we can collect all of our projectiles in one place
	//it keeps the Hierarchy cleaner

	AudioSource gunFireSource; //AudioSource for gun firing shot

	// Use this for initialization
	void Start () {
		projectileParent = new GameObject();
		projectileParent.name = "Projectiles";
		SetupGunFiringSoundSource (); //setup our sound object in a nicely encapsulated function
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) //you can use Input.GetKeyDown(KeyCode.A), etc as well
		{

			FireObject();

		}
	
	}

	void FireObject() {
		if (projectile == null) //if we didn't assign the projectile, forgetaboutit
			return;
		PlayFiringSound(); //play firing sound immediately (tells player they "fired")

		//instantiate a copy of the projectile object
		GameObject go = (GameObject)GameObject.Instantiate(projectile, 
		                                                   Camera.main.transform.position, 
		                                                   Camera.main.transform.rotation);

		go.transform.parent = projectileParent.transform; //make it a child of our parent container object
		go.transform.position += Camera.main.transform.forward * 2; //move it right in front of the camera's gaze
		go.transform.position += new Vector3(0, 0.5f, 0); //move it up a little so it is close to the center
		Vector3 gunForceVector = Camera.main.transform.forward * gunForce; //figure out the force direction
		gunForceVector.y += 10; //add an additional upward force for greater joy

		//this creates a random rotation so the box looks different every time we shoot it
		Vector3 rotationRandomization = new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));

		go.transform.eulerAngles = rotationRandomization; //apply rotation
		go.rigidbody.AddTorque(rotationRandomization / 180); //apply some torque to the box as well

		//finally... SHOOT
		go.rigidbody.AddForce(gunForceVector * go.rigidbody.mass); //apply the force to the object 

	}

	void SetupGunFiringSoundSource() {
		//we don't want to conflict with the AudioSource we already have on this GameObject,
		//so we make a new one that is a child of our player to play all of our sounds.

		GameObject gunFX = new GameObject(); 
		gunFX.name = "SFX_Gun"; // housekeeping
		gunFX.transform.parent = transform; //make this new source gameObject a child of this one
		gunFX.transform.localPosition = new Vector3(); //center sound on player's position
		gunFireSource = gunFX.AddComponent<AudioSource>(); //add our AudioSource for playback
		gunFireSource.panLevel = 0.2f; 	//reduce the 3D effect to potentially make the sound clearer,
										//see AudioSource.panLevel (and the other functions) docs!
		gunFireSource.clip = gunSFX; //assign our AudioClip for later playback
	}

	void PlayFiringSound() {
		if (gunFireSource.clip != null) //only play if we assigned a sound to the AudioClip slot!
		{
			if (gunFireSource.isPlaying) //if already playing, stop it before playing again
				gunFireSource.Stop ();
			gunFireSource.volume = soundVolume; //set the volume each time, in case it has been changed
			gunFireSource.Play();

		}
	}
}
