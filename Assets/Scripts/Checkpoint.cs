using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] Sprite checkpointSprite;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().sprite = checkpointSprite;
            FindFirstObjectByType<MySceneManager>().LastCheckpoint = transform;
        }
    }
}
