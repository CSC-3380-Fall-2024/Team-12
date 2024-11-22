using Godot;
using System;
using System.Collections.Generic;

public partial class BgLoader : ParallaxBackground
{	
	[Export] private ParallaxLayer Layer;
	private int NumBG = 6; //Number of background screens to load in
	private float ScrollSpeed = 100f; 
	private float BG_Width = 998;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Pre-Loading BG Imgs
		for (int i = 0; i < NumBG; i++) {
			var instance =(Sprite2D)Layer.Duplicate();
			AddChild(instance);
			instance.Position = new Vector2(i * BG_Width, 0);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public void _Process(float delta)
	{
		foreach (var child in GetChildren()) {
			if (child is Sprite2D layer) {
				layer.Position -= new Vector2(ScrollSpeed * delta, 0);
				if (layer.Position.X < -BG_Width) {
					layer.Position += new Vector2(NumBG * BG_Width, 0);
				}
			}
		}
	}
}
