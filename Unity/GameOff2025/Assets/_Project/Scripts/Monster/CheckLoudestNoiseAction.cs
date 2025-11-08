using System;
using SubHorror.Noise;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace SubHorror.Monster
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Check Loudest Noise", story: "[Noise] is loudest", category: "Action",
        id: "464f500cc43ec3179e6d05650b3ab774")]
    public partial class CheckLoudestNoiseAction : Action
    {
        [SerializeReference] public BlackboardVariable<GameObject> Noise;
        [SerializeReference] public BlackboardVariable<bool> HasTarget;

        protected override Status OnStart()
        {
            NoiseEmitter emitter = NoiseEmitter.GetLoudestNoiseEmitter();

            Noise.Value = emitter.gameObject;
            HasTarget.Value = emitter;

            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            return Status.Success;
        }

        protected override void OnEnd()
        {
        }
    }
}