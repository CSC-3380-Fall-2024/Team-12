using Godot;
using System;
using System.IO;

public partial class LoadingScreen : Control
{
	private string pathToScene;
	private Godot.ProgressBar loading; 
	private bool loadingNow;
	int count = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		loading = GetNode<Godot.ProgressBar>("LoadingBar");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		if (loadingNow == true) {
			var loadingProgress = new Godot.Collections.Array();
			var amountLoaded = ResourceLoader.LoadThreadedGetStatus(pathToScene, loadingProgress);
			if (amountLoaded == ResourceLoader.ThreadLoadStatus.InProgress) {
				loading.Value = Convert.ToDouble(loadingProgress[0]) * 100; //array returns a number from 0 - 1 of how much the file has loaded, multiply by 100 to get percent amount.
			}
			else if (amountLoaded == ResourceLoader.ThreadLoadStatus.Loaded) {
				loading.Value = 100;
				loadingNow = false;
			}
		}
		else {

			if (Input.IsAnythingPressed()) {
				EnterScene();
			}
		}
	}

	public void LoadScene(string path) {
		Show();
		pathToScene = path;
		ResourceLoader.LoadThreadedRequest(pathToScene);
		loadingNow = true;
	}

	public void EnterScene() {
		GetTree().ChangeSceneToFile(pathToScene);
	}
}
