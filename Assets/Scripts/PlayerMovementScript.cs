using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 5f;

    private float minX = -7.5f;
    private float maxX = 7.5f;

    private PlayerMovement controls;
    private Rigidbody2D rigidbody2D;
    private float direction;

    private void Awake()
    {
        controls = new PlayerMovement();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls.Movement.Enable();
    }

    private void OnDisable()
    {
        controls.Movement.Disable();
    }

    private void Update()
    {
        direction = controls.Movement.Move.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(math.clamp(rigidbody2D.position + Vector2.right * direction * moveSpeed * Time.fixedDeltaTime, minX, maxX));
    }
}
