using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 150f;              // Max speed of the player
	public const float JUMP_VELOCITY = -700f;     // Jump velocity
	public const float GRAVITY = 2000f;            // Gravity force
	public const float JUMP_DELAY = 0.2f;          // Delay in seconds before jump happens
	public const float DECELERATION = 600f;        // Deceleration factor
	public const float ACCELERATION = 800f;        // Acceleration factor

	private Timer jumpTimer;
	private bool jumpRequested = false;

	private AnimatedSprite2D sprite;

	public override void _Ready()
	{
		// Instantiate and configure the jump delay timer
		jumpTimer = new Timer
		{
			WaitTime = JUMP_DELAY,
			OneShot = true
		};
		AddChild(jumpTimer);
		jumpTimer.Timeout += OnJumpTimerTimeout;  // Connect the timeout signal

		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		// Get input for horizontal movement
		float horizontalInput = Input.GetAxis("move_left", "move_right");
		float velocityY = Velocity.Y; // Store current vertical velocity
		float targetSpeed = horizontalInput * Speed; // Calculate target speed based on input

		// Accelerate or decelerate based on input
		if (horizontalInput != 0)
		{
			// Accelerate towards target speed
			Velocity = new Vector2(Mathf.MoveToward(Velocity.X, targetSpeed, ACCELERATION * (float)delta), velocityY);

			sprite.FlipH = horizontalInput < 0;
		}
		else
		{
			// Apply deceleration
			Velocity = new Vector2(Mathf.MoveToward(Velocity.X, 0, DECELERATION * (float)delta), velocityY);
		}

		// Check for jump input and ensure the player is on the floor
		if (Input.IsActionJustPressed("jump") && jumpTimer.IsStopped() && IsOnFloor())
		{
			jumpRequested = true;
			jumpTimer.Start();  // Start the jump delay timer
		}

		// Apply gravity if not jumping
		if (!jumpRequested)
		{
			velocityY += GRAVITY * (float)delta;
		}

		Velocity = new Vector2(Velocity.X, velocityY); // Update velocity
		MoveAndSlide();
	}

	private void OnJumpTimerTimeout()
	{
		// Only jump if the jump was requested
		if (jumpRequested)
		{
			Velocity = new Vector2(Velocity.X, JUMP_VELOCITY);
			jumpRequested = false; // Reset jump request
		}
	}
}
