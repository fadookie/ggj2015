using UnityEngine;
using System.Collections;

public class RunnerGamePlayer : MonoBehaviour {
	public RunnerGame game;
	const int FRAMES_NO_COLLISION_CONSIDERED_VALID = 4;

	public float jumpBoostTime = 0.5f;
	public float jumpBoost = 10.0f;
	public float gravity = 10.0f;

	int collisionCount = 0;
	int framesNoCollision = 0;

	bool onTheGround = false;
	float currentJumpBoost = 0;
	float velocity;

	public Light particleLight;
	public ParticleEmitter sparksEmitter;
	
	// Use this for initialization
	void Start () {
		currentJumpBoost = jumpBoostTime;
		game = Services.instance.Get<RunnerGame>();
	}
	
	// Update is called once per frame
	void Update () {
		if(collisionCount <= 0)
		{
			framesNoCollision++;
		}
		else
		{
			framesNoCollision = 0;
		}

		onTheGround = framesNoCollision < FRAMES_NO_COLLISION_CONSIDERED_VALID;

		if (onTheGround)
		{
			currentJumpBoost = jumpBoostTime;
			velocity = 0f;
		}

		InputManager input = Services.instance.Get<InputManager>();
		if (input != null) {
			if (input.GetButton(game.PlayerIdx, InputManager.Button.Left))
			{
				if (onTheGround)
				{
					velocity = jumpBoost;
					framesNoCollision = FRAMES_NO_COLLISION_CONSIDERED_VALID;
				}
			}

#if true
			if (!onTheGround) 
			{
				if (input.GetButton(game.PlayerIdx, InputManager.Button.Left))
				{
					if (currentJumpBoost > 0f)
					{
						currentJumpBoost -= Time.fixedDeltaTime;
						velocity = jumpBoost;
					}
				}
				else
				{
					currentJumpBoost = 0f;
				}

			}
#endif
		}

		particleLight.enabled = currentJumpBoost > 0f && !onTheGround;
		sparksEmitter.emit = currentJumpBoost > 0f && !onTheGround;

		float tmpGravity = gravity;
		if (input != null) 
		{
			if (input.GetButton(game.PlayerIdx, InputManager.Button.Right))
			{
				tmpGravity *= 3.5f;
			}
		}
		if (!onTheGround)
		{
			velocity += -tmpGravity * Time.deltaTime;
		}



		Vector3 pos = transform.localPosition;
		pos.y += velocity * Time.deltaTime;
		transform.localPosition = pos;

		if (transform.localPosition.y < -6f)
		{
			game.ResetGame();
		}


	}

	void OnCollisionEnter(Collision collision)
	{
		Vector3 avgNormal = Vector3.zero;
		Vector3 avgPos = Vector3.zero;
		foreach(var contact in collision.contacts)
		{
			avgNormal += contact.normal;
			avgPos += contact.point;
		}
		avgPos /= collision.contacts.Length;
		avgNormal.Normalize();

		float dot = Vector3.Dot(avgNormal, Vector3.up);
		if (dot > 0.5f)
		{
			game.Score++;
			collisionCount++;
		}
		else
		{
			game.ResetGame();
		}
		transform.localPosition = avgPos + avgNormal * 0.49f;
	} 

	void OnCollisionExit(Collision collision)
	{
		collisionCount--;
		
	}
	
	public void ResetGame()
	{
		collisionCount = 0;
		transform.localPosition = Vector3.zero;
	}
}
