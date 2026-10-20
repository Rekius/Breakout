using UnityEngine;

[System.Serializable]
public class LevelData
{
    [TextArea(4, 12)]
    public string layout;
    public float ballSpeed = 6f;
}