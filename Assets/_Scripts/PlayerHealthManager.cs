using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public int playerHealth = 2; // Player's health points
    public GameObject damagePanel; // Panel to activate on first hit
    public string gameOverSceneName; // Name of the scene to load on second hit
    public AudioSource audioSource; // Audio source to play the sound

    private void Start()
    {
        damagePanel.SetActive(false); // Ensure the panel is initially inactive
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        playerHealth--;

        // Play the hit sound effect if audio source is assigned
        if (audioSource != null)
        {
            audioSource.Play();
        }

        if (playerHealth == 1)
        {
            // Activate the damage panel on the first hit
            damagePanel.SetActive(true);
        }
        else if (playerHealth <= 0)
        {
            // Change to the game over scene on the second hit
            SceneManager.LoadScene(gameOverSceneName);
        }
    }
}
