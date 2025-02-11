using UnityEngine;
using System.Collections;

public class MoneyTree3D : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject coinPrefab; // 생성할 코인 프리팹
    [SerializeField] private int coinAmount = 1; // 생성할 코인의 수
    [SerializeField] private float interactCooldown = 0.05f;
    [SerializeField] private float spawnRangeX = 1f; // x축 범위
    [SerializeField] private float spawnRangeZ = 1f; // z축 범위

    private Outline outline;

    private bool canInteract = true;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }


    public void Interact()
    {
        if (canInteract)
        {
            SpawnCoins();
            StartCoroutine(InteractCooldown());
        }
    }

    public void OnFocus()
    {
        // 나무에 포커스되었을 때의 효과를 추가할 수 있습니다.
        Debug.Log("OnFocus outline: " + outline);
        if (outline == null) return;
        Debug.Log("OnFocus");
        outline.enabled = true;
    }

    public void OnDefocus()
    {
        // 나무에서 포커스가 해제되었을 때의 효과를 추가할 수 있습니다.
        if (outline == null) return;
        Debug.Log("OnDefocus");
        outline.enabled = false;
    }

    private void SpawnCoins()
    {
        for (int i = 0; i < coinAmount; i++)
        {
            // 반지름이 1인 원 둘레에 위치 계산
            float angle = Random.Range(0f, 2f * Mathf.PI); // 랜덤 각도
            Vector3 spawnPosition = new Vector3(
                transform.position.x + 2*Mathf.Cos(angle),
                transform.position.y + 1f,              // y축: +1 고정
                transform.position.z + 2*Mathf.Sin(angle)
            );

            // 코인 생성
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private IEnumerator InteractCooldown()
    {
        canInteract = false;
        yield return new WaitForSeconds(interactCooldown);
        canInteract = true;
    }
}