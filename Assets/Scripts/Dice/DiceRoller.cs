using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    public GameObject dicePrefab;      // 주사위 프리팹
    public Transform gridCenter;       // Grid 객체의 Transform (중심)
    public float yOffset = 2f;         // 던지는 높이 오프셋
    public float throwForce = 5f;      // 던지는 힘

    [Header("Custom Face Assets")]
    public GameObject[] diceFaces = new GameObject[6]; // 각 면에 해당하는 오브젝트 또는 이미지

    private GameObject currentDice;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RollDice();
        }
    }

    void RollDice()
    {
        if (currentDice != null)
            Destroy(currentDice); // 기존 주사위 제거

        Vector3 spawnPos = gridCenter.position + Vector3.up * yOffset;
        currentDice = Instantiate(dicePrefab, spawnPos, Random.rotation);
        
        Rigidbody rb = currentDice.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.up * throwForce + Random.onUnitSphere * 2f, ForceMode.Impulse);
            rb.AddTorque(Random.insideUnitSphere * 10f, ForceMode.Impulse);
        }

        ApplyCustomFaces(currentDice);
    }

    void ApplyCustomFaces(GameObject dice)
    {
        DiceFaceSetter setter = dice.GetComponent<DiceFaceSetter>();
        if (setter != null)
        {
            setter.SetFaces(diceFaces);
        }
    }
}
