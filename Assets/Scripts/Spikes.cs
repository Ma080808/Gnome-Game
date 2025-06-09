using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] Collider2D deathCollider;

    Animator spikesAnimator;

    private void Start()
    {
        spikesAnimator = GetComponent<Animator>();
    }

    public IEnumerator BackToTornsCoroutine(float timeToTorns)
    {
        yield return new WaitForSeconds(timeToTorns);

        spikesAnimator.Play("backToTorns");
    }

    public void ToFlower(float timeToTorns)
    {
        deathCollider.enabled = false;
        spikesAnimator.SetTrigger("onFlower");
        StartCoroutine(BackToTornsCoroutine(timeToTorns));
    }

    public void BackToTorns()
    {
        deathCollider.enabled = true;
    }
}
