using Godot;
using System;
using System.IO;
using System.Reflection.Metadata;

public partial class Dialog : Control
{
	private string[] dialog = new string[] {"Thank you, I've been needing to talk to someone all day! (Press right arrow to advance dialog)", "There is a teddy bear that isn't so friendly, and someone needs to take him down.", "It will take a lot of experience, I recommend finishing the first level first", "But, if you are ready, I have something for you...", "Here is your key to enter the boss fight."};
	private int index = 0;
	private bool start = false;
	public override void _Ready() {
		StartAndContinueConversation();
	} 

	public override void _Process(double delta) {
		
	}

	public void StartDialog() {
		start = true;
	}

	public override void _Input(InputEvent @event) {
        // Check if the event is a key press
        if (@event is InputEventKey keyEvent && keyEvent.Pressed && start == true && Input.IsActionPressed("ui_right")) {
            GoToNextSentence();
        }
    }

	public void StartAndContinueConversation() {
		GetNode<RichTextLabel>("NinePatchRect/Text").Text = dialog[index];
	}

	public void GoToNextSentence() {
		if (index < dialog.Length - 2) {
			index += 1;
			StartAndContinueConversation();	
		}
		else if (index == dialog.Length - 2){
			index += 1;
			StartAndContinueConversation();	
			GetNode<Node2D>("/root/world/Key").Show();
			GetNode<BossLevelEntrance>("/root/world/BossLevelEntrance").KeyObtained();
		}
		else {
			return;
		}
	}

	public void HideDialog() {
		Hide();
	}
}
