using Godot;
using System;
using System.IO;

public partial class LoadingScreen : Control
{
	private string pathToScene;
	private ProgressBar loading; 
	private bool loadingNow;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		loading = GetNode<ProgressBar>("LoadingBar");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		if (loadingNow == true) {
			var loadingProgress = new Godot.Collections.Array();
			var amountLoaded = ResourceLoader.LoadThreadedGetStatus(pathToScene, loadingProgress);
			if (amountLoaded == ResourceLoader.ThreadLoadStatus.InProgress) {
				GD.Print("Here");
				loading.Value = Convert.ToDouble(loadingProgress[0]) * 100;
			}
			else if (amountLoaded == ResourceLoader.ThreadLoadStatus.Loaded) {
				GD.Print("Yay");
				loading.Value = 100;
				loadingNow = false;
			}
		}
		else {
			if (Input.IsAnythingPressed()) {
				enterScene();
			}
		}
	}

	public void loadScene(string path) {
		Show();
		pathToScene = path;
		GD.Print("Hey");
		ResourceLoader.LoadThreadedRequest(pathToScene);
		loadingNow = true;
	}

	public void enterScene() {
		GetTree().ChangeSceneToFile(pathToScene);
	}
}
