using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleShopItem : ShopItem
{
    [Range(0, 100)]
    [SerializeField] private float providedHeat;

    override public void OnShopItemButtonClicked()
    {
        if (CheckForMoney())
        {
            audioManager.PlayClip("Purchase");
            heatController.AddHeat(providedHeat);
            gameManager.Score -= itemPrice;
            gameManager.AddToSpentGold(itemPrice);
        }
    }
}
