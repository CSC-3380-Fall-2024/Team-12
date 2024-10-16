using Godot;
using System;

public partial class MainChar : CharacterBody2D
{
	public const float Speed = 400.0f;
	public const float JumpVelocity = -900.0f;
	private AnimatedSprite2D sprite2D;

	public override void _Ready() {
		sprite2D = GetNode<AnimatedSprite2D>("Sprite2D");
		GD.Print(sprite2D);
	}
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (Math.Abs(velocity.X) > 1) {
			sprite2D.Animation = "running";
		}
		else {
			sprite2D.Animation = "default";
		}
		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
			sprite2D.Animation = "jumping";
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, 12);
		}

		Velocity = velocity;
		MoveAndSlide();
		//Flip and move the sprite
		bool isLeft = velocity.X < 0;
		sprite2D.FlipH = isLeft;
	}
}
