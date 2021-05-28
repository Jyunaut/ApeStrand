using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class RaftManager : MonoBehaviour
    {
        public static RaftManager Instance { get; private set; }

        public List<GameObject> RaftObjects = new List<GameObject>();

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
            RaftObjects.Add(obj);
        }

        public void RemoveRaftObject(GameObject obj)
        {
            RaftObjects.Remove(obj);
        }
    }
}