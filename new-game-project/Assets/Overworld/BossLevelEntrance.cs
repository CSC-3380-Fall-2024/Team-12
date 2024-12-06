using Godot;
using System;

public partial class BossLevelEntrance : Area2D
{

	private bool hasKey = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {}

	public void _on_body_entered_boss(CharacterBody2D body) {
		if (body == GetNode<CharacterBody2D>("/root/world/TileMap/MC-boy") && hasKey == true) {
			GetNode<WorldPt1>("/root/world").SwitchToBossLevel();
		}
		else if (body == GetNode<CharacterBody2D>("/root/world/TileMap/MC-boy") && hasKey == false) {
			GD.Print("Need key first");
		}
	}

	public void _on_body_exitedboss() {

	}

	public void KeyObtained() {
		hasKey = true;
	}
}
