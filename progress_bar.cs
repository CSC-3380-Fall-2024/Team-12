using Godot; 
using System;


# Called when the node enters the scene tree for the first time.
public class progBar : ProgressBar
{

	int hp = 5; 
	public ProgressBar healthbar;
	
	public override void hpLoseHealth(){

			hp--; 
			healthbar.value = hp;
	}
	public override void hpGainHealth(){
			hp++;
			healthbar.value = hp;
	}
}
