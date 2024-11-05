using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 150f;              // Max speed of the player
	public const float JUMP_VELOCITY = -400f;     // Jump velocity
	public const float GRAVITY = 2000f;            // Gravity force
	public const float JUMP_DELAY = 0.08f;          // Delay in seconds before jump happens
	public const float DECELERATION = 600f;        // Deceleration factor
	public const float ACCELERATION = 800f;        // Acceleration factor

	private Timer jumpTimer;
	private bool jumpRequested = false;

	private AnimatedSprite2D sprite;

	public override void _Ready()
	{
		//delay timer
		jumpTimer = new Timer
		{
			WaitTime = JUMP_DELAY,
			OneShot = true
		};
		AddChild(jumpTimer);
		jumpTimer.Timeout += OnJumpTimerTimeout;

		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

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
	}

	private void OnJumpTimerTimeout()
	{

		if (jumpRequested)
		{
			Velocity = new Vector2(Velocity.X, JUMP_VELOCITY);
			jumpRequested = false;
		}
	}
}
