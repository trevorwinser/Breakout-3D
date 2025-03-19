using UnityEngine;

public class ButtonHooks : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneHandler.Instance.LoadNextScene();
    }

    public void ExitToMenu()
    {
        SceneHandler.Instance.LoadMenuScene();
    }
    public void LoadSpecificScene(string sceneName)
    {
        SceneHandler.Instance.LoadSpecificScene(sceneName);
    }
}
