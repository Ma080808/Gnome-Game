using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Animator platformAnimator;
    
    [SerializeField] Transform waypointUp;
    [SerializeField] Transform waypointDown;
    [SerializeField] Transform waypointLeft;
    [SerializeField] Transform waypointRight;

    [SerializeField] float normalSpeed = 5f;
    float currentSpeed;
    [SerializeField] float slowEffectDuration = 5f;


    [SerializeField][Range(0,1)] int horizontal;
    [SerializeField][Range(0,1)] int vertical;

    int currentDirectionX, currentDirectionY;

    private void Start()
    {
        platformAnimator = GetComponent<Animator>();
        currentDirectionX = horizontal;
        currentDirectionY = vertical;
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        if (transform.position.x <= waypointLeft.position.x) { currentDirectionX = 1; }
        if (transform.position.x >= waypointRight.position.x) { currentDirectionX = -1; }
        if (transform.position.y <= waypointDown.position.y) { currentDirectionY = 1; }
        if (transform.position.y >= waypointUp.position.y) { currentDirectionY = -1; }
    }


    private void FixedUpdate()
    {
        Debug.Log("Current Speed on Fixed Update:" + currentSpeed);
        transform.Translate(currentSpeed * currentDirectionX * Time.deltaTime, currentSpeed * currentDirectionY * Time.deltaTime, 0f);    
    }


    public IEnumerator SlowDownCoroutine(float slowPercentage)
    {
        currentSpeed = normalSpeed * (1 - slowPercentage);

        yield return new WaitForSeconds(slowEffectDuration);

        platformAnimator.Play("MeltIce");
    }

    public void SlowDown(float slowPercentage)
    {
        platformAnimator.SetTrigger("frozen");
        StartCoroutine(SlowDownCoroutine(slowPercentage));
    }

    public void SpeedUp()
    {
        currentSpeed = normalSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
