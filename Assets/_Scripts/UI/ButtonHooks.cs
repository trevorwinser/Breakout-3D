using UnityEngine;

public class ButtonHooks : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneHandler.Instance.LoadNextScene();
    }

    public void ExitToMenu()
    {
        SceneHandler.Instance.ExitToMenu();
    }

    public void ExitGame()
    {
        SceneHandler.Instance.ExitGame();
    }
}
