using UnityEngine;


public class GameManager : MonoBehaviour
{
    static GameManager instance = null;

    private bool redEnabled = false;
    private bool blueEnabled = false;
    private bool yellowEnabled = false;

    public bool RedEnabled { get => redEnabled; private set => redEnabled = value; }
    public bool BlueEnabled { get => blueEnabled; private set => blueEnabled = value; }
    public bool YellowEnabled { get => yellowEnabled; private set => yellowEnabled = value; }

    private void Awake()
    {
        GameManager[] instances = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if (instances.Length > 1) { Destroy(this.gameObject); }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void EnableRed(bool value)
    {
        redEnabled = value;
    }

    public void EnableBlue(bool value)
    {
        blueEnabled = value;
    }

    public void EnableYellow(bool value)
    {
        yellowEnabled = value;
    }
}
