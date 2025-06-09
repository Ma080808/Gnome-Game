using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] Image dashKey, dashIcon, freezeKey, freezeIcon, sunKey, sunIcon;
    GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager.RedEnabled) { ActivateRed(); }
        if (gameManager.BlueEnabled) { ActivateBlue(); }
        if (gameManager.YellowEnabled) { ActivateYellow(); }
    }

    public void ActivateRed()
    {
        dashKey.color = Color.white;
        dashIcon.color = Color.white;
    }

    public void ActivateBlue()
    {
        freezeKey.color = Color.white;
        freezeIcon.color = Color.white;
    }

    public void ActivateYellow()
    {
        sunKey.color = Color.white;
        sunIcon.color = Color.white;
    }
}
