using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NPC
{
    [RequireComponent(typeof(PathfindingHandler))]
    public class Controller : MonoBehaviour
    {
        [field: SerializeField] public List<Phase> Phases        { get; set; }
        [field: SerializeField] private Phase CurrentPhase       { get; set; }
        [field: SerializeField] private Action CurrentAction     { get; set; }
        [field: SerializeField] private int PhasesTraversed      { get; set; }
        [field: SerializeField] private int ActionsTraversed     { get; set; }

        private void Awake()
        {
            foreach (Phase phase in Phases)
                foreach (Action action in phase.Actions)
                {
                    action.Controller = this;
                    action.SetAction();
                }
                    
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
                Debug.Log($"Missing NPC phases/actions on start {e}");
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

        private void Stunned()
        {
            // Reset phases and actions traversed and play stunned action
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
