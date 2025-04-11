using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;

    [Header("Elements")]
    [SerializeField] private LevelSO[] levels;
    private GameObject finishLine;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        GenerateLevel();

        finishLine = GameObject.FindGameObjectWithTag("Finish");
    }

    private void GenerateLevel()
    {
        int currentLevel = GetLevel();

        currentLevel = currentLevel % levels.Length;

        LevelSO level = levels[currentLevel];

        CreateOrderedLevel(level.chunks);
    }

    private void CreateOrderedLevel(Chunk[] levelChunk)
    {
        Vector3 chunkPosition = Vector3.zero;

        for (int i = 0; i < levelChunk.Length; i++)
        {
            Chunk chunkToCreate = levelChunk[i];

            if (i > 0)
            {
                chunkPosition.z += chunkToCreate.GetLength() / 2;
            }

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);

            chunkPosition.z += chunkInstance.GetLength() / 2;
        }
    }

    public float GetFinishZ()
    {
        return finishLine.transform.position.z;
    }

    public int GetLevel()
    {
        return PlayerPrefs.GetInt("level", 0);
    }
}