using Godot;
using System;

public partial class WorldPt1 : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_button_pressed_overworldsettings() {
		GetNode<SettingsMenu>("/root/world/CanvasLayer/SettingsMenu").Show();
	}

	public void SwitchToLevelOne() {
		GetNode<LoadingScreen>("/root/world/CanvasLayer/Loading").LoadScene("res://Assets/Levels/LevelOne.tscn");
	}

	public void SwitchToBossLevel() {
		GetNode<LoadingScreen>("/root/world/CanvasLayer/Loading").LoadScene("res://Assets/Levels/BossLevel/BossLevel.tscn");
	}
}
