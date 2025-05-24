using UnityEngine;
using UnityEngine.UI;

public class HeartUIManager : MonoBehaviour
{
    public Image[] hearts; // 하트 이미지 3개 연결
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public int maxHealth = 3;
    public int currentHealth = 3;

    public Transform respawnPosition;
    public GameObject player;

    void Start()
    {
        UpdateHearts();
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        UpdateHearts();

        if (currentHealth <= 0)
        {
            RespawnPlayer();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateHearts();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }

    void RespawnPlayer()
    {
        if (player != null && respawnPosition != null)
        {
            player.transform.position = respawnPosition.position;
            player.transform.rotation = respawnPosition.rotation;

            currentHealth = maxHealth;
            UpdateHearts();
        }
    }
}