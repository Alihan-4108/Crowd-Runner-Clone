using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [Header("Elements")]
    private CrowdSystem crowdSystem;

    private void Start()
    {
        crowdSystem = GetComponent<CrowdSystem>();
    }

    private void Update()
    {
        DetectDoors();
    }

    private void DetectDoors()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, 1);

        for (int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Doors doors))
            {
                print("bişeylere değdik");

                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

                doors.DisabledCollider();

                crowdSystem.ApplyBonus(bonusType, bonusAmount);
            }
            
            else if (detectedColliders[i].CompareTag("Finish"))
            {
                print("Finish Line");
            }
        }
    }
}