using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerZone : MonoBehaviour
{
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform bossSpawnPoint;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private GameObject arenaBoundaries;
    [SerializeField] private Transform PlayerPosSpawn;
    [SerializeField] private GameObject bossLevel;
    [SerializeField] private GameObject currentLevel;
    [SerializeField] private Animator sceneTransitionAnimator;
    [SerializeField] private Transform bossMaxBound;
    private Player player;

    private bool isActivated = false;

    // Kiểm tra player có đến vị trí để dịch chuyển tới boss chưa
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            StartCoroutine(ActivateBossSequence());// Chuyển cảnh sang màn đánh boss
        }
    }

    private IEnumerator ActivateBossSequence()
    {
        player = FindObjectOfType<Player>();// Tìm Player
        bossLevel.SetActive(true);// Mở cảnh đánh boss
        currentLevel.SetActive(false);
        // Di chuyển Player
        player.transform.position = PlayerPosSpawn.position;
        player.OnInit();
        Camera cam = Camera.main;
        cam.GetComponent<CameraFollow>().SetMaxBound(bossMaxBound);// Thiết lập lại giới hạn camera để không đi ra ngoài map
        sceneTransitionAnimator.gameObject.SetActive(true);// Bắt đầu anim chuyển cảnh
        yield return new WaitForSeconds(1.5f);
        // Kích hoạt rào chắn
        arenaBoundaries.SetActive(true);
        // Tạo boss
        BossController bossController = FindObjectOfType<BossController>();
        bossController.InitiateBattle();// Khởi động boss
        yield return true;
    }
}
