using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using PoolManager;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("Active Objects UI Components")]
    [SerializeField]
    private Text activeObjectsText;
    [SerializeField]
    private InputField activeObjectsInputField;
    [SerializeField]
    private Button activeObjectsSubmitButton;
    
    [Header("Score UI Components")]
    [SerializeField]
    private Text scoreText;

    private void Start()
    {
        EventsFactory.instance.OnActiveObjectsCountChanged.AddListener(UpdateActiveCubesText);
        EventsFactory.instance.OnScoreChanged.AddListener(UpdateScoreText);
        EventsFactory.instance.OnCubeDestroyed.AddListener(OnCubeDestroyed);
    }

    public void SubmitButtonClick()
    {
        int activeObjectsCount = Int32.Parse(activeObjectsInputField.text);
        EventsFactory.instance.OnActiveObjectsCountChanged.Invoke(activeObjectsCount);
    }
    
    private void UpdateActiveCubesText(int count)
    {
        activeObjectsText.text = String.Format("Active Objects: {0}", count);
    }
    
    private void UpdateScoreText(int score)
    {
        scoreText.text = String.Format("Score: {0}", score);
    }
    
    private void OnCubeDestroyed(Cube cube)
    {
        UpdateActiveCubesText(CubesPoolManager.instance.activeObjects.Count);
    }

    private void OnDestroy()
    {
        EventsFactory.instance.OnActiveObjectsCountChanged.RemoveListener(UpdateActiveCubesText);
        EventsFactory.instance.OnScoreChanged.RemoveListener(UpdateScoreText);
        EventsFactory.instance.OnCubeDestroyed.RemoveListener(OnCubeDestroyed);
    }
}
