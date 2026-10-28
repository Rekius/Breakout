using System.Collections;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private Transform paddle;

    private float speedMultiplier = 1f;
    private Coroutine slowRoutine;
    private Coroutine launchRoutine;

    public float BallSpeed => speed * speedMultiplier;

    private PlayerMovement controls;

    [SerializeField] private float offsetY = 1f;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        controls.Movement.Enable();
    }

    private void OnDisable()
    {
        controls.Movement.Disable();
    }

    private void Awake()
    {
        controls = new PlayerMovement();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBall();
    }

    public void ResetBall()
    {
        if (launchRoutine != null) StopCoroutine(launchRoutine);
        launchRoutine = StartCoroutine(ResetRoutin());
    }

    private IEnumerator ResetRoutin()
    {
        rb.linearVelocity = Vector2.zero;

        Vector2 offset = Vector2.up * offsetY;

        while (!controls.Movement.LaunchBall.WasPressedThisFrame())
        {
            rb.position = (Vector2)paddle.position + offset;
            yield return null;
        }

        Vector2 direction = new Vector2(Random.Range(-1f, 1f), 1f).normalized;
        rb.linearVelocity = direction * BallSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Wall")) return;

        AudioManager.Instance.Play(SoundType.WallBounce);
    }

    public void SetBallSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SlowDown(float multiplier, float duration)
    {
        if (slowRoutine != null) StopCoroutine(slowRoutine);
        slowRoutine = StartCoroutine(SlowRoutine(multiplier, duration));
    }

    private IEnumerator SlowRoutine(float multiplier, float duration)
    {
        speedMultiplier = multiplier;
        ApplyCurrentSpeed();

        yield return new WaitForSeconds(duration);

        speedMultiplier = 1f;
        ApplyCurrentSpeed();
        slowRoutine = null;
    }

    private void ApplyCurrentSpeed()
    {
        rb.linearVelocity = rb.linearVelocity.normalized * BallSpeed;
    }
}
