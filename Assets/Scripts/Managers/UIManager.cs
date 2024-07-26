using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private UIDocument _uiDocument;
    private VisualElement _pauseButton;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        _uiDocument = GetComponent<UIDocument>();

        _pauseButton = _uiDocument.rootVisualElement.Q("UnpauseButton");
        _pauseButton.Q<Button>().clicked += GameManager.Instance.UnPauseGame;

        GameManager.Instance.OnPauseStateChanged += PauseStateChanged;
    }

    private void PauseStateChanged(bool isPaused)
    {
        if (isPaused)
        {
            _pauseButton.RemoveFromClassList("hidden");
        }
        else
        {
            _pauseButton.AddToClassList("hidden");
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPauseStateChanged -= PauseStateChanged;
        _pauseButton.Q<Button>().clicked -= GameManager.Instance.UnPauseGame;
    }
}
