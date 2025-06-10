using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaintPots : MonoBehaviour
{
    [SerializeField] bool red, blue, yellow;
    [SerializeField] Canvas interactIcon;

    PlayerInput playerInput;
    GameUIController gameUIController;

    private void Start()
    {
        playerInput = FindFirstObjectByType<PlayerInput>();
        gameUIController = FindFirstObjectByType<GameUIController>();
        interactIcon.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) { 
        Debug.Log("Entered pot");
        if (other.gameObject.CompareTag("Player"))
        {
            interactIcon.enabled = true;
            playerInput.actions["Interact"].performed += Interacted;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Left pot");
        interactIcon.enabled = false;
        playerInput.actions["Interact"].performed -= Interacted;
    }

    private void Interacted(InputAction.CallbackContext context)
    {
        Debug.Log("read the interected");
        GameManager gameManager = FindFirstObjectByType<GameManager>();

        if (red && !gameManager.RedEnabled)
        { 
            gameManager.EnableRed(true); 
            gameUIController.ActivateRed();
        }
        
        if (blue && !gameManager.BlueEnabled)
        {
            gameManager.EnableBlue(true);
            gameUIController.ActivateBlue();
        }

        if (yellow && !gameManager.YellowEnabled)
        { 
            gameManager.EnableYellow(true);
            gameUIController.ActivateYellow();
        }
    }
            
}
