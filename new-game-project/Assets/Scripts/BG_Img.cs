using Godot;
using System;
using System.Collections.Generic;

public partial class BG_Img : Node2D
{
    // Exported PackedScenes for different objects
    [Export] private PackedScene Window;
    [Export] private PackedScene Chest;
    [Export] private PackedScene Bookshelf;

    // Random number generator for spawning
    private RandomNumberGenerator rng = new RandomNumberGenerator();

    // List to keep track of spawned objects
    private List<Node2D> SpawnedObjects = new List<Node2D>();

    // Constants to control spawning behavior
    private const int MaxSpawnedObj = 10; // Maximum number of objects in the scene at once
    private const float MinSpawnDistance = 150f; // Minimum distance between objects
    private const int MaxSpawnAttempts = 10; // Maximum attempts to find a valid spawn position

    // Cooldown timer to control spawn frequency
    private float SpawnCooldown = 0f;

    public override void _Ready()
    {
        // Initialize the random number generator
        rng.Randomize();

        // Spawn initial objects at fixed positions
        SpawnInitialObjects();
    }

    /// <summary>
    /// Spawns the initial set of objects when the scene starts.
    /// </summary>
    private void SpawnInitialObjects()
    {
        Vector2 basePosition = GlobalPosition;

        // Spawn a Window at (200, 200) relative to the spawner
        SpawnObjectAtPosition(Window, basePosition + new Vector2(200, 200), 0);

        // Spawn a Chest at (400, ChestY)
        SpawnObjectAtPosition(Chest, basePosition + new Vector2(400, GetFloorYPosition(1)), 1);

        // Spawn a Bookshelf at (600, BookshelfY)
        SpawnObjectAtPosition(Bookshelf, basePosition + new Vector2(600, GetFloorYPosition(2)), 2);
    }

    /// <summary>
    /// Spawns an object of a given type at the specified global position.
    /// </summary>
    /// <param name="objectType">The PackedScene to spawn.</param>
    /// <param name="position">The global position to spawn the object at.</param>
    /// <param name="objectTypeIndex">Index representing the type of object (0: Window, 1: Chest, 2: Bookshelf).</param>
    private void SpawnObjectAtPosition(PackedScene objectType, Vector2 position, int objectTypeIndex = 0)
    {
        // Use the position directly as the global position
        Vector2 globalPosition = position;

        // Check if the position is valid (no overlap)
        if (IsPositionValid(globalPosition, objectTypeIndex))
        {
            // Instantiate the object
            Node2D newObject = (Node2D)objectType.Instantiate();

            // Set the global position
            newObject.GlobalPosition = globalPosition;

            // Set ZIndex lower than the character's ZIndex to render behind
            newObject.ZIndex = 5;

            // Add the object to the scene
            AddChild(newObject);

            // Add the object to the list of spawned objects
            SpawnedObjects.Add(newObject);

            // Debug print
            GD.Print($"Spawned {objectType.ResourcePath} at {globalPosition}");
        }
        else
        {
            GD.Print($"Failed to spawn {objectType.ResourcePath} at {globalPosition} due to proximity constraints.");
        }
    }

    public override void _Process(double delta)
    {
        // Decrease the cooldown timer
        SpawnCooldown -= (float)delta;

        // Check if it's time to spawn a new object
        if (SpawnCooldown <= 0f)
        {
            // Only spawn if we haven't reached the maximum number of objects
            if (SpawnedObjects.Count < MaxSpawnedObj)
            {
                GD.Print($"Current SpawnedObjects.Count: {SpawnedObjects.Count}");
                SpawnObjectsAhead();
            }

            // Reset the cooldown with a new random value between 2 and 5 seconds
            SpawnCooldown = rng.RandfRange(2.0f, 5.0f);
        }

        // Clean up objects that have moved off-screen
        CleanupOffScreenObjects();

        // Update the camera position to follow the player with a vertical offset
        UpdateCameraPosition();
    }

    /// <summary>
    /// Updates the camera to follow the player with a specified vertical offset.
    /// </summary>
    private void UpdateCameraPosition()
    {
        // Attempt to get the MainCharacter node
        var player = GetNode<Node2D>("/root/LevelOne/MainCharacter");
        if (player == null)
        {
            GD.PrintErr("Error: Could not find MainCharacter node in UpdateCameraPosition.");
            return;
        }

        // Attempt to get the Camera2D node within MainCharacter
        var camera = GetNode<Camera2D>("/root/LevelOne/MainCharacter/Camera2D");
        if (camera == null)
        {
            GD.PrintErr("Error: Could not find Camera2D node in UpdateCameraPosition.");
            return;
        }

        // Define the vertical offset (e.g., 150 pixels above the player)
        float verticalOffset = -150f; // Negative value moves the camera up

        // Set the camera's global position to follow the player with the offset
        Vector2 desiredCameraPosition = new Vector2(player.GlobalPosition.X, player.GlobalPosition.Y + verticalOffset);
        camera.GlobalPosition = desiredCameraPosition;
    }

    /// <summary>
    /// Cleans up objects that have moved off-screen to the left of the camera.
    /// </summary>
    private void CleanupOffScreenObjects()
    {
        // Attempt to get the Camera2D node within MainCharacter
        var camera = GetNode<Camera2D>("/root/LevelOne/MainCharacter/Camera2D");
        if (camera == null)
        {
            GD.PrintErr("Error: Could not find Camera2D node in CleanupOffScreenObjects.");
            return;
        }

        // Calculate the left edge of the camera's viewport
        Vector2 cameraGlobalPosition = camera.GlobalPosition;
        float cameraLeftEdge = cameraGlobalPosition.X - (GetViewport().GetVisibleRect().Size.X / 2);

        // Iterate through the spawned objects in reverse to safely remove them
        for (int i = SpawnedObjects.Count - 1; i >= 0; i--)
        {
            Node2D obj = SpawnedObjects[i];

            // If the object is far enough to the left of the camera, remove it
            if (obj.GlobalPosition.X < cameraLeftEdge - 200)
            {
                GD.Print($"Removing object {obj.Name} at {obj.GlobalPosition}");
                obj.QueueFree();
                SpawnedObjects.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Spawns new objects ahead of the player based on random choices.
    /// </summary>
    private void SpawnObjectsAhead()
    {
        // Ensure we haven't reached the maximum number of spawned objects
        if (SpawnedObjects.Count >= MaxSpawnedObj)
        {
            GD.Print($"Spawn limit reached: {SpawnedObjects.Count}/{MaxSpawnedObj}");
            return;
        }

        // Attempt to spawn up to MaxSpawnAttempts times to find a valid position
        int attempts = 0;
        while (attempts < MaxSpawnAttempts)
        {
            // Randomly choose which object to spawn (0: Window, 1: Chest, 2: Bookshelf)
            int choice = rng.RandiRange(0, 2);
            PackedScene objectType = GetObjectType(choice);

            // Attempt to spawn ahead of the player by a random distance between 500 and 800 pixels
            float spawnX = GetPlayerPosition().X + rng.RandiRange(500, 800);

            // Determine the Y position based on the object type
            float spawnY = GetFloorYPosition(choice);

            // Create the spawn position
            Vector2 spawnPosition = new Vector2(spawnX, spawnY);

            // Check if overlapping is allowed on this attempt (last two attempts allow overlap)
            bool allowOverlap = attempts >= (MaxSpawnAttempts - 2);

            GD.Print($"Attempting to spawn {objectType.ResourcePath} at {spawnPosition} (Allow Overlap: {allowOverlap})");

            // Check if the position is valid
            if (IsPositionValid(spawnPosition, choice, allowOverlap))
            {
                // Spawn the object at the valid position
                SpawnObjectAtPosition(objectType, spawnPosition, choice);
                break;
            }

            attempts++;
        }

        if (attempts >= MaxSpawnAttempts)
        {
            GD.Print($"Failed to spawn object after {MaxSpawnAttempts} attempts.");
        }
    }

    /// <summary>
    /// Retrieves the PackedScene based on the random choice.
    /// </summary>
    /// <param name="choice">Integer representing the object type (0: Window, 1: Chest, 2: Bookshelf).</param>
    /// <returns>The corresponding PackedScene.</returns>
    private PackedScene GetObjectType(int choice)
    {
        return choice switch
        {
            0 => Window,
            1 => Chest,
            2 => Bookshelf,
            _ => Chest // Default to Chest if out of range
        };
    }

    /// <summary>
    /// Returns a fixed Y position based on the object type to ensure consistency.
    /// </summary>
    /// <param name="objectType">Integer representing the object type (0: Window, 1: Chest, 2: Bookshelf).</param>
    /// <returns>Fixed Y position for the object.</returns>
    private float GetFloorYPosition(int objectType)
    {
        return objectType switch
        {
            0 => 200f, // Window Y position
            1 => GetViewport().GetVisibleRect().Size.Y - 150f, // Chest Y position
            2 => GetViewport().GetVisibleRect().Size.Y - 150f, // Bookshelf Y position (same as Chest)
            _ => GetViewport().GetVisibleRect().Size.Y - 150f // Default to Chest Y position
        };
    }

    /// <summary>
    /// Checks if the spawn position is valid based on distance constraints.
    /// </summary>
    /// <param name="position">The position to check.</param>
    /// <param name="objectType">Integer representing the object type.</param>
    /// <param name="allowOverlap">Whether to allow overlapping with existing objects.</param>
    /// <returns>True if the position is valid; otherwise, false.</returns>
    private bool IsPositionValid(Vector2 position, int objectType, bool allowOverlap = false)
    {
        float allowedDistance = MinSpawnDistance;

        // Customize the allowed distance per object type if needed
        if (objectType == 0) // Window
            allowedDistance *= 0.8f; // Slightly closer for windows
        else if (objectType == 1) // Chest
            allowedDistance *= 1.0f; // Default distance
        else if (objectType == 2) // Bookshelf
            allowedDistance *= 1.2f; // Slightly farther for bookshelves

        // Check distance to all existing spawned objects
        foreach (Node2D spawnedObj in SpawnedObjects)
        {
            float distance = position.DistanceTo(spawnedObj.GlobalPosition);
            if (distance < allowedDistance)
            {
                if (!allowOverlap)
                {
                    GD.Print($"Invalid position {position} due to proximity ({distance}) to {spawnedObj.Name} at {spawnedObj.GlobalPosition}");
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// Retrieves the player's current global position.
    /// </summary>
    /// <returns>The player's global position as a Vector2.</returns>
    private Vector2 GetPlayerPosition()
    {
        var player = GetNode<Node2D>("/root/LevelOne/MainCharacter");
        if (player == null)
        {
            GD.PrintErr("Error: Could not find MainCharacter node in GetPlayerPosition.");
            return Vector2.Zero;
        }
        return player.GlobalPosition;
    }
}
