using Godot;
using System;

public partial class ProgressBar : Godot.ProgressBar
{
	int hp = 5; 
	
	// Called when the node enters the scene tree for the first time.
	
	public void hpLoseHealth(){

			hp--; 
			this.Value = hp;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void hpGainHealth(){
			hp++;
			this.Value = hp;
	}
}









