using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private UIManager uIManager;
    [SerializeField] private DifficultyManager difficultyManager;
    [SerializeField] private AudioManager audioManager;

    [Header("Spawning Items")]
    [SerializeField] private List<GameObject> items;
    [SerializeField] private Transform leftSpawnPoint, rightSpawnPoint;
    [SerializeField] private Transform chestPosition;
    [SerializeField] private AnimationCurve fallingTrajectory;

    [SerializeField] private SpriteRenderer dragonSpriteRenderer;
    [SerializeField] private Sprite flexDragon;

    [SerializeField] private GameObject finalScreen;
    [SerializeField] private GameObject mainUi;


    private float spentGold = 0f;

    private int itemsBought = 0;
    private float score;
    public float Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            if (score < 0)
            {
                score = 0;
            }
            uIManager.SetHighscoreText((int)Score);
        }
    }

    private void Start()
    {
        audioManager.PlayClip("Main");
    }

    public void AddToSpentGold(float itemPrice)
    {
        spentGold += itemPrice;
    }

    //Возвращает общую сумму денег потраченную на предметы + текущее количество денег
    public float SumGold() => Score + spentGold;

    public IEnumerator FallingObjectSpawnCoroutine()
    {
        while (true)
        {
            Vector2 spawnPosition = GenerateSpawnPosition();
            GameObject randomObject = GenerateFallingItem();
            GameObject createdItem = Instantiate(randomObject, spawnPosition, Quaternion.identity);

            createdItem.GetComponent<FallingItem>().FlyingGoal = chestPosition.position;
            createdItem.GetComponent<FallingItem>().FallingTrajectory = fallingTrajectory;

            float randomFlyingSpeed = difficultyManager.GenerateFlyingSpeed();
            createdItem.GetComponent<FallingItem>().Duration = randomFlyingSpeed;

            StartCoroutine(createdItem.GetComponent<FallingItem>().CurveMovementCoroutine());
            yield return new WaitForSeconds(difficultyManager.CurrentWaitTimeBetweenSpawns);
        }
    }

    private Vector2 GenerateSpawnPosition()
    {
        float distanceBetweenSpawnPoints = rightSpawnPoint.position.x - leftSpawnPoint.position.x;
        float randomPositionX = leftSpawnPoint.position.x + Random.Range(0, distanceBetweenSpawnPoints);

        Vector2 spawnPosition = new Vector2(randomPositionX, rightSpawnPoint.position.y);
        return spawnPosition;
    }

    private GameObject GenerateFallingItem()
    {
        int randomNumber = Random.Range(0, items.Count);
        return items[randomNumber];
    }

    public void AddToItemsBought()
    {
        itemsBought++;
        if (itemsBought == 5)
        {
            //dragonSpriteRenderer.sprite = flexDragon;

            //Показать победный экран
            finalScreen.SetActive(true);
            mainUi.SetActive(false);
            Time.timeScale = 0;
        }
    }
}
