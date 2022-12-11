using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //При столкновении с объектом проверять его тип, и взависимости от него добавлять или убирать очки
        if (other.GetComponent<FallingItem>() != null)
        {
            gameManager.Score += other.GetComponent<FallingItem>().Value();
            Destroy(other.gameObject);
        }
    }
}
