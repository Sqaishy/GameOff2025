using System;
using SubHorror.Noise;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "MonsterMovement",
    description: "Multiplies the monsters movement speed based on the targets noise level",
    story: "Multiply [speed] based on [target] noise",
    category: "Action",
    id: "d2440e868e0fb638f229186a80e0b750")]
public partial class MonsterMovementAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Speed;
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> MinSpeedMultiplier;
    [SerializeReference] public BlackboardVariable<float> MaxSpeedMultiplier;

    private NoiseEmitter playerEmitter;
    private float movementSpeed;

    protected override Status OnStart()
    {
        playerEmitter = Target.Value.GetComponentInChildren<NoiseEmitter>();

        return playerEmitter ? Status.Running : Status.Failure;
    }

    protected override Status OnUpdate()
    {
        float noiseLevel = playerEmitter.TotalNoiseLevelCombined();

        //Lerp the monsters movement speed based on the noise level
        float speedMultiplier = Mathf.Lerp(MinSpeedMultiplier.Value, MaxSpeedMultiplier.Value, noiseLevel / 100f);

        Speed.Value = 2f * speedMultiplier;

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

