using System;
using Unity.InferenceEngine;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    [SerializeField] private GameObject breakEffect;
    [SerializeField] private Color[] hitColors;

    [SerializeField] private GameObject[] powerUpPrefabs;
    [SerializeField, Range(0, 1)] private float powerUpDropChance = 0.5f;

    private SpriteRenderer sr;
    private int hitLeft = 1;
    private int maxHits = 1;
    [SerializeField] private int breakPoints = 10;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Setup(int hits)
    {
        maxHits = Mathf.Clamp(hits, 1, hitColors.Length);
        hitLeft = maxHits;
        UpdateColor();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Ball")) return;
        
        hitLeft--;

        if (hitLeft <= 0)
        {
            Break();
        }
        else
        {
            BreakingEffect();
            UpdateColor();
            AudioManager.Instance.PlayBrickBreak();
        }
    }

    private void UpdateColor()
    {
        sr.color = hitColors[hitLeft - 1];
    }

    private void Break()
    {
        TryDropPowerUp();
        BreakingEffect();

        AudioManager.Instance.PlayBrickBreak();
        GameManager.Instance.BrickDestroyed(maxHits * breakPoints);
        Destroy(gameObject);
    }

    private void BreakingEffect()
    {
        GameObject fx = Instantiate(breakEffect, transform.position, Quaternion.identity);
        var main = fx.GetComponent<ParticleSystem>().main;
        main.startColor = sr.color;
    }

    private void TryDropPowerUp()
    {   
        if (powerUpPrefabs.Length == 0) return;
        if (UnityEngine.Random.value > powerUpDropChance) return;

        GameObject prefab = powerUpPrefabs[UnityEngine.Random.Range(0, powerUpPrefabs.Length)];
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
