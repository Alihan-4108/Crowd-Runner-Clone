using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform runnersParent;
    [SerializeField] private GameObject runnerPrefab;
    private PlayerAnimator playerAnimator;

    [Header("Settings")]
    [SerializeField] private float radius;
    private const float angle = 137.508f;

    private void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        PlaceRunners();
    }

    private void PlaceRunners()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Vector3 childLocalPosition = GetRunnerLocalPosition(i);
            runnersParent.GetChild(i).localPosition = childLocalPosition;
        }
    }

    private Vector3 GetRunnerLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);

        return new Vector3(x, 0, z);
    }

    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnersParent.childCount); // Tüm runnerlerin toplam alanını hesapla
    }

    public void ApplyBonus(BonusType bonusType, int bonusAmount)
    {
        switch (bonusType)
        {
            case BonusType.Addition:
                AddRunners(bonusAmount);
                break;
            case BonusType.Difference:
                RemoveRunner(bonusAmount);
                break;
            case BonusType.Product:
                int runnersToAdd = runnersParent.childCount * (bonusAmount - 1);
                // or  (runnersParent.childCount * bonusAmount) - runnersParent.childCount; 
                AddRunners(runnersToAdd);
                break;
            case BonusType.Division:
                int runnersToRemove = runnersParent.childCount - (runnersParent.childCount / bonusAmount);
                RemoveRunner(runnersToRemove);
                break;
            default:
                break;
        }
    }

    private void AddRunners(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(runnerPrefab, runnersParent);
        }

        playerAnimator.Run();
    }

    private void RemoveRunner(int amount)
    {
        if (amount > runnersParent.childCount)
            amount = runnersParent.childCount;

        int runnersAmount = runnersParent.childCount; //Last index

        for (int i = runnersAmount - 1; i >= runnersAmount - amount; i--)
        {
            Transform runnerToDestroy = runnersParent.GetChild(i);
            runnerToDestroy.SetParent(null); // Set parent to null before destroying

            Destroy(runnerToDestroy.gameObject);
        }
    }
}
