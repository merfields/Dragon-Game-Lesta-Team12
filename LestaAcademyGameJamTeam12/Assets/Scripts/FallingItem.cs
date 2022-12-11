using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingItem : MonoBehaviour
{

    [SerializeField] private string audioClipName;
    private float duration = 1f;
    public float Duration
    {
        get
        {
            return duration;
        }
        set
        {
            duration = value;
        }
    }

    [SerializeField] private GameObject ps_Prefab;

    private AnimationCurve fallingTrajectory;
    public AnimationCurve FallingTrajectory
    {
        get
        {
            return fallingTrajectory;
        }

        set
        {
            fallingTrajectory = value;
        }
    }
    //Коробка может не знать про тип объекта, просто добавлять значение value, которое может быть отрицательным
    [SerializeField] private float value;
    public float Value() => value;
    public string AudioClipName() => audioClipName;

    private Vector2 flyingGoal;
    public Vector2 FlyingGoal
    {
        get
        {
            return flyingGoal;
        }

        set
        {
            flyingGoal = value;
        }
    }

    private float rotationSpeed = 60f;



    private void Awake()
    {
        if (transform.position.x < 0)
        {
            rotationSpeed = -rotationSpeed;
        }
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    public IEnumerator CurveMovementCoroutine()
    {
        float currentTimePassed = 0f;
        Vector2 start = transform.position;

        while (currentTimePassed < duration && this != null)
        {
            currentTimePassed += Time.deltaTime;

            //По факту у нас получается значение от 0 до 1, которое можно использовать для получения позиции в графике AC в данный момент времени
            float linearTime = currentTimePassed / duration;
            float timeToCurve = fallingTrajectory.Evaluate(linearTime);

            float objectPositionX = Mathf.Lerp(0f, flyingGoal.x - start.x, timeToCurve);

            transform.position = Vector2.Lerp(start, flyingGoal, linearTime) + new Vector2(objectPositionX, 0f);
            yield return null;
        }
    }

    public void DestroyItem()
    {
        Instantiate(ps_Prefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
