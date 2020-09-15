using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.EventSystems;

namespace Simulator.Api.Commands
{
    public class AgentPushMessage : IDistributedCommand
    {
        public string Name => "simulator/push_message";
        private SimulatorManager _sim = null;

        SimulatorManager sim => _sim ?? Object.FindObjectOfType<SimulatorManager>();

        public void Execute(JSONNode args)
        {
            var api = ApiManager.Instance;
            Debug.Log($"AgentPushMessage.Execute()");
            sim.AgentManager.BroadcastMessage("PushMessage", args["message"].ToString(), SendMessageOptions.DontRequireReceiver);

            api.SendResult(this);
        }
    }
}