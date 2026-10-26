using System;
using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
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

        other.GetComponent<PlayerMovementScript>().WidenPaddle(multiplier, duration);
        Destroy(gameObject);
    }
}
