using Godot;
using System;

public partial class ProgressBar : Godot.ProgressBar
{
	int hp = 5; 
	
	// Called when the node enters the scene tree for the first time.
	
	public override void hpLoseHealth(){

			hp--; 
			Godot.ProgressBar = hp;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void hpGainHealth(){
			hp++;
			healthbar.value = hp;
	}
}









