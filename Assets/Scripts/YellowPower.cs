using UnityEngine;

public class YellowPower : MonoBehaviour
{
    float timeToTorns;

    public float TimeToTorns { get => timeToTorns; set => timeToTorns = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            collision.gameObject.GetComponent<Spikes>().ToFlower(TimeToTorns);
        }

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
