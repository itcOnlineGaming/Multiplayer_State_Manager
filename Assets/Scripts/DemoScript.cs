using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class DemoScript : MonoBehaviour
{
    private RpcHandlerSystem _rpcHandlerSystem;

    [SerializeField]
    public Button changeColourButton;

    [SerializeField]
    public GameObject playerPrefab;

    public void Awake()
    {
        // Initialize the handler system
        _rpcHandlerSystem = gameObject.AddComponent<RpcHandlerSystem>();

        // Register the command to change color
        Debug.Log("Registering ChangeColour handler...");
        _rpcHandlerSystem.RegisterHandler("ChangeColour", (value, extraInt, outInt) =>
        {
            Debug.Log($"Handler Invoked with Colour: {value}");
            Color newColour = ParseColour(value);
            ApplyColourToPlayers(newColour);
        });
    }

    public void Start()
    {
        // Add listener to the button
        changeColourButton.onClick.AddListener(OnChangeColourButtonPressed);
    }

    public void OnChangeColourButtonPressed()
    {
        // Generate a random colour and send it as a command
        Color demoColour = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        string colorString = ColourToString(demoColour);

        _rpcHandlerSystem.SendServerRpc("ChangeColour", colorString, 0);
    }

    public void ApplyColourToPlayers(Color colour)
    {
        var players = FindObjectsOfType<DemoPlayerScript>();
        Debug.Log($"Number of players detected: {players.Length}");
        foreach (var player in players)
        {
            Debug.Log($"Player found: {player.name}");
            player.SetColour(colour);
        }
    }

    public Color ParseColour(string colourString)
    {
        var values = colourString.Split(',');
        return new Color(
            float.Parse(values[0]),
            float.Parse(values[1]),
            float.Parse(values[2])
        );
    }

    public string ColourToString(Color colour)
    {
        return $"{colour.r},{colour.g},{colour.b}";
    }
}
