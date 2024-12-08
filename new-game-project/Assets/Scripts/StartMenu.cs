using Godot;
using System;

public partial class StartMenu : Node2D
{
	private Godot.Button button;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {

	}

	public void _on_button_pressed() {
		GetNode<Godot.Button>("StartGame").Hide();
		GetNode<Godot.Button>("SettingsButton").Hide();
		GetNode<Godot.Label>("GameName").Hide();
		GetNode<Sprite2D>("Bossbaby").Hide();
		GetNode<LoadingScreen>("Loading").LoadScene("res://Assets/Overworld/world_pt_1.tscn");
	}

	public void _on_settings_button_pressed() {
		// GetTree().ChangeSceneToFile("res://Assets/Nodes/settings_menu.tscn");
		GetNode<SettingsMenu>("/root/StartMenu/CanvasLayer/SettingsMenu").Show();
	}
}
