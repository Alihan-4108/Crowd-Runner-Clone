using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level", order = 0)]
public class LevelSO : ScriptableObject
{
    public Chunk[] chunks;
}