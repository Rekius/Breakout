using UnityEngine;

public class BrickSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private LevelData[] levels;
    [SerializeField] private float spacingX = 1.7f;
    [SerializeField] private float spacingY = 0.7f;

    public int LevelCount => levels.Length;

    public void SpawnLevel(int levelIndex)
    {
        string layout = levels[levelIndex].layout.Replace("\r","");
        string[] rows = layout.Split('\n');

        int maxWidth = 0;
        for (int row = 0; row < rows.Length; row++)
        {
            maxWidth = Mathf.Max(maxWidth, rows[row].Length);
        }
        float startX = -((maxWidth - 1) * spacingX) / 2f;

        for (int row = 0; row < rows.Length; row++)
        {
            for (int col = 0; col < rows[row].Length; col++)
            {
                char symbol = rows[row][col];

                if (symbol < '1' || symbol > '9') continue;
                
                int hits = symbol - '0';

                Vector2 position = new Vector2(
                    startX + col * spacingX,
                    transform.position.y - row * spacingY
                );

                GameObject brickObject = Instantiate(brickPrefab, position, Quaternion.identity, transform);
                brickObject.GetComponent<BrickScript>().Setup(hits);
                GameManager.Instance.RegisterBrick();
            }
        }
    }

    public float GetLevelSpeed(int levelIndex)
    {
        return levels[levelIndex].ballSpeed;
    }
}
