using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas deathCanvas;
    PlayerController player;
    

    private void Awake()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    private void Start()
    {
        deathCanvas.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            player.Die();
            deathCanvas.enabled = true;
        }
    }
}
