using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindFirstObjectByType<LevelDoor>().PlayerHasKey = true;
            Destroy(this.gameObject);
        }
    }
}
