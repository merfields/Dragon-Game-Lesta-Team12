using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private GameManager gameManager;

    [Header("Spawning Objects")]
    [SerializeField] private float startingWaitTimeBetweenSpawns = 2.0f;
    [SerializeField] private float startingObjectFlyingDuration = 1.5f;
    [SerializeField] private float minFlyingDuration = 1.0f;
    [SerializeField] private float maxFlyingDuration = 2.0f;
    [SerializeField] private int startingNumberOfSpawningObjects = 2;

    [SerializeField] private int[] difficultyCheckpoints;

    private float currentWaitTimeBetweenSpawns;
    public float CurrentWaitTimeBetweenSpawns
    {
        get
        {
            return currentWaitTimeBetweenSpawns;
        }

        private set
        {
            currentWaitTimeBetweenSpawns = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWaitTimeBetweenSpawns = startingWaitTimeBetweenSpawns;
        for (int i = 0; i < startingNumberOfSpawningObjects; i++)
        {
            StartCoroutine(gameManager.FallingObjectSpawnCoroutine());
        }

        StartCoroutine(DifficultyChangeCoroutine());
    }

    /*
        При увеличении сложности увеличить количество объектов от 1 до ??
        За сложность отвечает скрытый параметр, представляющий из себя сумму купленных предметов + счет от 1 до 5
        Duration от 1 до 2
    */

    public float GenerateFlyingSpeed()
    {
        float randomNumber = Random.Range(minFlyingDuration, maxFlyingDuration);
        return randomNumber;
    }

    public IEnumerator DifficultyChangeCoroutine()
    {
        for (int i = 0; i < difficultyCheckpoints.Length; i++)
        {
            while (gameManager.SumGold() < difficultyCheckpoints[i])
            {
                yield return null;
            }
            StartCoroutine(gameManager.FallingObjectSpawnCoroutine());
        }

    }
    //При достижении определенного числа SumGold добавлять новый спаун с помощью запуска доп курутины/уменьшать кулдаун между выстрелами

}
