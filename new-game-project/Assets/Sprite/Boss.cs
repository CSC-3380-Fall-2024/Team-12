using Godot;
using System;

public partial class Boss : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public const float LungeSpeed = 800.0f; 
	public const float LungeDuration = 0.2f; 
	private float lungeTimer = 0.0f; 
	private bool isLunging = false; 
	private double lastJumpTime = 0;
	private const double DoubleTapThreshold = .75; 

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (isLunging)
		{
			lungeTimer -= (float)delta;
			if (lungeTimer <= 0) {isLunging = false;}
		}

		if (!IsOnFloor() && !isLunging) {velocity += GetGravity() * (float)delta;}

		if (Input.IsActionJustPressed("ui_right"))
		{
			double currentTime = Time.GetTicksMsec() / 1000.0;
			if (currentTime - lastJumpTime <= DoubleTapThreshold && IsOnFloor())
			{
				isLunging = true;
				lungeTimer = LungeDuration;
				velocity.X = LungeSpeed;
			}
			else {lastJumpTime = currentTime;}
		}

		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor() && !isLunging)
		{
			velocity.Y = JumpVelocity;
		}

		if (!isLunging)
		{
			Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
			if (direction != Vector2.Zero) {velocity.X = direction.X * Speed;}
			else {velocity.X = Mathf.MoveToward(velocity.X, 0, Speed);}
			if (direction.X > 0) {Scale = new Vector2(1, Scale.Y); }
            else if (direction.X < 0) {Scale = new Vector2(-1, Scale.Y);}
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
