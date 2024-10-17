using Godot;
using System;

public partial class Topdown : CharacterBody2D
{
	public const float Speed = 150f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		if (Input.IsActionPressed("ui_left")){
				velocity.X -= 1;
		}
		if (Input.IsActionPressed("ui_right")){
				velocity.X += 1;
		}
		if (Input.IsActionPressed("ui_up")){
				velocity.Y -= 1;
		}
		if (Input.IsActionPressed("ui_down")){
				velocity.Y += 1;
		}
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}else{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}
		Velocity = velocity;
		MoveAndSlide();
	}
}
