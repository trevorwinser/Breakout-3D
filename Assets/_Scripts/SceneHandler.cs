using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : SingletonMonoBehavior<SceneHandler>
{
    [Header("Scene Data")]
    [SerializeField] private List<string> levels;
    [SerializeField] private string menuScene;
    [Header("Transition Animation Data")]
    [SerializeField] private Ease animationType;
    [SerializeField] private float animationDuration;
    [Header("Temp")]
    [SerializeField] private int nextLevelIndex = 0;
    [SerializeField] private Transform transitionCanvas;

    protected override void Awake()
    {
        base.Awake();
        transitionCanvas = GameObject.FindGameObjectsWithTag("TransitionCanvas")[0].transform;
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode _)
    {
        transitionCanvas = GameObject.FindGameObjectsWithTag("TransitionCanvas")[0].transform;
        transitionCanvas.DOLocalMoveX(-1920f, animationDuration).SetEase(animationType);
    }

    public void LoadNextScene()
    {
        transitionCanvas.DOLocalMoveX(0f, animationDuration).SetEase(animationType);
        StartCoroutine(LoadSceneAfterTransition(levels[nextLevelIndex]));
    }

    public void ExitToMenu()
    {
        transitionCanvas.DOMoveX(0f, animationDuration).SetEase(animationType);
        StartCoroutine(LoadSceneAfterTransition(menuScene));
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private IEnumerator LoadSceneAfterTransition(string scene)
    {
        yield return new WaitForSeconds(animationDuration);
        nextLevelIndex++;
        SceneManager.LoadScene(scene);
    }
}
