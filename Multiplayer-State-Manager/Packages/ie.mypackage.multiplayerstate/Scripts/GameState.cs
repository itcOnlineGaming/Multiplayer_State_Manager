using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Networking
{
    // Represents a single game state with optional enter and exit actions.
    public class GameState
    {
        // The name of the state
        public string StateName { get; private set; }

        // Action to execute when entering the state
        public Action OnEnter;

        // Action to execute when exiting the state
        public Action OnExit;

        // Constructor to initialize the state with a name
        public GameState(string stateName)
        {
            StateName = stateName;
        }

        // Method to trigger the OnEnter action when the state is entered
        public void Enter()
        {
            if (OnEnter != null) // Check if any action is registered
            {
                OnEnter.Invoke(); // Execute the action
            }
        }

        // Method to trigger the OnExit action when the state is exited
        public void Exit()
        {
            if (OnExit != null) // Check if any action is registered
            {
                OnExit.Invoke(); // Execute the action
            }
        }
    }
}

