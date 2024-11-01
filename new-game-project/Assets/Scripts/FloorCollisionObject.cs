// using Godot;
// using System;

// public class floorCollisionObject : StaticBody2D
// {
//     [Export] public float parallaxSpeed = 50f;  
//     [Export] public float floorWidth = 1000f;  

//     private CollisionShape2D collisionShape;  
//     private Vector2 initialPosition; 
//     public override void _Ready() {
//         collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
//         initialPosition = Position;
//     }

//     public override void _PhysicsProcess(float delta) {
//         Position += new Vector2(-parallaxSpeed * delta, 0);
//         if (Position.x < -floorWidth) {
//             RepositionFloor();
//         }
//     }

//     private void RepositionFloor() {
//         Position += new Vector2(floorWidth * 2f, 0);
//     }
// }
