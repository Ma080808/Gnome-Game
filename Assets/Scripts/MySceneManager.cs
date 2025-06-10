using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    Transform lastCheckpoint;


    public Transform LastCheckpoint { get => lastCheckpoint; set => lastCheckpoint = value; }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void TryAgain()
    {
        FindFirstObjectByType<PlayerController>().ReTry(lastCheckpoint);
        GetComponent<Canvas>().enabled = false;
    }
}
