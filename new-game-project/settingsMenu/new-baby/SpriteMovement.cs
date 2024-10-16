using Godot;
using System;

public partial class SpriteMovement : Node2D
{
	Button button;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		button = GetNode<Button>("goToSettings");
		button.Connect("press", new Callable(this, "_on_button_pressed"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {

	}

	public void _on_button_pressed() {
		GetTree().ChangeSceneToFile("res://settings_menu.tscn");
	}
}
