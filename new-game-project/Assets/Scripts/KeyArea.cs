using Godot;
using System;

public partial class KeyArea : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
	}

	public void _on_body_entered_key(CharacterBody2D body) {
		if (body == GetNode<CharacterBody2D>("/root/world/TileMap/MC-boy")) {
			GetNode<Node2D>("/root/world/Key").Hide();
		}
	}
}
