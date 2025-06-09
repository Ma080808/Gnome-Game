using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    bool hasOpened = false;
    PlayerInput playerInput;
    [SerializeField] Canvas interactIcon;
    [SerializeField] Canvas clueIcon;
    [SerializeField] Sprite openedSprite;

    private void Start()
    {
        playerInput = FindFirstObjectByType<PlayerInput>();
        interactIcon.enabled = false;
        clueIcon.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UpdateIcons();
            playerInput.actions["Interact"].performed += Interacted;
        }
    }

    private void UpdateIcons()
    {
        interactIcon.enabled = !hasOpened;
        clueIcon.enabled = hasOpened;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactIcon.enabled = false;
        clueIcon.enabled = false;
        playerInput.actions["Interact"].performed -= Interacted;
    }

    private void Interacted(InputAction.CallbackContext context)
    {
        hasOpened = true;
        UpdateIcons();
        GetComponentInChildren<SpriteRenderer>().sprite = openedSprite;
    }
}
