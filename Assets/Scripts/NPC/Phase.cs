using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class Phase : Action
    {
        public List<Action> Actions;

        public Phase(Controller controller) : base(controller) 
        {
            Controller = controller;
        }
    }
}
