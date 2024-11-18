using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Networking
{
    // The StateManager class manages different game states and transitions between them
    public class StateManager
    {
        // Dictionary to store states by their name for quick lookup
        private Dictionary<string, GameState> states = new Dictionary<string, GameState>();

        // The currently active state
        private GameState currentState;

        // Registers a new state into the manager ensuring no duplicates
        public void RegisterState(GameState state)
        {
            // Add the state to the dictionary if it doesnt already exist
            if (!states.ContainsKey(state.StateName))
            {
                states.Add(state.StateName, state);
            }
        }

        // Transitions to a new state by name
        public void ChangeState(string newState)
        {
            // Exit the current state if one is active
            if (currentState != null)
            {
                currentState.Exit();
            }

            // Attempt to find the new state in the dictionary and activate it
            if (states.TryGetValue(newState, out GameState state))
            {
                currentState = state;
                currentState.Enter();
            }
        }

        // Returns the name of the current active state
        public string GetCurrentState()
        {
            if (currentState != null)
            {
                return currentState.StateName;
            }
            else
            {
                return null;  // Use null conditional operator for safety
            } 
        }
    }
}
