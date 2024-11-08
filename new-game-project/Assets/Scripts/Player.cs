using Godot;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

public partial class Player : CharacterBody2D
{
	public const float Speed = 150f;
	public const float JumpVelocity = -200f;

    public override void _PhysicsProcess(double delta)
	{
		//input handling
		float horizontalInput = Input.GetAxis("move_left", "move_right");
		float velocityY = Velocity.Y;
		float targetSpeed = horizontalInput * Speed;

		if (horizontalInput != 0)
		{
			Velocity = new Vector2(Mathf.MoveToward(Velocity.X, targetSpeed, ACCELERATION * (float)delta), velocityY);

			sprite.FlipH = horizontalInput < 0;
		}
		else
		{
			velocity.Y = JumpVelocity;
		}

		if (Input.IsActionJustPressed("jump") && jumpTimer.IsStopped() && IsOnFloor())
		{
			Velocity = new Vector2(Mathf.MoveToward(Velocity.X, targetSpeed, ACCELERATION * (float)delta), velocityY);

			sprite.FlipH = horizontalInput < 0;
		}


		if (!jumpRequested)
		{
			Velocity = new Vector2(Mathf.MoveToward(Velocity.X, 0, DECELERATION * (float)delta), velocityY);
		}

		if (Input.IsActionJustPressed("jump") && jumpTimer.IsStopped() && IsOnFloor())
		{
			jumpRequested = true;
			jumpTimer.Start();
		}


		if (!jumpRequested)
		{
			velocityY += GRAVITY * (float)delta;
		}

		Velocity = new Vector2(Velocity.X, velocityY);
		MoveAndSlide();

		if (interactable && Input.IsActionJustPressed("interact")){

			GD.Print("Interacting in zone. Updating health...");
			healtharray[1] = 1;
			hparray();
		}
	}
<<<<<<< HEAD
		public void AreaEntered(Node body){
			if (body is Player){
				interactable = true;
			
			}
			
		}
		public void AreaExited(Node body){
			if (body is Player){
			interactable = false;
			}
		}

		// if (Input.IsActionJustPressed("attack")){
		// 	Attack();
		// }

	}


=======

	private void OnJumpTimerTimeout()
	{

		if (jumpRequested)
		{
			Velocity = new Vector2(Velocity.X, JUMP_VELOCITY);
			jumpRequested = false;
		}
	}
}
>>>>>>> origin/dev
