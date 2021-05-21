using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// public class GameManager : MonoBehaviour
// {
//     public static GameManager Instance { get; private set; }
//     public GameState gameState;

//     // Start is called before the first frame update
//     void Start()
//     {
//         Instance = this;
//         DontDestroyOnLoad(this);
//         SetState(new Title(this));
//     }

//     // void FixedUpdate()
//     // {
//     //     gameState.DoStateBehaviourFixedUpdate();
//     // }

//     // void Update()
//     // {
//     //     gameState.DoStateBehaviour();
//     //     gameState.Transitions();
//     // }

//     public void SetState(GameState state)
//     {
//         if (gameState != null)
//             gameState.ExitState();

//         gameState = state;
//         gameState.EnterState();
//     }

//     // Use SceneManager.LoadSceneAsync() to accomodate loading screens
//     // public IEnumerator LoadScene()
//     // {
//     //     AsyncOperation asyncload = SceneManager.LoadSceneAsync(1);
//     //     while (!asyncload.isDone)
//     //     {
//     //         yield return null;
//     //     }
//     // }

//     // Test buttons to transition between scenes
//     public bool btnMenu()
//     {
//         if (GUILayout.Button("Menu"))
//         {
//             return true;
//             Debug.Log("Load Menu scene");
//         }
//         return false;
//     }

//     public bool btnGame()
//     {
//         if (GUILayout.Button("Game"))
//         {
//             return true;
//             Debug.Log("Load Game scene");
//         }
//         return false;
//     }

//     public bool btnWin()
//     {
//         if (GUILayout.Button("Win"))
//         {
//             return true;
//             Debug.Log("Load Win scene");
//         }
//         return false;
//     }

//     public bool btnLose()
//     {
//         if (GUILayout.Button("Lose"))
//         {
//             return true;
//             Debug.Log("Load Lose scene");
//         }
//         return false;
//     }
// }