using Godot;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

public partial class Player : CharacterBody2D
{
	public const float Speed = 150f;
	public const float JumpVelocity = -200f;

	private ProgressBar health;
	private int[] healtharray = new int[4];
	
	private bool interactable = false;
	
	private bool interactableCookie = false;


     public override void _Ready()
     {
		health = GetNode<ProgressBar>("Control/ProgressBar");
		healtharray[1] = 0;
		hparray();
     }
	 public void hparray()
	 {
		if (healtharray[1] == 0)
		{
			health.hp = 5;
		}else if ( healtharray[1] == 1){
		health.hp = 10;

	 }else if (healtharray [1] == 2){
		health.hp = 15;
	 }

	 health.Value = health.hp;
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

		if (interactable && Input.IsActionJustPressed("interact")){
			interactableCookie = false;
			GD.Print("Interacting in zone. Updating health...");
			healtharray[1] = 1;
			hparray();
			interactable = false;
		}

		if (interactableCookie && Input.IsActionJustPressed("food")){
			interactable = false;
			GD.Print("your gay 9000000000");
			hparray();
			interactableCookie = false;
			
		}
		
	}
		public void AreaEntered(Node body){
			if (body is Player){
				
				interactable = true;
				GD.Print("Player entered a general area");
			
			}
			
		}
		public void AreaExited(Node body){
			if (body is Player){
				
			interactable = false;
			GD.Print("Player entered a general area");
			}
		}
		

		public void AreaEnteredHP(Node body){
			if (body is Player){
				interactableCookie = true;
	
				GD.Print("your gay 1");
			
			}
		}
		public void AreaExitedHP(Node body){
			if (body is Player){
			interactableCookie = false;
			GD.Print("your gay 3");
			}
		}
	}


