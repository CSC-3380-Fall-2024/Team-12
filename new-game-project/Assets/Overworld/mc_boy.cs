// using UnityEngine;

// public class CharacterController2D : MonoBehaviour
// {
//     private const float ACCELERATION = 800f;
//     private const float FRICTION = 500f;
//     private const float MAX_SPEED = 120f;

//     private enum State { IDLE, RUN }
//     private State state = State.IDLE;

//     private Vector2 velocity;
//     private Vector2 blendPosition = Vector2.zero;

//     private Animator animator;
//     private string[] animationStateKeys = { "idle", "run" };

//     void Start()
//     {
//         animator = GetComponent<Animator>();
//     }

//     void Update()
//     {
//         float delta = Time.deltaTime;
//         Move(delta);
//         Animate();
//     }

//     private void Move(float delta)
//     {
//         Vector2 inputVector = new Vector2(
//             Input.GetAxis("Horizontal"),
//             Input.GetAxis("Vertical")
//         ).normalized;

//         if (inputVector == Vector2.zero)
//         {
//             state = State.IDLE;
//             ApplyFriction(FRICTION * delta);
//         }
//         else
//         {
//             state = State.RUN;
//             ApplyMovement(inputVector * ACCELERATION * delta);
//             blendPosition = inputVector;
//         }

//         transform.Translate(velocity * delta);
//     }

//     private void ApplyMovement(Vector2 amount)
//     {
//         velocity += amount;
//         velocity = Vector2.ClampMagnitude(velocity, MAX_SPEED);
//     }

//     private void ApplyFriction(float amount)
//     {
//         if (velocity.magnitude > amount)
//         {
//             velocity -= velocity.normalized * amount;
//         }
//         else
//         {
//             velocity = Vector2.zero;
//         }
//     }

//     private void Animate()
//     {
//         animator.Play(animationStateKeys[(int)state]);
//         animator.SetFloat("BlendX", blendPosition.x);
//         animator.SetFloat("BlendY", blendPosition.y);
//     }
// }
