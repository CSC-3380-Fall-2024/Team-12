using Godot;
using System;
using System.IO;
using System.Reflection.Metadata;

public partial class Dialog : Control
{
	string[] dialog = new string[] {"Thank you, I've been needing to talk to someone all day!", "There is a teddy bear that isn't so friendly, and someone needs to take him down.", "It will take a lot of experience, I recommend finishing the first level first", "But, if you are ready, I have something for you...", "Here is your key to enter the boss fight."};
	int index = 0;
	public override void _Ready() {
		StartAndContinueConversation();
	} 

	public override void _Process(double delta) {
		
	}

	public override void _Input(InputEvent @event)
    {
        // Check if the event is a key press
        if (@event is InputEventKey keyEvent && keyEvent.Pressed)
        {
            GoToNextSentence();
        }
    }

	public void StartAndContinueConversation() {
		GetNode<RichTextLabel>("NinePatchRect/Text").Text = dialog[index];
	}

	public void GoToNextSentence() {
		if (index < dialog.Length - 1) {
			index += 1;
			StartAndContinueConversation();	
		}
		else {
			return;
		}
	}
}
