using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [Header("Movement configs")]
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] Collider2D groundChecker;

    [Header("RedPower configs")]
    [SerializeField] float dashForce = 10f;
    [SerializeField] float normalGravityScale = 8f;
    [SerializeField] float dashGravityTimer = 0.2f;
    [SerializeField] Transform gnomeGFX;

    [Header("BluePower configs")]
    [SerializeField] GameObject freezeProjectilePrefab;
    [SerializeField][Range(0, 1)] float slowPercentage;

    [Header("YellowPower configs")]
    [SerializeField] GameObject sunProjectilePrefab;
    [SerializeField] float timeToTorns;

    [Header("Power Projectiles Configs")]
    [SerializeField] float projectileSpeed = 35f;
    [SerializeField] float projectileUpForce = 35f;
    [SerializeField] Transform shootPos;

    GameManager gameManager;
    float moveInput;
    bool isDashing = false;
    Rigidbody2D myRigibody;
    bool isDead = false;

    private void Awake()
    {
        myRigibody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void OnMove(InputValue value)
    {
        if (isDead) { return; }
        moveInput = value.Get<float>();
    }

    void OnJump(InputValue value)
    {
        if (isDead) { return; }
        if (value.isPressed)
        {
            if (groundChecker.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                myRigibody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    void OnRedPower(InputValue value)
    {
        if (isDead) { return; }
        if (!gameManager.RedEnabled) { return; }
        if (value.isPressed)
        {
            myRigibody.linearVelocity = Vector2.zero;
            myRigibody.AddForce(Vector2.right * dashForce * transform.localScale.x, ForceMode2D.Impulse);
            isDashing = true;
            gnomeGFX.transform.rotation = Quaternion.Euler(0,0,-30*transform.localScale.x);
            StartCoroutine(ZeroGravity());
        }
    }

    void OnBluePower(InputValue value)
    {
        if (isDead) { return; }
        if (!gameManager.BlueEnabled) return;
        if (value.isPressed)
        {
            GameObject freezeProjectile = Instantiate(freezeProjectilePrefab, shootPos.position, Quaternion.identity);
            freezeProjectile.GetComponent<BluePower>().SlowPercentage = slowPercentage;
            Vector2 throwDirection = new(transform.localScale.x * projectileSpeed, projectileUpForce);
            Rigidbody2D projectileRB = freezeProjectile.GetComponent<Rigidbody2D>();
            projectileRB.AddForce(throwDirection, ForceMode2D.Impulse);
            projectileRB.angularVelocity = Random.Range(1f, 50f);
        }
    }

    void OnYellowPower(InputValue value)
    {
        if (isDead) { return; }
        if (!gameManager.YellowEnabled) return;
        if (value.isPressed)
        {
            GameObject sunProjectile = Instantiate(sunProjectilePrefab, shootPos.position, Quaternion.identity);
            sunProjectile.GetComponent<YellowPower>().TimeToTorns = timeToTorns;
            Vector2 throwDirection = new(transform.localScale.x * projectileSpeed, projectileUpForce);
            Rigidbody2D projectileRB = sunProjectile.GetComponent<Rigidbody2D>();
            projectileRB.AddForce(throwDirection, ForceMode2D.Impulse);
            projectileRB.angularVelocity = Random.Range(1f, 50f);

        }
    }

    private void FixedUpdate()
    {
        if (isDead) { return; }
        if (Mathf.Abs(moveInput) > 0 && !isDashing)
        {
            transform.localScale = new Vector3 (Mathf.Sign(moveInput),1,1);
            myRigibody.linearVelocity = new Vector2(moveInput * speed, myRigibody.linearVelocityY);
        }
        
    }

    IEnumerator ZeroGravity()
    {
        myRigibody.gravityScale = 0;
        yield return new WaitForSeconds(dashGravityTimer);
        myRigibody.linearVelocity = Vector2.zero;
        myRigibody.gravityScale = normalGravityScale;
        isDashing = false;
        gnomeGFX.transform.rotation = Quaternion.Euler(0,0,0);
    }

    public void Die()
    {
        myRigibody.Sleep();
        isDead = true;
    }

    public void ReTry(Transform checkpoint)
    {
        moveInput = 0f;
        transform.position = checkpoint.position;
        myRigibody.linearVelocity = Vector3.zero;
        myRigibody.angularVelocity = 0f;
        myRigibody.WakeUp();
        isDead = false;
    }
}
