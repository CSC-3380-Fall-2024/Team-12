using Godot;
using System;

public partial class SettingsMenu : Control
{
	HSlider slide;
	CheckBox check;
	Button button;
	float volume = 0;
	bool muted = false;


	public override void _Ready(){
		slide = GetNode<HSlider>("Volume Control");
		check = GetNode<CheckBox>("Mute");
		button = GetNode<Button>("ExitSettings");
		slide.Connect("value_changed", new Callable(this, nameof(_on_h_slider_value_changed)));
		check.Connect("toggled", new Callable(this, nameof(_on_mute_toggled)));
		button.Connect("toggled()", new Callable(this, nameof(_on_exit_settings_pressed)));
	}


	public void _on_h_slider_value_changed(float value) {
		if (value > volume) {
			GD.Print("Volume increased");
		}
		else if (value < volume) {
			GD.Print("Volume decreased");
		}
		volume = value;
		AudioServer.SetBusVolumeDb(0, value/3);
	}

	public void _on_mute_toggled(bool toggled) {
		if (toggled) {
			GD.Print("Audio Muted");
		}
		else {
			GD.Print("Audio Unmuted");
		}

		AudioServer.SetBusMute(0, toggled);
	}

	public void _on_exit_settings_pressed() {
		GetTree().ChangeSceneToFile("res://sprite_movement.tscn"); //Change to change back to whatever scene it came from
	}
}
