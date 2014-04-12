using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class CollisionAudioHandlerExample : MonoBehaviour {

	public AudioClip collisionSound; //the sound we use for collisions with this object

	private AudioSource audioSource; //a separate audioSource to play our collision sound

	// Use this for initialization
	void Start () {
		InitializeAudioSource(); //setup the extra audioSource
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitializeAudioSource()
	{
		//we setup a new AudioSource here to not conflict with any ambient sound effects
		//we may have put on objects in our scene

		//This lets us have an ambient loop play while also playing a different
		//sound on collision with the character or another object.

		if (audioSource != null) //if we have already set it up
			return;
		
		GameObject go = new GameObject();
		go.transform.parent = transform;
		go.name = "Collision-" + gameObject.name;
		audioSource = go.AddComponent<AudioSource>();
		audioSource.clip = collisionSound;
	}

	void OnCharacterCollision(Vector3 velocity) //from hit.gameObject.SendMessage("OnCharacterCollision")
												//allows for the character controller to tell our object
												//that it has been hit.
	{
		//Debug.Log("char" + gameObject.name + " " + velocity.magnitude);
		PlayCollisionSound(velocity.magnitude);
	}

	//https://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnCollisionEnter.html
	void OnCollisionEnter(Collision collision)
	{
		//Debug.Log("obj" + gameObject.name + " " + collision.relativeVelocity.magnitude);
		PlayCollisionSound(collision.relativeVelocity.magnitude);

	}

	void PlayCollisionSound(float magnitude)
	{
		//these values chosen are somewhat arbitrary... 
		//you will want to try different values to see what works well for you

		if (collisionSound == null || magnitude < 1.5f || audioSource.isPlaying)
			return;
		//checking for magnitude below a certain number prevents sounds when 
		//something is just barely colliding

		//What's "Remap?", look at ExtensionMethods.cs.
		audioSource.volume = magnitude.Remap(1.5f, 2.5f, 0, 1);
		//NOTE: if this line is giving you errors, make sure you have ExtensionMethods.cs as provided

		//a sneaky trick is to subtlely change the playback speed to make the same sound
		//have some variance. A bank of sounds is better for very repetitive sounds,
		//but this is better than nothing.

		audioSource.pitch = Random.Range (0.95f, 1.05f);

		//NOTE: for your assignment, you will also need to have this value be less arbitrary
		//and tied to something happening within the world... 
		
		//we encourage you to first LISTEN to how sounds behave in real life 
		//and then attempt to abstract that behavior in your code.


		//now finally play!
		audioSource.Play ();

		//Debug.Log ("Play Sound");

	}


}
