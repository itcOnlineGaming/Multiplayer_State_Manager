using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using static log4net.Appender.ColoredConsoleAppender;

public class ColourChangeHandler : BaseServerRpc
{
    [SerializeField] private Button buttonToChange;
    private Color[] colours = { Color.red, Color.green, Color.blue };
    private int colourIndex = 0;

    private void Start()
    {
        // Ensure all players see color changes
        if (buttonToChange != null)
        {
            buttonToChange.onClick.AddListener(OnButtonClicked);
        }
    }

    public override void OnDestroy()
    {
        if (buttonToChange != null)
        {
            buttonToChange.onClick.RemoveListener(OnButtonClicked);
        }
    }

    public void OnButtonClicked()
    {
        if (IsServer)
        {
            ChangeColorServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void ChangeColorServerRpc()
    {
        // Increment color index and broadcast to clients
        colourIndex = (colourIndex + 1) % colours.Length;
        ChangeColorClientRpc(colourIndex);
    }

    [ClientRpc]
    private void ChangeColorClientRpc(int newColorIndex)
    {
        if (buttonToChange != null)
        {
            buttonToChange.GetComponent<Image>().color = colours[newColorIndex];
        }
    }
}
