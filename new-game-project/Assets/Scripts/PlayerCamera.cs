using Godot;
using System;



public partial class PlayerCamera : Camera2D
{

	[Export] private TileMapLayer tilemap;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		TileSet tileSet = tilemap.TileSet;
		Vector2 Tilesize = tileSet.TileSize;
		Rect2 MapRect = tilemap.GetUsedRect();
		Vector2 WorldSize = MapRect.Size * Tilesize;
		float limit_right = WorldSize.X;
		float limit_bottom = WorldSize.Y; 
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
