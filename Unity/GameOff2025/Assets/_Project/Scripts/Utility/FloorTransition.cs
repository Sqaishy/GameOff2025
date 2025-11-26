using SubHorror.Interaction;
using SubHorror.Monster;
using UnityEngine;
using UnityEngine.AI;

public class FloorTransition : MonoBehaviour, IInteractable
{
	[SerializeField] private InteractorContext interactorContext;
	[SerializeField] private Transform destination;
	[SerializeField] private Transform monsterDestination;
	[SerializeField] private MonsterSpawner monsterSpawner;

	public bool CanInteract()
	{
		return true;
	}

	public void Interact(GameObject interactor, InteractorContext context)
	{
		if (context != interactorContext)
			return;

		TeleportToFloor(interactor);
	}

	public void ResetInteraction()
	{

	}

	private void TeleportToFloor(GameObject interactor)
	{
		interactor.transform.root.position = destination.transform.position;

		if (!monsterSpawner.ActiveMonster)
			return;

		monsterSpawner.ActiveMonster.GetComponent<NavMeshAgent>().Warp(monsterDestination.position);
	}
}