using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LuggageCartUI : MonoBehaviour
{
    public TextMeshProUGUI Text;
    private LuggageCart displayingFor;

    // Update is called once per frame
    void Update()
    {
        if (displayingFor.MyState == LuggageCart.State.Empty)
        {
            Text.text = "Press [E] to ride the luggage cart.";
            if (Input.GetKeyDown(KeyCode.E))
            {
                displayingFor.BoardPassenger(ObjectRegistry.Singleton.Player.gameObject);
            }
        }
        else
        {
            Text.text = "Press [R] to exit the luggage cart.";
            if (Input.GetKeyDown(KeyCode.R))
            {
                displayingFor.RemovePassenger();
            }
        }
    }

    public void DisplayFor(LuggageCart cart)
    {
        if (displayingFor == null) displayingFor = cart;
    }
}
