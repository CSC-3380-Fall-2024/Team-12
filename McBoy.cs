using Godot;

public partial class McBoy : CharacterBody2D
{
	[Export] private float Speed = 150.0f;
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}

		Velocity = velocity;
		MovementAnimation();
		MoveAndSlide();
	}

	//handles movement animation
	void MovementAnimation()
	{
		Vector2 velocity = Velocity;
		var animatedSprite = GetNode("AnimatedSprite2D") as AnimatedSprite2D;

		// handles movement animations
		if (velocity.X < 0)
		{
			animatedSprite.Play("left");
		}
		else if (velocity.X > 0)
		{
			animatedSprite.Play("right");
		}
		else if (velocity.Y < 0)
		{
			animatedSprite.Play("up");
		}
		else if (velocity.Y > 0)
		{
			animatedSprite.Play("down");
		}
		else
		{
			animatedSprite.Play("idle");
		}

		// handles diagonal direction facing
		if (velocity.X > 0 && velocity.Y != 0)
		{
			animatedSprite.Play("right");
		}
		if (velocity.X < 0 && velocity.Y != 0)
		{
			animatedSprite.Play("left");

		}
	}
}