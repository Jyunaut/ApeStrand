using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class RaftManager : MonoBehaviour
    {
        public static RaftManager Instance { get; private set; }

        [field: SerializeField] public List<GameObject> RaftObjects { get; set; } = new List<GameObject>();

        void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        public static void AddRaftObject(GameObject obj)
        {
            Instance.RaftObjects.Add(obj);
        }

        public static void RemoveRaftObject(GameObject obj)
        {
            Instance.RaftObjects.Remove(obj);
        }
    }
}