using Godot;
using System;
using System.Collections.Generic;

public partial class BG_Img : Node2D
{
    [Export] private PackedScene Window;
    [Export] private PackedScene Chest;
    [Export] private PackedScene Bookshelf;

    private RandomNumberGenerator rng = new RandomNumberGenerator();
    private List<Node2D> SpawnedObjects = new List<Node2D>();
    private const int MaxSpawnedObj = 6;
    private float spawnOffset = 1500;
    private const float MinSpawnDistance = 250f;

    public override void _Ready() {
        rng.Randomize();
        SpawnInitialObjects();
    }

    private void SpawnInitialObjects() {
        Vector2 windowPos = new Vector2(200, 200);
        Vector2 chestPos = new Vector2(400, GetFloorYPosition(1)); 
        Vector2 bookshelfPos = new Vector2(600, GetFloorYPosition(2));

        SpawnObjectAtPosition(Window, windowPos);
        SpawnObjectAtPosition(Chest, chestPos);
        SpawnObjectAtPosition(Bookshelf, bookshelfPos);
    }

    private void SpawnObjectAtPosition(PackedScene objectType, Vector2 position) {
        if (IsPositionValid(position)) {
            Node2D newObject = (Node2D)objectType.Instantiate();
            newObject.Position = position;
            GetParent().AddChild(newObject); 
            SpawnedObjects.Add(newObject);
        }
    }

    public override void _Process(double delta) {
        if (SpawnedObjects.Count < MaxSpawnedObj) {
            SpawnObjectsAhead();
        }
        for (int i = SpawnedObjects.Count - 1; i >= 0; i--) {
            Node2D obj = SpawnedObjects[i];
            if (obj.Position.X < GetViewport().GetVisibleRect().Position.X - 200) {
                obj.QueueFree();
                SpawnedObjects.RemoveAt(i);
            }
        }
    }

    private void SpawnObjectsAhead() {
        while (SpawnedObjects.Count < MaxSpawnedObj) {
            int choice = rng.RandiRange(0, 2);
            PackedScene objectType = GetObjectType(choice);
            Vector2 spawnPosition = new Vector2( GetViewport().GetVisibleRect().Size.X + spawnOffset, GetFloorYPosition(choice));

            if (IsPositionValid(spawnPosition)) {
                SpawnObjectAtPosition(objectType, spawnPosition);
            }
        }
    }

    private PackedScene GetObjectType(int choice) {
        switch (choice) {
            case 0:
                return Window;
            case 1:
                return Chest;
            case 2:
                return Bookshelf;
            default:
                return Chest;
        }
    }

    private float GetFloorYPosition(int objectType) {
        switch (objectType) {
            case 0:
                return 200; //Window POS.
            case 1:
            case 2:
                return GetViewport().GetVisibleRect().Size.Y - 150; // Floor position for chest and bookshelf
            default:
                return GetViewport().GetVisibleRect().Size.Y - 50;
        }
    }

    private bool IsPositionValid(Vector2 position) {
        foreach (Node2D spawnedObj in SpawnedObjects) {
            if (position.DistanceTo(spawnedObj.Position) < MinSpawnDistance) {
                return false;
            }
        }
        return true;
    }
}