using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class DiceRoller : MonoBehaviour
{
    public GameObject dicePrefab;      // 주사위 프리팹
    public Transform gridCenter;       // Grid 객체의 Transform (중심)
    public float yOffset = 2f;         // 던지는 높이 오프셋
    public float throwForce = 5f;      // 던지는 힘

    [Header("Custom Face Assets")]
    public GameObject[] diceFaces = new GameObject[6]; // 각 면에 해당하는 오브젝트 또는 이미지

    private List<GameObject> currentDiceList = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RollThreeDice();
        }
    }

    
    void RollThreeDice()
    {
        // 기존 주사위 삭제
        foreach (var dice in currentDiceList)
        {
            if (dice != null) Destroy(dice);
        }
        currentDiceList.Clear();

        // 3개의 주사위 생성 및 굴림
        for (int i = 0; i < 3; i++)
        {
            Vector3 offset = Vector3.right * (i - 1) * 1.5f; // 서로 간격 주기
            Vector3 spawnPos = gridCenter.position + Vector3.up * yOffset + offset;

            GameObject dice = Instantiate(dicePrefab, spawnPos, Random.rotation);
            Rigidbody rb = dice.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * throwForce + Random.insideUnitSphere * 2f, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * 10f, ForceMode.Impulse);
            }

            ApplyCustomFaces(dice);
            currentDiceList.Add(dice);
        }

        StartCoroutine(CheckTopFaces());

    }

    void ApplyCustomFaces(GameObject dice)
    {
        DiceFaceSetter setter = dice.GetComponent<DiceFaceSetter>();
        if (setter != null)
        {
            setter.SetFaces(diceFaces);
        }
    }

    IEnumerator CheckTopFaces()
    {
        yield return new WaitForSeconds(3f); // 주사위 멈출 시간

        List<string> topFaces = new List<string>();

        foreach (var dice in currentDiceList)
        {
            string face = GetTopFaceName(dice);
            topFaces.Add(face);
            Debug.Log("윗면 이름: " + face); // 이름 그대로 출력
        }

        if (topFaces.Count == 3 && topFaces[0] == topFaces[1] && topFaces[1] == topFaces[2])
        {
            Debug.Log("동일한 윗면 3개 - 유닛 소환 가능!");
        }
        else
        {
            Debug.Log("심볼이 다름 - 킵 또는 자원화로 진행");
        }
    }

    string GetTopFaceName(GameObject dice)
    {
        float maxDot = -1f;
        string topFaceName = "None";

        foreach (Transform child in dice.transform)
        {
            float dot = Vector3.Dot(child.up, Vector3.up);
            if (dot > maxDot)
            {
                maxDot = dot;
                topFaceName = child.name; // 자식 오브젝트의 이름을 그대로 저장
            }
        }

        return topFaceName;
    }



}
