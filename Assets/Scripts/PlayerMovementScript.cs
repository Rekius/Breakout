using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float maxCenterX = 7.5f; //Max center position of the paddle

    private PlayerMovement controls;
    private Rigidbody2D rb;
    private BoxCollider2D col;

    private float direction;
    private float limitX; //Max border for the paddle movement
    private Vector3 baseScale; //Original scale of the paddle
    private Coroutine widenRoutine;

    private float HalfWidith => col.size.x * transform.lossyScale.x / 2f;

    private void Awake()
    {
        controls = new PlayerMovement();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        baseScale = transform.localScale;
        limitX = maxCenterX + HalfWidith;
    }

    private void OnEnable() => controls.Movement.Enable();
    private void OnDisable() => controls.Movement.Disable();

    private void Update()
    {
        direction = controls.Movement.Move.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        float currentMaxX = limitX - HalfWidith;

        Vector2 newPosition = rb.position + Vector2.right * direction * moveSpeed * Time.fixedDeltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -currentMaxX, currentMaxX);
        rb.MovePosition(newPosition);
    }

    public void WidenPaddle(float multiplier, float duration)
    {
        if (widenRoutine != null) StopCoroutine(widenRoutine);
        widenRoutine = StartCoroutine(WidenRoutine(multiplier, duration));
    }

    private IEnumerator WidenRoutine(float multiplier, float duration)
    {
        transform.localScale = new Vector3(baseScale.x * multiplier, baseScale.y, baseScale.z);

        yield return new WaitForSeconds(duration);

        transform.localScale = baseScale;
        widenRoutine = null;
    }

}
