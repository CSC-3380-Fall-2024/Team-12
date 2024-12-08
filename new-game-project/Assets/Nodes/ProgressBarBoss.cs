using Godot;
using System;

public partial class ProgressBarBoss : ProgressBar
{
	public int enemybosshp = 10;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Value = enemybosshp; // Optional, set the initial health value on ready
	}

	public void enemyhpLoseHealth()
	{
		enemybosshp--;
		this.Value = enemybosshp;
	}
	
}

