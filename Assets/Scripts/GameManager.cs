using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class GameManager : BaseServerRpc
{
    [SerializeField] private Button demoButton; // Assign this via the Inspector
    private NetworkVariable<Color> buttonColor = new NetworkVariable<Color>(Color.white); // Shared button color

    private void Start()
    {
        // Set up the button to call the OnButtonPressed method when clicked
        demoButton.onClick.AddListener(OnButtonPressed);

        // Synchronize button color for all clients
        buttonColor.OnValueChanged += OnColorChanged;

        // Initialize button color for the local client
        UpdateButtonColor(buttonColor.Value);
    }

    public override void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        buttonColor.OnValueChanged -= OnColorChanged;
    }

    // Called when the button is pressed
    private void OnButtonPressed()
    {
        if (IsHost) // Ensure only the host can change the color
        {
            ChangeColorServerRpc(); // Call server RPC to change the color
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void ChangeColorServerRpc()
    {
        // Assign a new random color to the NetworkVariable
        buttonColor.Value = new Color(Random.value, Random.value, Random.value);
    }

    // Callback for when the color changes
    private void OnColorChanged(Color oldColor, Color newColor)
    {
        UpdateButtonColor(newColor); // Update the button's color locally
    }

    // Updates the button's visual color
    private void UpdateButtonColor(Color newColor)
    {
        ColorBlock colorBlock = demoButton.colors;
        colorBlock.normalColor = newColor;
        demoButton.colors = colorBlock;
    }
}
