using Godot;
using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public partial class Player : CharacterBody2D
{
	public float Speed = 250f;           
	public const float JUMP_VELOCITY = -800f;    
	public const float GRAVITY = 2000f;          
	public const float JUMP_DELAY = 0.08f;          
	public const float DECELERATION = 1000f;     
	public const float ACCELERATION = 800f;       

	private float dashSpeed = 600f;
	private float dashDuration = 0.2f;
	private float dashCoolDown = 1f;

	private Vector2 dashDirection = Vector2.Zero;
	private bool isDashing = false;
	private float dashTimeLeft = 0f;
	private float dashCoolDownTime = 0f;

	private Area2D hitbox;
	private ProgressBar health;

	private int[] healtharray = new int[4];

	private bool interactable = false;

	private bool interactableCookie = false;
	private bool interactablePotion = false;


	private Timer jumpTimer;
	private bool jumpRequested = false;

	private AnimatedSprite2D sprite;
	private AudioStreamPlayer Jump_SFX;


 private PackedScene armorPickup = (PackedScene)ResourceLoader.Load("res://armorPickup.tscn");
private PackedScene cookiePickup = (PackedScene)ResourceLoader.Load("res://cookie_remove.tscn");
  private PackedScene potionPickup = (PackedScene)ResourceLoader.Load("res://potionramove.tscn");
private AnimatedSprite2D hit;
private AnimatedSprite2D defmove;
private AnimatedSprite2D jumpAnimation;
 private Timer animationtime;
 private Area2D attackAOE;
 private ArmorGlobal cape;

private CollisionShape2D disablehitbox;
	public override void _Ready()
	{
		
		 cape = (ArmorGlobal)GetNode("/root/ArmorGlobal");
		animationtime = new Timer();
        animationtime .WaitTime = 0.5f; 
        animationtime .OneShot = true;
		AddChild(animationtime);
		
		jumpTimer = new Timer
		{
			WaitTime = JUMP_DELAY,
			OneShot = true
		};
		AddChild(jumpTimer);
		jumpTimer.Timeout += OnJumpTimerTimeout;

		sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		Jump_SFX = GetNode<AudioStreamPlayer>("Jump_SFX");

		health = GetNode<ProgressBar>("HealthBar/ProgressBar");
		healtharray[1] = 0;
		checkArmor();
		hparray();
		 hit = GetNode<AnimatedSprite2D>("hit");
		 hit.Visible = false;
		 defmove = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		defmove.Visible = true;
		jumpAnimation =  GetNode<AnimatedSprite2D>("jump");

		hitbox = GetNode<Area2D>("hitboxplayer");
		hitbox.AreaEntered += _on_hitboxarea_area_entered;
		attackAOE = GetNode<Area2D>("attackhitbox");
		disablehitbox = GetNode<CollisionShape2D>("attack/attackhitbox");
		
	}
	public void checkArmor(){
		if (cape.Armor == 0){
			healtharray[1]= 0;
		}else 
		{
			healtharray[1] = 1;
		}
	}
	public void hparray()
	{
		if (healtharray[1] == 0)
		{
			health.hp = 5;
			
		}else if ( healtharray[1] == 1){
		health.hp = 10;
	 	}
	 
	 health.Value = health.hp;

	 }
	   private void _on_hitboxarea_area_entered(Area2D area)
    {
		
		GD.Print("Area entered: ", area);
		        if (area.GetParent() is Enemy1)
        {
			//GD.Print("IN DA ZONE");
            health.Value -= 1;
        }
		else if (area.GetParent().Name == "Bear"){
			GD.Print("bear");
			health.Value -= 3;
		}
	}

	private void _on_attackhitbox_body_entered(Node body)
    {
		   
		if (body is Enemy1 enemy){
		enemy.TakeDamage(4);
		}
		if (body is Bear scary){
		scary.TakeDamage(1);
		}
		

            
        
	}
	 
    public override void _PhysicsProcess(double delta)
	{



		if (isDashing)
		{
			PerformDash((float)delta);
			return;
		}
		dashCoolDownTime = Mathf.Max(0f, dashCoolDownTime - (float)delta);
		HandleDashInput();



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
			Jump_SFX.Play();
			jumpTimer.Start();

		}
	

		
		int i =0;

			if(!IsOnFloor()){
			jumpAnimation.Visible =true;
			if (horizontalInput != 0)
		{
			Velocity = new Vector2(Mathf.MoveToward(Velocity.X, targetSpeed, ACCELERATION * (float)delta), velocityY);

			jumpAnimation.FlipH = horizontalInput < 0;
		}
		else
		{
			Velocity = new Vector2(Mathf.MoveToward(Velocity.X, 0, DECELERATION * (float)delta), velocityY);
		}
			defmove.Visible = false;
			i=0;
			}



			if(IsOnFloor()){
			jumpAnimation.Visible =false;
			defmove.Visible=false;
			if (i == 0){
				defmove.Visible = true;
				i++;
			}
			if(Input.IsActionJustPressed("hit")){
				defmove.Visible = false;
				i++;
				

				
			}
			
			}




		if (!jumpRequested)
		{
			velocityY += GRAVITY * (float)delta;
		}
		if(hit.Visible == true){
			defmove.Visible = false;
			jumpAnimation.Visible=false;
		}


if (Input.IsActionJustPressed("hit") && IsOnFloor())
		{
			hit.Visible = true;
			i = 2;
			defmove.Visible = false;
			GD.Print("attacking");
			animationtime.Start();
			
			
		
			






			if (horizontalInput != 0)
		{
			Velocity = new Vector2(Mathf.MoveToward(Velocity.X, targetSpeed, ACCELERATION * (float)delta), velocityY);

			hit.FlipH = horizontalInput < 0;
		}
		else
		{
			Velocity = new Vector2(Mathf.MoveToward(Velocity.X, 0, DECELERATION * (float)delta), velocityY);
		}
		}
		if (animationtime.TimeLeft <= 0 && hit.Visible)
        {
            hit.Visible = false; 
			defmove.Visible=true;
        }

		Velocity = new Vector2(Velocity.X, velocityY);
		MoveAndSlide();
	if (Input.IsActionJustPressed("takedmg")){
			health.Value -= 1;
		}
				if (health.Value == 0){
			GetTree().ChangeSceneToFile("res://Assets/Nodes/start_menu.tscn");
		}
				if (interactableCookie && Input.IsActionJustPressed("food")){

			interactable = false;
			GD.Print("9000000000");
			hparray();
			interactableCookie = false;
			Node cookiepiickup = cookiePickup.Instantiate();
			var gameNode = GetParent() as Node2D;
        		gameNode.AddChild(cookiepiickup);
        		cookiepiickup.Name = "foodPickup"; 
		}
				if (interactablePotion && Input.IsActionJustPressed("potion")){
			Speed = 4000f;
			interactablePotion = false;
			Node potionpick = potionPickup.Instantiate();
				var gameNode = GetParent() as Node2D;
        		gameNode.AddChild(potionpick);
        		potionpick.Name = "potionPickup"; 

		}

		if (interactable && Input.IsActionJustPressed("interact")){
			interactableCookie = false;
			GD.Print("12565556-");

		if (interactable && Input.IsActionJustPressed("interact"))
		{
			GD.Print("Interacting in zone. Updating health...");

			healtharray[1] = 1;
			hparray();
			interactable = false;
			Node armorPickupInstance = armorPickup.Instantiate();
			var gameNode = GetParent() as Node2D;
        		gameNode.AddChild(armorPickupInstance);
        		armorPickupInstance.Name = "armorPickup"; 
		}


	


		}
	}

	
	    private void OnTimeout()
    {
        hit.Visible = false; 
    }
		public void AreaEnteredHP(Node body)
			{
		if (body is Player)
		{
			interactableCookie = true;
			GD.Print("your in the hp zone");
		}
	}
		public void AreaEntered(Node body){
			if (body is Player){
				interactable = true;

				interactableCookie = true;
				interactablePotion = true;

			
			}
			
		}

		public void AreaExited(Node body){
			if (body is Player){
				
			interactable = false;

			interactableCookie = false;
			}
		}
		
	public void AreaExitedHP(Node body)
	{
		if (body is Player)
		{
			interactableCookie = false;
			GD.Print("left the hp zone");
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
		//GD.Print("Performing Dash");
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


