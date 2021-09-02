using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class Action : MonoBehaviour
    {
        public Controller Controller { get; set; }
        public Action(Controller controller) { Controller = controller; }
        public virtual void SetAction() {}
        public virtual void OnEnter() {}
        public virtual void OnUpdate() {}
        public virtual void OnFixedUpdate() {}
        public virtual void OnExit() {}
    }
}
