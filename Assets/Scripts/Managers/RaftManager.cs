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

        public void MoveRaft(Vector2 direction, float speed = 2f)
        {
            foreach (GameObject e in _raftObjects)
                e.transform.Translate(speed * direction.normalized * Time.fixedDeltaTime);
        }

        public void AddRaftObject(GameObject obj)
        {
            _raftObjects.Add(obj);
        }

        public void RemoveRaftObject(GameObject obj)
        {
            _raftObjects.Remove(obj);
        }
    }
}