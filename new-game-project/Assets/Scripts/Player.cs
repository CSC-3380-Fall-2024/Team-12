using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 150f;
	public const float JumpVelocity = -200f;

	public AnimatedSprite2D animatedSprite;
	
	public override void _Ready(){
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		
		//gets input direction: -1, 0, 1
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
		//#flip the sprite if need be
		if (direction.X > 0){
			animatedSprite.FlipH = false;
		}
		else if(direction.X < 0){
			animatedSprite.FlipH = true;	
		}
		
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
