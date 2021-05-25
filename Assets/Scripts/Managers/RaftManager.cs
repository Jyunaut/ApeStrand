using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class RaftManager : MonoBehaviour
    {
        public static RaftManager Instance { get; private set; }

        [SerializeField]
        private List<GameObject> _raftObjects = new List<GameObject>();

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void AddRaftObject(GameObject obj)
        {
            _raftObjects.Add(obj);
        }

        public void RemoveRaftObject(GameObject obj)
        {
            _raftObjects.Remove(obj);
        }

        [Range(0.05f,0.25f)] public float paddleDistance = 0.1f;
        [Range(3f,6f)] public float paddleSpeed = 5f;
        public float paddleTime = 0f;
        public void MoveRaft(Vector2 dir)
        {
            // Paddle distance over time based on sine wave
            Vector2 e1 = paddleDistance * Mathf.Sin(paddleSpeed * paddleTime) * dir;
            if (Mathf.Sin(paddleSpeed * paddleTime) < 0)
            {
                paddleTime = 0f;
                return;
            }
            paddleTime += Time.fixedDeltaTime;
            foreach (GameObject e in _raftObjects)
                e.transform.Translate(e1);
        }
    }
}