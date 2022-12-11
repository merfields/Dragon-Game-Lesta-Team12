using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected DifficultyManager difficultyManager;
    [SerializeField] protected AudioManager audioManager;
    [SerializeField] private TextMeshProUGUI shopDescription;


    [SerializeField] private GameObject correspondingObjectOnScene;
    [SerializeField] protected int itemPrice = 0;
    [SerializeField] protected HeatController heatController;

    private bool isPurchased = false;

    private void Start()
    {
        string price = " " + itemPrice;
        shopDescription.text = shopDescription.text + price;
    }

    public virtual void OnShopItemButtonClicked()
    {
        if (!isPurchased && CheckForMoney())
        {
            //Поменять спрайт в магазе
            GetComponent<Image>().color = Color.black;

            //Активировать предмет
            correspondingObjectOnScene.SetActive(true);

            audioManager.PlayClip("Purchase");
            isPurchased = true;
            
            gameManager.Score -= itemPrice;
            gameManager.AddToSpentGold(itemPrice);
            gameManager.AddToItemsBought();
            heatController.ChangeDecreaseRate();

        }
    }

    protected bool CheckForMoney()
    {
        if (gameManager.Score >= itemPrice)
        {
            return true;
        }
        else return false;
    }
}
