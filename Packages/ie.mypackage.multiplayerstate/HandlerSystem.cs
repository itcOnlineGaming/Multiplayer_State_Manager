using System;
using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine;

namespace ie.mypackage.multiplayerstate
{
    /// <summary>
    /// Core system for managing and invoking handlers dynamically.
    /// </summary>
    public class RpcHandlerSystem
    {
        // Dictionary to map case types to their respective handler functions
        private readonly Dictionary<string, Action<string, int, int>> _handlers = new();

        /// <summary>
        /// Registers a handler for a specific case type.
        /// </summary>
        /// <param name="caseType">The case type to handle (e.g., "PlayerMove").</param>
        /// <param name="handler">The function to execute for this case type.</param>
        public void RegisterHandler(string caseType, Action<string, int, int> handler)
        {
            if (!_handlers.ContainsKey(caseType))
            {
                _handlers.Add(caseType, handler);
            }
            else
            {
                Debug.LogWarning($"Handler for '{caseType}' is already registered.");
            }
        }

        /// <summary>
        /// Executes the handler for a given case type if one exists.
        /// </summary>
        /// <param name="caseType">The case type to handle.</param>
        /// <param name="value">The string parameter passed to the handler.</param>
        /// <param name="extraInt">The first integer parameter passed to the handler.</param>
        /// <param name="outInt">The second integer parameter passed to the handler.</param>
        public void Handle(string caseType, string value, int extraInt, int outInt)
        {
            if (_handlers.TryGetValue(caseType, out var handler))
            {
                // Invoke the handler function with the provided parameters
                handler.Invoke(value, extraInt, outInt);
            }
            else
            {
                Debug.LogWarning($"No handler found for '{caseType}'.");
            }
        }
    }
}
