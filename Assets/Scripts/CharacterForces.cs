using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterMotor))]
//require the script to have a certain type of Unity component
//the only component all Unity GameObjects have is a Transform (containing position, rotation and scale)

public class CharacterForces : MonoBehaviour {

	CharacterMotor motor; //our character controller

	//a public field that lets us tweak our forces in the Inspector while the scene is running.
	public float pushPower = 20.0f;

	// Use this for initialization
	void Start () {
		//grab the CharacterController on start so we can reference it later if necessary

		//"GetComponent" calls can get expensive, so if you are using a Component frequently, 
		//(especially in the Update loop!) store it in a variable
		motor = GetComponent<CharacterMotor>();

	}
	
	// Fixed update is called at a locked framerate to reduce Physics bugs if you put code here
	void FixedUpdate () {

	}


	void OnControllerColliderHit(ControllerColliderHit hit)
	{

		ApplyForce (hit);

		//Debug.Log (motor.movement.collisionFlags);

		//we can also check to see if we are colliding with an object in particular directions
		if ((motor.movement.collisionFlags & CollisionFlags.CollidedBelow) != CollisionFlags.None)
		{

			if (motor.movement.velocity.y < 0)
			{
				//Debug.Log (motor.movement.velocity.y);
				//Debug.Log ("Controller Collider Below Hit");
			}
			 
		}
	}

	//from https://docs.unity3d.com/Documentation/ScriptReference/CharacterController.OnControllerColliderHit.html
	void ApplyForce(ControllerColliderHit hit)
	{
		//collider.attachedRigidbody is a shortcut allowing you to grab the Rigidbody without having to call
		//e.g. hit.gameObject.GetComponent<Rigidbody>();
		//NOTE that this will return null if there is no rigid body, hence the later check
		Rigidbody body = hit.collider.attachedRigidbody;

		//we send the object this message... to use it create a function called "OnCharacterCollision"
		//in this case, we can pass an argument, so we are sending the velocity of the character controller
		//"Don't Require Receiver" prevents Unity from giving you tons of error flags in the console
		//see "CollisionAudioHandlerExample.cs" for a usage example

		hit.gameObject.SendMessage("OnCharacterCollision", motor.movement.velocity, SendMessageOptions.DontRequireReceiver);

		//if we have no rigidbody, or the rigidbody is kinematic 
		//(meaning it should not respond to Physics forces),
		//then we want to kick out before applying forces.

		//However, we still send the message above
		//in case we want to play a collision sound.

		if (body == null || body.isKinematic) 
			return;

		//NOTE: You will want to tweak the below code if you want more sophisticated behavior.
		//For instance, you may want to be able to push things along the Y axis.

		if (hit.moveDirection.y < -0.3F) 
			return; //we'd rather not be pushing things we are trying to jump on in most cases
		
		Vector3 pushDir = new Vector3(motor.movement.velocity.x, 0, motor.movement.velocity.z);
		body.velocity = pushDir * pushPower;

	}
}
