using Godot;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

public partial class Player : CharacterBody2D
{
	public const float Speed = 250f;              // Max speed of the player
	public const float JUMP_VELOCITY = -800f;     // Jump velocity
	public const float GRAVITY = 2000f;            // Gravity force
	public const float JUMP_DELAY = 0.08f;          // Delay in seconds before jump happens
	public const float DECELERATION = 600f;        // Deceleration factor
	public const float ACCELERATION = 800f;        // Acceleration factor


	private ProgressBar health;
	private int[] healtharray = new int[4];

	private bool interactable = false;

	private bool interactableCookie = false;


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


		health = GetNode<ProgressBar>("HealthBar/ProgressBar");
		healtharray[1] = 0;
		hparray();

	}

	public void hparray()
	{
		if (healtharray[1] == 0)
		{
			health.hp = 5;
		}
		else if (healtharray[1] == 1)
		{
			health.hp = 10;

		}
		else if (healtharray[1] == 2)
		{
			health.hp = 15;
		}

		health.Value = health.hp;
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

		if (interactable && Input.IsActionJustPressed("interact"))
		{
			interactableCookie = false;
			GD.Print("Interacting in zone. Updating health...");
			healtharray[1] = 1;
			hparray();
			interactable = false;
		}

		if (interactableCookie && Input.IsActionJustPressed("food"))
		{
			interactable = false;
			GD.Print("your gay 9000000000");
			hparray();
			interactableCookie = false;

		}
	}
	public void AreaEnteredHP(Node body)
	{
		if (body is Player)
		{
			interactableCookie = true;

			GD.Print("your gay 1");

		}
	}

	public void AreaExitedHP(Node body)
	{
		if (body is Player)
		{
			interactableCookie = false;
			GD.Print("your gay 3");
		}
	}

	public void AreaEntered(Node body)
	{
		if (body is Player)
		{

			interactable = true;
			GD.Print("Player entered a general area");

		}

	}
	public void AreaExited(Node body)
	{
		if (body is Player)
		{

			interactable = false;
			GD.Print("Player entered a general area");
		}
	}



	// if (Input.IsActionJustPressed("attack")){
	// 	Attack();
	// }





	private void OnJumpTimerTimeout()
	{

		if (jumpRequested)
		{
			Velocity = new Vector2(Velocity.X, JUMP_VELOCITY);
			jumpRequested = false;
		}
	}
}