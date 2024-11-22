using Godot;
using System;

public partial class NpcArea : Area2D
{
	private bool entered = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		if (entered == true) {
			if (Input.IsAnythingPressed()) {
				GetTree().ChangeSceneToFile("res://Assets/Levels/LevelOne.tscn");
			}
		}
	}
}
