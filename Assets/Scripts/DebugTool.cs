using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class DebugTool : MonoBehaviour
{
    public static DebugTool Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            SceneLoader.LoadScene(SceneLoader.Scene.Title);
        if (Input.GetKeyDown(KeyCode.K))
            SceneLoader.LoadScene(SceneLoader.Scene.Main);
    }
}
