using DG.Tweening;
using UnityEngine;

public class SceneManager : SingletonMonoBehavior<SceneManager>
{
    [SerializeField] private string nextScene;
    [SerializeField] private Ease easing;

    public void OnSceneTransition()
    {

    }
}
