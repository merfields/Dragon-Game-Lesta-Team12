using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeatController : MonoBehaviour
{

    [Header("Heat System")]
    [Range(0, 100)]
    [SerializeField] private float heat = 100f;
    [SerializeField] private float heatDecreaseRate = 1.0f;
    [SerializeField] private Slider heatSlider;
    [SerializeField] private float itemBuffMultiplier = 0.1f;
    private float heatDecreaseBuffDelta;

    private void Start()
    {
        heatDecreaseBuffDelta = heatDecreaseRate * itemBuffMultiplier;
    }

    private void Update()
    {
        DecreaseHeat();
    }

    public void AddHeat(float heat)
    {
        this.heat += heat;
        if (this.heat > 100)
        {
            this.heat = 100;
        }
    }

    private void DecreaseHeat()
    {
        heat -= heatDecreaseRate * Time.deltaTime;
        heatSlider.value = heat;

        if (heat <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ChangeDecreaseRate()
    {
        heatDecreaseRate = heatDecreaseRate - heatDecreaseBuffDelta;
    }
}
