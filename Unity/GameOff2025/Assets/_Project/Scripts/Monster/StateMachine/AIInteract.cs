using System;
using SubHorror.Interaction;
using UnityEngine;

namespace SubHorror.Monster
{
    public class AIInteract : MonoBehaviour
    {
        private Interactor interactor;

        private void Awake()
        {
            interactor = GetComponentInChildren<Interactor>();
        }

        private void OnEnable()
        {
            interactor.OnEnterInteractable += TryInteract;
        }

        private void OnDisable()
        {
            interactor.OnEnterInteractable -= TryInteract;
        }

        private void TryInteract()
        {
            interactor.Interact();
        }
    }
}