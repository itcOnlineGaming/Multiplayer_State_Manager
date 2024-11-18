using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace MyGame.Networking
{
    // The NetworkStateManager class is responsible for managing game states in a networked environment.
    // It uses Unity's networking system to synchronize state changes between the server and clients.
    public class NetworkStateManager : NetworkBehaviour
    {
        // Local instance of the StateManager to handle state logic
        private StateManager stateManager = new StateManager();

        // ServerRpc: Called by a client but executed on the server
        [ServerRpc]
        public void ChangeStateServerRpc(string newState)
        {
            // Change the state on the server
            stateManager.ChangeState(newState);

            // Notify all clients about the state change
            ChangeStateClientRpc(newState);
        }

        // ClientRpc: Called by the server to execute on all clients
        [ClientRpc]
        private void ChangeStateClientRpc(string newState)
        {
            // Change the state on the client
            stateManager.ChangeState(newState);
        }

        // Registers a new game state to the StateManager
        public void RegisterState(GameState state)
        {
            stateManager.RegisterState(state);
        }

        // Retrieves the name of the current state
        public string GetCurrentState()
        {
            return stateManager.GetCurrentState();
        }
    }
}
