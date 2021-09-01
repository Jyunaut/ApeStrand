using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NPC
{
    public class Controller : MonoBehaviour
    {
        public List<Phase> Phases;

        [field: SerializeField] public Phase CurrentPhase       { get; set; }
        [field: SerializeField] public Action CurrentAction     { get; set; }
        [field: SerializeField] public int PhasesTraversed      { get; set; }
        [field: SerializeField] public int ActionsTraversed     { get; set; }

        private void Awake()
        {
            foreach (Phase phase in Phases)
                foreach (Action action in phase.Actions)
                    action.Controller = this;
            ActionsTraversed = PhasesTraversed = 0;
        }

        private void Start()
        {
            try
            {
                SetPhase(Phases[PhasesTraversed]);
                SetAction(Phases[PhasesTraversed].Actions[ActionsTraversed]);
            }
            catch(Exception e)
            {
                Debug.Log($"Missing NPC phases/actions {e}");
            }
        }

        private void Update()
        {
            CurrentAction?.OnUpdate();
            CurrentPhase?.OnUpdate();
        }

        private void FixedUpdate()
        {
            CurrentAction?.OnFixedUpdate();
            CurrentPhase?.OnFixedUpdate();
        }

        public void TriggerActionComplete()
        {
            if(++ActionsTraversed < CurrentPhase.Actions.Count)
            {
                SetAction(CurrentPhase.Actions[ActionsTraversed]);
            }
            else
            {
                TriggerPhaseComplete();
            }
        }

        private void TriggerPhaseComplete()
        {
            if(++PhasesTraversed < Phases.Count)
            {
                SetPhase(Phases[PhasesTraversed]);
                SetAction(CurrentPhase.Actions[ActionsTraversed = 0]);
            }
            else
            {
                SetPhase(Phases[PhasesTraversed = 0]);
                SetAction(CurrentPhase.Actions[ActionsTraversed = 0]);
            }
        }

        private void SetAction(Action action)
        {
            Debug.Log($"Traversed {ActionsTraversed}");
            CurrentAction?.OnExit();
            CurrentAction = action;
            CurrentAction?.OnEnter();
        }

        private void SetPhase(Phase phase)
        {
            CurrentPhase?.OnExit();
            CurrentPhase = phase;
            CurrentPhase?.OnEnter();
        }
    }   
}
