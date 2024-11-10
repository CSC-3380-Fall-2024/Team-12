using Godot;
using System;

public partial class SettingsMenu : Control
{
	HSlider slide;
	CheckBox check;
	OptionButton window;
	float volume = 0;
	ConfigFile config = new ConfigFile();


	public override void _Ready() {
		slide = GetNode<HSlider>("MarginContainer/VBoxContainer/VolumeControl");
		check = GetNode<CheckBox>("MarginContainer/VBoxContainer/Mute");
		window = GetNode<OptionButton>("MarginContainer/VBoxContainer/ChangeWindow");
		LoadData();
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
		SaveData("VolumeLevel", (Variant)value);
	}

	public void _on_mute_toggled(bool toggled) {
		if (toggled) {
			GD.Print("Audio Muted");
		}
		else {
			GD.Print("Audio Unmuted");
		}

		AudioServer.SetBusMute(0, toggled);
		SaveData("MuteOption", (Variant)toggled);
	}

	public void _on_exit_settings_pressed() {
		GetTree().ChangeSceneToFile("res://Assets/Nodes/start_menu.tscn"); //Change to change back to whatever scene it came from
	}

	public void _on_change_resolution_item_selected(int index) {
		if (index == 0) {
			ChangeToFullScreen();
		}
		else if (index == 1) {
			ChangeToWindowed();
		}
		else if (index == 2) {
			ChangeToBorderlessFullScreen();
		}
		else {
			ChangeToBorderlessWindowed();
		}
		SaveData("WindowOption", (Variant)index);
	}

	public void SaveData(string dataToSave, Variant value) {
		config.SetValue("Settings", dataToSave, value);
		config.Save("res://settings.cfg");
	}

	public void LoadData() {
		Error err = config.Load("res://settings.cfg");
		if (err != Error.Ok) {
    		return;
		}	
		slide.Value = (float)config.GetValue("Settings", "VolumeLevel");
		check.ButtonPressed = (bool)config.GetValue("Settings", "MuteOption");
		window.Selected = (int)config.GetValue("Settings", "WindowOption");
	}

	public void ChangeToFullScreen() {
		DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, false);
	}
	public void ChangeToWindowed() {
		DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
		DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, false);
	}
	public void ChangeToBorderlessFullScreen() {
		DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, true);
	}
	public void ChangeToBorderlessWindowed() {
		DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, true);
	}
}
