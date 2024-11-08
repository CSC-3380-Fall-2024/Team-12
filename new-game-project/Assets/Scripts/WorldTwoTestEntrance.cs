using Godot;
using System;

public partial class WorldTwoTestEntrance : Area2D
{
	private bool entered = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		if (entered == true) {
			GD.Print("Press any button to enter scene");
			if (Input.IsAnythingPressed()) {
				GetTree().ChangeSceneToFile("res://Assets/Nodes/WorldOneTest.tscn");
			}
		}
	}

	public void _on_area_2d_body_entered(Node2D body) {
		entered = true;
	}

	public void _on_area_2d_body_exited(Node2D body) {
		entered = false;
	}
}
