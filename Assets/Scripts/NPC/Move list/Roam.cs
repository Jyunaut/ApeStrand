using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class Roam : Action
    {
        public float MinTime;
        public float MaxTime;
        public Transform[] targets;
        private Coroutine Coroutine;

        public Roam(Controller controller) : base(controller) 
        {
            Controller = controller;
        }

        public override void OnEnter()
        {
            Controller.StartCoroutine(Wait(Random.Range(MinTime, MaxTime)));
        }

        IEnumerator Wait(float WaitTime)
        {
            Debug.Log($"Waiting for {WaitTime}");
            yield return new WaitForSeconds(WaitTime);
            Controller.TriggerActionComplete();
        }
    }
}