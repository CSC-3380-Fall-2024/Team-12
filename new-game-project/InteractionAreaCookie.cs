using Godot;
using System;

public partial class InteractionAreaCookie : Area2D
{
	// Called when the node enters the scene tree for the first time


	public void inTheZoneHP(Node body){
		if (body is Player player)
			player.AreaEntered(body);
	}
	public void notInTheZoneHP(Node body)
	{
		if (body is Player player)
		     player.AreaExited(body);
	}
	public override void _Ready()
	{
		this.BodyEntered += inTheZoneHP;
		this.BodyExited += notInTheZoneHP;
	}

}