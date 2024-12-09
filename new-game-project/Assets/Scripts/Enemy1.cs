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
	public const float GRAVITY = 2000f;
	public const float DECELERATION = 1000f;
	public const float ACCELERATION = 800f;

	private Random _rng = new Random();
	private int danceFramesRemaining = 0;
	private Vector2 danceDirection = Vector2.Zero;

	private enum EnemyState
	{
		idle,
		active,
		dance,
		attack
	}

	private EnemyState currentState;

	private Node2D player;

	private ProgressBarEnemy healthBar;


	public override void _Ready()
	{
		currentState = EnemyState.idle;
		player = GetNode<Node2D>("../MainCharacter");
		healthBar = GetNode<ProgressBarEnemy>("Enemy_hp/ProgressBarEnemy");
		healthBar.enemyhp = 4;
	
	}


	public override void _PhysicsProcess(double delta)
	{
		
		applyGravity(delta);
		HandleStateTransitions();


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
			handleActive(delta);
		}
		if (currentState == EnemyState.dance)
		{
			HandleDance(delta);
		}

		MoveAndSlide();
		
	}

	private void HandleIdle()
	{
		//GD.Print("IDLE");
	}

	public float acceleration = 1000f;
	public float deceleration = 1000f;

	private Vector2 velocity = new Vector2();

	private void handleActive(double delta)
	{
		Vector2 direction;
		float distanceToPlayer = Position.DistanceTo(player.Position);


		if (distanceToPlayer <= CLOSERANGE)
		{
			// GD.Print("TOO CLOSE, MOVING AWAY");
			direction = (Position - player.Position).Normalized();
			velocity = velocity.MoveToward(direction * speed, acceleration * (float)delta);
		}

		else if (distanceToPlayer >= FARRANGE)
		{
			//GD.Print("TOO FAR, MOVING TOWARD DANCE RANGE");
			direction = (player.Position - Position).Normalized();
			velocity = velocity.MoveToward(direction * speed, acceleration * (float)delta);
		}

		else if (distanceToPlayer > CLOSERANGE && distanceToPlayer < FARRANGE)
		{

			currentState = EnemyState.dance;
			velocity = velocity.MoveToward(Vector2.Zero, deceleration * (float)delta);
		}


		Position += velocity * (float)delta;
	}


	private void HandleDance(double delta)
	{

		if (danceFramesRemaining <= 0)
		{

			float randomHorizontal = _rng.Next(0, 2) == 0 ? -1f : 1f;

			danceDirection = new Vector2(randomHorizontal, 0);

			danceFramesRemaining = _rng.Next(30, 90);
			//GD.Print("New Dance Direction: ", danceDirection, " for ", danceFramesRemaining, " frames");
		}


		velocity = new Vector2(Mathf.MoveToward(velocity.X, danceDirection.X * speed, (float)(ACCELERATION * delta)), Velocity.Y);



		Velocity = velocity;
		MoveAndSlide();


		danceFramesRemaining--;
	}

	private void HandleStateTransitions()
	{
		if (currentState != EnemyState.dance || danceFramesRemaining <= 0)
		{
			if (Position.DistanceTo(player.Position) <= CLOSERANGE)
			{
				currentState = EnemyState.idle;
			}
			else if (Position.DistanceTo(player.Position) >= FARRANGE)
			{
				currentState = EnemyState.active;
			}
		}
	}

	public void applyGravity(double delta)
	{
		Velocity = new Vector2(Velocity.X, (float)(Velocity.Y + GRAVITY * delta));

	}
	    public void TakeDamage(int damage)
    {
		healthBar.enemyhp -= damage;
		healthBar.Value = healthBar.enemyhp;
        if (healthBar.Value <= 0)
        {
            QueueFree(); 
        	
   		 }
	}
}