using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// using TMPro.Examples;

public class CollectItem : MonoBehaviour
{
    public float pickupRange = 10f;
    public float sphereRadius = 0.6f;
    public float pickupAngle = 60f;
    private bool isSwapping = false;
    private int? firstSlot = null;
    private int selectedSlot = -1; // 선택된 슬롯 인덱스 (-1이면 없음)
    public CameraSwitcher cameraSwitcher; // ← 인스펙터에서 연결 필수


    public LayerMask itemLayer;
    public string itemLayerName = "Item";
    public TextMeshProUGUI toastText;

    public TextMeshProUGUI pickupText;
    public InventoryUIController inventoryUI;  // UI 연동용

    private GameObject currentItem;
    private Inventory inventory = new Inventory();

    void Start()
    {
        itemLayer = LayerMask.GetMask(itemLayerName);
    }

    void Update()
    {
        ShowPickupPrompt();

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Debug.Log("[CollectItem] E키 눌림");
            TryPickupItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) selectedSlot = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) selectedSlot = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) selectedSlot = 2;

        if (Input.GetMouseButtonDown(0)) UseSelectedItem();

        if (Input.GetKeyDown(KeyCode.F))
        {
            isSwapping = true;
            firstSlot = null;
            Debug.Log("[swap 모드] 시작 : 바꿀 첫 번째 슬롯 번호를 누르세요 (1~3)");
        }

        if (isSwapping)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) HandleSwapInput(0);
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) HandleSwapInput(1);
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) HandleSwapInput(2);
        }
    }


    Camera GetActiveCamera()
    {
        if (cameraSwitcher == null)
        {
            Debug.LogWarning("CameraSwitcher가 연결되지 않았습니다.");
            return Camera.main;
        }

        return cameraSwitcher.IsFirstPersonCamera()
            ? cameraSwitcher.firstPersonCam
            : cameraSwitcher.thirdPersonCam;
    }

    void UseSelectedItem()
    {
        // Debug.Log($"[사용 시도] 현재 selectedSlot = {selectedSlot}");
        Camera cam = GetActiveCamera();

        if (selectedSlot < 0 || selectedSlot >= inventory.items.Count)
        {
            // Debug.LogWarning("[사용 실패] 선택된 슬롯이 비어 있거나 범위 초과");
            return;
        }

        GameObject item = inventory.GetItem(selectedSlot);
        if (item == null)
        {
            // Debug.LogWarning("[사용 실패] 해당 슬롯에 아이템이 없습니다.");
            return;
        }

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        Vector3 spawnPos;

        if (Physics.Raycast(ray, out hit, 10f)) // 최대 10m 거리까지
        {
            spawnPos = hit.point;
            spawnPos.y += 0.2f; // 땅 위에 살짝 뜨게
            // Debug.Log($"[소환 위치] 바닥 감지됨 → {spawnPos}");
        }
        else
        {
            // 바닥 못 찾았을 때 fallback: 시야 앞 1.5m, 현재 위치 y 유지
            spawnPos = transform.position + transform.forward * 1.5f;
            spawnPos.y = transform.position.y;
            // Debug.Log($"[소환 위치] 바닥 감지 실패 → fallback 위치 {spawnPos}");
        }

        // 👇 시야 각도 기반 스케일 계산
        Vector3 viewDirection = cam.transform.forward;
        Vector3 groundNormal = Vector3.up;
        float angle = Vector3.Angle(viewDirection, groundNormal); // 90도: 수평, 180도: 완전 아래
        float t = Mathf.InverseLerp(90f, 180f, angle); // 0~1 정규화
        float scaleFactor = Mathf.Lerp(0.5f, 2.0f, t); // 0.5배 ~ 2배

        // Debug.Log($"[스케일 계산] 시야각 = {angle:F1}°, 비율 t = {t:F2}, 스케일 = {scaleFactor:F2}");

        GameObject clone = Instantiate(item);
        clone.name = item.name;
        clone.tag = "Item";
        clone.layer = LayerMask.NameToLayer("Item");
        clone.SetActive(true);
        clone.transform.position = spawnPos;
        clone.transform.rotation = Quaternion.identity;
        clone.transform.localScale *= scaleFactor;

        inventory.RemoveItem(selectedSlot);
        inventoryUI.ClearSlotIcon(selectedSlot);
        selectedSlot = -1;
    }


    void HandleSwapInput(int slot)
    {
        if (firstSlot == null)
        {
            firstSlot = slot;
            Debug.Log($"[교환 모드] 첫 번째 슬롯: {slot + 1}번 선택됨. 바꿀 두 번째 슬롯 번호를 누르세요.");
        }
        else
        {
            int secondSlot = slot;
            int first = firstSlot.Value;
            firstSlot = null;
            isSwapping = false;

            inventory.SwapItems(first, secondSlot);
            inventoryUI.SwapIcons(first, secondSlot);
            Debug.Log($"[교환 완료] {first + 1}번 ↔ {secondSlot + 1}번 슬롯의 아이템이 교환되었습니다.");
        }
    }

    // void UseItem(int index)
    // {
    //     GameObject item = inventory.GetItem(index);
    //     if (item != null)
    //     {
    //         Debug.Log($"[사용] {index + 1}번 슬롯의 아이템 '{item.name}'을 사용합니다.");
    //         // TODO: 실제 사용 로직 여기에 추가
    //     }
    //     // else
    //     // {
    //     //     Debug.Log($"[사용 실패] {index + 1}번 슬롯이 비어있습니다.");
    //     // }
    // }

    void TryPickupItem()
    {
        Camera cam = GetActiveCamera();
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        RaycastHit hit;

        // ⬇️ OverlapSphere 대신 Raycast로만 감지
        if (Physics.Raycast(ray, out hit, pickupRange, itemLayer))
        {
            GameObject target = hit.collider.gameObject;

            // 1) 각도 검사 (원래대로)
            Vector3 eyePos = ray.origin;
            Vector3 dirToTarget = (target.transform.position - eyePos).normalized;
            float bestAngle = Vector3.Angle(ray.direction, dirToTarget);
            if (bestAngle > pickupAngle) return;

            // 2) 스케일 조절 (거리 기반 or 시야각 기반 등)
            float itemDistance = Vector3.Distance(eyePos, target.transform.position);
            float t   = Mathf.Clamp01(itemDistance / pickupRange);
            float scaleFactor = Mathf.Lerp(0.5f, 2.0f, t);
            target.transform.localScale *= scaleFactor;

            // 3) 인벤토리용 복제 + 저장
            GameObject clone = Instantiate(target);
            clone.SetActive(false);
            clone.name = target.name;
            clone.tag = "Item";
            clone.layer = LayerMask.NameToLayer("Item");

            ItemData data = target.GetComponent<ItemData>();
            if (data != null && data.icon != null)
            {
                int index = inventory.AddItem(clone);
                if (index == -1)
                {
                    Destroy(clone);
                    ShowToast("아이템이 꽉 찼습니다!");
                    return;
                }
                inventoryUI.AddItemToUI(data.icon);
                selectedSlot = index;
            }

            // 4) 원본 제거
            Destroy(target);
            currentItem = null;
        }
    }

    void ShowPickupPrompt()
    {
        Camera cam = GetActiveCamera();
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        Debug.DrawRay(ray.origin, ray.direction * pickupRange, Color.red);

        if (Physics.SphereCast(ray, sphereRadius, out var hit, pickupRange, itemLayer)
            && hit.collider.CompareTag("Item"))
        {
            currentItem = hit.collider.gameObject;
            pickupText.text = $"Press 'E' to pick up \"{currentItem.name}\"";
            pickupText.gameObject.SetActive(true);
        }
        else
        {
            pickupText.gameObject.SetActive(false);
            currentItem = null;
        }
    }


    void ShowToast(string message)
    {
        toastText.text = message;
        toastText.gameObject.SetActive(true);
        CancelInvoke(nameof(HideToast));
        Invoke(nameof(HideToast), 1.5f);
    }

    void HideToast()
    {
        toastText.gameObject.SetActive(false);
    }
}
