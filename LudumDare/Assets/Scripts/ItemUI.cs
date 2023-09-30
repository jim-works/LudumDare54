using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ItemUI : MonoBehaviour
{
    public Inventory Displaying;
    private Image image;
    private Item displayedItem;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update() {
        if (Displaying == null) return;
        if (Displaying.Item != displayedItem) {
            displayedItem = Displaying.Item;
            updateDisplay(displayedItem);
        }
    }

    void updateDisplay(Item item) {
        if (item.Icon == null) {
            image.enabled = false;
        } else {
            image.sprite = item.Icon;
            image.enabled = true;
        }
    }
}
