using Godot;
using System;

public partial class Enemy1 : CharacterBody2D
{
	//hello

	public float detectionRadius = 600f;
	public float CLOSERANGE = 200f;
	public float FARRANGE = 500f;
	[Export]
	public float speed = 300f;
	public const float GRAVITY = 2000f;            // Gravity force
	public const float DECELERATION = 1000f;        // Deceleration factor
	public const float ACCELERATION = 800f;

	private Random _rng = new Random();

	private enum EnemyState
	{
		idle,
		active,
		dance,
		attack
	}

	private EnemyState currentState;

	private Node2D player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentState = EnemyState.idle;
		player = GetNode<Node2D>("../MainCharacter");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{

		applyGravity(delta);


		if (Position.DistanceTo(player.Position) <= detectionRadius)
		{
			currentState = EnemyState.active;
		}
		else
		{
			currentState = EnemyState.idle;
		}

		if (currentState == EnemyState.idle)
		{
			HandleIdle();
		}
		if (currentState == EnemyState.active)
		{
			HandleActive(delta);
		}

		MoveAndSlide();
	}

	private void HandleIdle()
	{
		GD.Print("IDLE");
	}

	private void HandleActive(double delta)
	{
		if (Position.DistanceTo(player.Position) <= CLOSERANGE)
		{
			GD.Print("VERY CLOSE");
		}
		if (Position.DistanceTo(player.Position) >= FARRANGE)
		{
			GD.Print("VERY FAR");
		}

		GD.Print(Position.DistanceTo(player.Position));

		Vector2 direction = (player.Position - Position).Normalized();
		Position += direction * speed * (float)delta;
	}

	private void dance()
	{

	}

	public void applyGravity(double delta)
	{
		Velocity = new Vector2(Velocity.X, (float)(Velocity.Y + GRAVITY * delta));

	}
}
