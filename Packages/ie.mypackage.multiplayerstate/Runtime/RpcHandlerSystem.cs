using System;
using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine;

public class RpcHandlerSystem : NetworkBehaviour
{
    // Dictionary to map state types to their handler functions
    private readonly Dictionary<string, Action<string, int, int>> _handlers = new();

    private void Awake()
    {
        Debug.Log("DynamicServerRpc initialized. Ready to register handlers.");
    }

    /// <summary>
    /// Registers a handler for a specific state type.
    /// </summary>
    /// <param name="stateType">The state type to handle (e.g., "PlayerMove").</param>
    /// <param name="handler">The function to execute for this state type.</param>
    public void RegisterHandler(string stateType, Action<string, int, int> handler)
    {
        if (!_handlers.ContainsKey(stateType))
        {
            _handlers.Add(stateType, handler);
            Debug.Log($"Handler for '{stateType}' registered.");
        }
        else
        {
            Debug.LogWarning($"Handler for '{stateType}' is already registered.");
        }
    }

    /// <summary>
    /// Server RPC to handle incoming commands dynamically.
    /// </summary>
    /// <param name="stateType">State type to handle (e.g., "PlayerMove").</param>
    /// <param name="value">String parameter passed to the handler.</param>
    /// <param name="extraInt">First integer parameter passed to the handler.</param>
    [ServerRpc(RequireOwnership = false)]
    public void SendServerRpc(string stateType, string value, int extraInt)
    {
        int outInt = 0;
        int.TryParse(value, out outInt);

        if (_handlers.TryGetValue(stateType, out var handler))
        {
            handler.Invoke(value, extraInt, outInt);
        }
        else
        {
            Debug.LogWarning($"No handler found for state type '{stateType}'.");
        }
    }
}
