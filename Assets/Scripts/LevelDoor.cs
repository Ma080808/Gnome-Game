using UnityEngine;
using UnityEngine.InputSystem;

public class LevelDoor : MonoBehaviour
{
    [SerializeField] private bool playerHasKey;
    PlayerInput playerInput;
    [SerializeField] Canvas interactIcon;
    [SerializeField] Canvas needKeyIcon;
    [SerializeField] string nextLevelName;

    public bool PlayerHasKey { get => playerHasKey; set => playerHasKey = value; }

    private void Start()
    {
        playerInput = FindFirstObjectByType<PlayerInput>();
        interactIcon.enabled = false;
        needKeyIcon.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerHasKey)
            {
                interactIcon.enabled = true;
                playerInput.actions["Interact"].performed += Interacted;
            }
            else
            {
                needKeyIcon.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactIcon.enabled = false;
        needKeyIcon.enabled = false;
        playerInput.actions["Interact"].performed -= Interacted;
    }

    private void Interacted(InputAction.CallbackContext context)
    {
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        FindFirstObjectByType<MySceneManager>().ChangeScene(nextLevelName);    
    }
}
