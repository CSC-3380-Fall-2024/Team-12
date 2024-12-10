using Godot;
using System;

public partial class LevelOneEntrance : Area2D
{
	private bool entered = false;
	private bool toTopDown = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {

	}

	public void _on_area_2d_body_entered(CharacterBody2D body) {
		if (body == GetNode<CharacterBody2D>("/root/world/TileMap/MC-boy")) {
			GetNode<WorldPt1>("/root/world").SwitchToLevelOne();
		}
	}

	public void _on_area_2d_body_exited(Node2D body) {
		entered = false;
	}
}
