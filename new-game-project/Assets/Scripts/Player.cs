using Godot;
using System;
using System.Net.Http;

public partial class Player : CharacterBody2D
{
	public const float Speed = 150f;
	public const float JumpVelocity = -200f;

	private ProgressBar health;
	private int[] healtharray = new int[4];
	
	

	//private Area2D attackHitbox;

     public override void _Ready()
     {
		health = GetNode<ProgressBar>("Control/ProgressBar");
		healtharray[1] = 2;
		hparray();
    //     attackHitbox = GetNode<Area2D>("AttackHit");
	// 	attackHitbox.Monitoring = false;
	// 	attackHitbox.BodyEntered += Onhitbox;
     }
	 public void hparray()
	 {
		if (healtharray[1] == 0)
		{
			health.hp = 10;
		}else if ( healtharray[1] == 1){
		health.hp = 20;

	 }else if (healtharray [1] == 2){
		health.hp = 30;
	 }

	 health.Value = health.hp;
	 }
	 
	// public void Attack(){
	// 	attackHitbox.Monitoring = true;
	// 	SceneTreeTimer attackHitboxtime = GetTree().CreateTimer(0.5f);
	// 	attackHitboxtime.Timeout += noAttacky;
		
	// }
	// public void noAttacky(){
	// 	attackHitbox.Monitoring = false;
	// }
	// public void Onhitbox(Node body){
	// 	if (body.HasMethod("hpLoseHealth")){
	// 		body.Call("hpLoseHealth");
	// 	}
	// }

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
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
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

		// if (Input.IsActionJustPressed("attack")){
		// 	Attack();
		// }

	}

}
