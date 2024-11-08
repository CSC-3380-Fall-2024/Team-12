using Godot;
using System;

public partial class InteractionArea : Area2D
{
	// Called when the node enters the scene tree for the first time


	public void inTheZone(Node body){
		if (body is Player player)
			player.AreaEntered(body);
	}
	public void notInTheZone(Node body)
	{
		if (body is Player player)
		     player.AreaExited(body);
	}
	public override void _Ready()
	{
		this.BodyEntered += inTheZone;
		this.BodyExited += notInTheZone;
	}

}