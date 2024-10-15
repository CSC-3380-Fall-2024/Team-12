using Godot;
using System;

public partial class ProgressBar : Godot.ProgressBar
{
	int hp = 5; 
	
	
	
	public void hpLoseHealth(){

			hp--; 
			this.Value = hp;
	}

	public void hpGainHealth(){
			hp++;
			this.Value = hp;
	}
}









