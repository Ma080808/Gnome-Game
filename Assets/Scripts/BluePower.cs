using UnityEngine;

public class BluePower : MonoBehaviour
{
    float slowPercentage;

    public float SlowPercentage { get => slowPercentage; set => slowPercentage = value; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            collision.gameObject.GetComponent<MovingPlatform>().SlowDown(SlowPercentage);
        }

        Destroy(this.gameObject);
    }
}
