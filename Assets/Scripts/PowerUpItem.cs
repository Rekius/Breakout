using System;
using UnityEngine;
public enum PowerUpType
{
    WidePaddle,
    ExtraLife
}
public class PowerUpItem : MonoBehaviour
{
    [SerializeField] private PowerUpType type;
    [SerializeField] private float fallSpeed = 3f;
    [SerializeField] private float multiplier = 2f;
    [SerializeField] private float duration = 4f;

    private void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Apply(other);
        Destroy(gameObject);
    }

    private void Apply(Collider2D paddle)
    {
        AudioManager.Instance.Play(SoundType.PowerUpPickup);
        switch (type)
        {
            case PowerUpType.WidePaddle:
                paddle.GetComponent<PlayerMovementScript>().WidenPaddle(multiplier, duration);
                break;

            case PowerUpType.ExtraLife:
                GameManager.Instance.AddLife();
                break;
        }
    }
}
