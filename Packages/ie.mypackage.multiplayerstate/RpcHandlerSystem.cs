using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace NetworkPackage
{
    /// <summary>
    /// Abstract base class for handling server RPC calls.
    /// </summary>
    public class BaseServerRpc : NetworkBehaviour
    {
        // Instance of the handler system
        protected RpcHandlerSystem _rpcHandlerSystem;

        /// <summary>
        /// Initializes the RpcHandlerSystem.
        /// </summary>
        protected virtual void Awake()
        {
            _rpcHandlerSystem = new RpcHandlerSystem();
        }

        /// <summary>
        /// Server RPC method to handle incoming commands.
        /// </summary>
        /// <param name="valueType">The type of command (case type).</param>
        /// <param name="value">String parameter for the command.</param>
        /// <param name="extraInt">First integer parameter for the command.</param>
        [ServerRpc(RequireOwnership = false)]
        public void SendServerRpc(string valueType, string value, int extraInt)
        {
            int outInt = 0;

            // Attempt to parse the string value into an integer
            int.TryParse(value, out outInt);

            // Delegate handling to the RpcHandlerSystem
            _rpcHandlerSystem.Handle(valueType, value, extraInt, outInt);
        }
    }
}