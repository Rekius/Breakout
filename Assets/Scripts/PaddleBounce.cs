using UnityEngine;

public class PaddleBounce : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;
        
        AudioManager.Instance.Play(SoundType.PaddleHit);

        BallScript ball = collision.gameObject.GetComponent<BallScript>();
        Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();

        float paddleHalfWidth = GetComponent<Collider2D>().bounds.extents.x;
        float hitFactor = (collision.transform.position.x - transform.position.x) / paddleHalfWidth;

        Vector2 direction = new Vector2(hitFactor, 1f).normalized;
        ballRb.linearVelocity = direction * ball.BallSpeed;
    }
}
