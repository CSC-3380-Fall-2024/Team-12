using Godot;
using System;

public partial class ProgressBarEnemy : ProgressBar
{
	public int enemyhp = 4;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Value = enemyhp; // Optional, set the initial health value on ready
	}

	public void enemyhpLoseHealth()
	{
		enemyhp--;
		this.Value = enemyhp;
	}
}
