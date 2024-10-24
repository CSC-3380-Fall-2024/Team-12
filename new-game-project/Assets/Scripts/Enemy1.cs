using Godot;
using System;

public partial class Enemy1 : Node2D
{	
	private enum EnemyState{
		idle,
		active
	}
	
	private EnemyState currentState;
	
	private Node2D player;
	
	public float detectionRadius = 50f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentState = EnemyState.idle;
		player = GetNode<Node2D>("../Player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Position.DistanceTo(player.Position) <= detectionRadius){
			currentState = EnemyState.active;
		}
		else{
			currentState= EnemyState.idle;
		}
		
		if(currentState == EnemyState.idle){
			handleIdle();
		}
		else{
			handleActive(delta);
		}
	}
	
	private void handleIdle(){
		GD.Print("IDLE");
	}
	
	private void handleActive(double delta){
		GD.Print("ACTIVE");
		Vector2 direction = (player.Position - Position).Normalized();
		Position += direction * 100 * (float)delta;
	}
	
}
