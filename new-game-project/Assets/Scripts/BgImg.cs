using Godot;
using System;
using System.Collections.Generic;

public partial class BgImg : Node2D
{
	[Export] public Texture2D paintingOne;
	[Export] public Texture2D paintingTwo;
	[Export] public Texture2D paintingThree;
	[Export] public Texture2D paintingFour;

	[Export] public Vector2[] SpawnLocations = 
    {
        new Vector2(100, 200),
        new Vector2(650, 150),
		new Vector2(900, 250),
		new Vector2(1150, 175),
		new Vector2(1850, 150),
		new Vector2(2100,75),
		new Vector2(3450, 225),
		new Vector2(3900,250),
		new Vector2(4750,100),
		new Vector2(5450,250),
		new Vector2(6900,175),
		new Vector2(7650,150),
		new Vector2(8350,350),
		new Vector2(8800,200),
		new Vector2(9800,300),
		new Vector2(10650,150),
		new Vector2(11250,150),

    };

	public Vector2 ScaleOne = new Vector2(2.0f,2.0f);
	private Random _random = new Random();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (var location in SpawnLocations) {
			SpawnImg(location);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SpawnImg(Vector2 location) {
		Sprite2D sprite = new Sprite2D();
		sprite.Texture = ChooseImg();
		sprite.Position = location;
		sprite.Scale = ScaleOne;
		AddChild(sprite);
	}

	public Texture2D ChooseImg() {
		int rand_int = _random.Next(1,5);
		GD.Print("Painting: " + rand_int);
		return rand_int switch {
			1 => paintingOne,
			2 => paintingTwo,
			3 => paintingThree,
			4 => paintingFour,
		};
	}
}
