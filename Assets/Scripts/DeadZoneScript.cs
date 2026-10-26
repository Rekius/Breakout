using UnityEngine;

public class DeadZoneScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            GameManager.Instance.LoseLife();
        }
        if (collision.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);
        }
    }
}
