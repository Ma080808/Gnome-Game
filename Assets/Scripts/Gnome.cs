using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gnome : MonoBehaviour
{
    [SerializeField] bool isTraitor = false;

    PlayerInput playerInput;
    [SerializeField] Canvas interactIcon;

    private void Start()
    {
        playerInput = FindFirstObjectByType<PlayerInput>();
        interactIcon.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactIcon.enabled = true;
            playerInput.actions["Interact"].performed += Interacted;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactIcon.enabled = false;
        playerInput.actions["Interact"].performed -= Interacted;
    }

    private void Interacted(InputAction.CallbackContext context)
    {
        playerInput.GetComponent<PlayerController>().Die();
        playerInput.GetComponentInChildren<Animator>().SetTrigger("Kill");
        StartCoroutine(LoadEnding());
    }

    private IEnumerator LoadEnding()
    {
        yield return new WaitForSeconds(1.5f);

        if (isTraitor)
        {
            FindFirstObjectByType<MySceneManager>().ChangeScene("z_GoodEnding");
        }
        else
        {
            FindFirstObjectByType<MySceneManager>().ChangeScene("z_BadEnding");
        }
    }
}
