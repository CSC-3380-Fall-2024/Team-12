using Godot;
using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

public partial class Player : CharacterBody2D
{
	public const float Speed = 250f;              // Max speed of the player
	public const float JUMP_VELOCITY = -800f;     // Jump velocity
	public const float GRAVITY = 2000f;            // Gravity force
	public const float JUMP_DELAY = 0.08f;          // Delay in seconds before jump happens
	public const float DECELERATION = 1000f;        // Deceleration factor
	public const float ACCELERATION = 800f;        // Acceleration factor

	private float dashSpeed = 600f;
	private float dashDuration = 0.2f;
	private float dashCoolDown = 1f;

	private Vector2 dashDirection = Vector2.Zero;
	private bool isDashing = false;
	private float dashTimeLeft = 0f;
	private float dashCoolDownTime = 0f;


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

		//dashing

		if (isDashing)
		{
			PerformDash((float)delta);
			return;
		}
		dashCoolDownTime = Mathf.Max(0f, dashCoolDownTime - (float)delta);
		HandleDashInput();


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

	private void HandleDashInput()
	{
		if (Input.IsActionJustPressed("dash") && dashCoolDownTime <= 0f)
		{
			Vector2 dir = GetInputDirection();
			GD.Print(dir);
			if (Input.IsActionPressed("move_left"))
			{
				GD.Print("LEFT PRESSED WHILE NOT DASHING STILL");
			}

			if (dir != Vector2.Zero)
			{
				GD.Print("DASHING", dir);
				StartDash(dir);
			}
		}
	}

	// private void HandleDashInput()
	// {

	// 	if (Input.IsActionJustPressed("dash") && dashCoolDownTime <= 0f)
	// 	{
	// 		Vector2 dir = Vector2.Zero;
	// 		if (Input.IsActionPressed("ui_left"))
	// 		{
	// 			GD.Print("DASHING");
	// 			dir = new Vector2(-1, 0);
	// 		}
	// 		if (Input.IsActionPressed("ui_right"))
	// 		{
	// 			GD.Print("DASHING");
	// 			dir = new Vector2(1, 0);
	// 		}
	// 		if (Input.IsActionPressed("ui_down"))
	// 		{
	// 			GD.Print("DASHING");
	// 			dir = new Vector2(0, 1);
	// 		}
	// 		if (Input.IsActionPressed("ui_up"))
	// 		{
	// 			GD.Print("DASHING");
	// 			dir = new Vector2(0, -1);
	// 		}
	// 		if (dir != Vector2.Zero)
	// 		{
	// 			StartDash(dir);
	// 		}
	// 	}
	// }

	private void StartDash(Vector2 dir)
	{
		isDashing = true;
		dashTimeLeft = dashDuration;
		dashCoolDownTime = dashCoolDown;

		dashDirection = dir.Normalized();
		Velocity = dashDirection * dashSpeed;
		GD.Print("dashDirection: " + dashDirection);
	}

	private void PerformDash(float delta)
	{
		GD.Print("Performing Dash");
		Velocity = dashDirection * dashSpeed;
		dashTimeLeft -= delta;

		if (dashTimeLeft <= 0f)
		{
			EndDash();
		}
		MoveAndSlide();
	}

	private void EndDash()
	{
		isDashing = false;
		if (IsOnFloor())
		{
			Velocity = new Vector2(0, 0);
		}
		Velocity = dashDirection * dashSpeed * 0.75f;
	}

	private Vector2 GetInputDirection()
	{
		Vector2 dir = Vector2.Zero;
		if (Input.IsActionPressed("move_left"))
		{
			dir.X -= 1;
		}
		if (Input.IsActionPressed("move_right"))
		{
			dir.X += 1;
		}
		if (Input.IsActionPressed("down"))
		{
			dir.Y += 1;
		}
		if (Input.IsActionPressed("up"))
		{
			dir.Y -= 1;
		}
		return dir.Normalized();
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