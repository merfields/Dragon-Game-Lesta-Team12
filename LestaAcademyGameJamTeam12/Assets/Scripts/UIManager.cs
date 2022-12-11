using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] TextMeshProUGUI highscoreText;
    [SerializeField] Button shopButton;
    [SerializeField] GameObject shopMenu;
    [SerializeField] private GameObject mainUi;
    [SerializeField] private GameObject finalScreen;

    [SerializeField] private string defaultText;
    [SerializeField] private Slider heatSlider;

    [SerializeField] private float shopIndicatorMaxWidth = 1.3f;
    [SerializeField] private float shopIndicatorBlinkingDuration = 1.0f;




    // Start is called before the first frame update
    void Start()
    {
        defaultText = highscoreText.text;
    }

    public void OnShopButtonClicked()
    {
        shopMenu.SetActive(true);
        shopButton.gameObject.SetActive(false);
        Camera.main.GetComponent<ClickToDestroy>().CanMouseClick(false);
        Time.timeScale = 0;
    }

    public void OnContinueGameButtonClicked()
    {
        shopMenu.SetActive(false);
        shopButton.gameObject.SetActive(true);
        Camera.main.GetComponent<ClickToDestroy>().CanMouseClick(true);
        Time.timeScale = 1;
    }

    public void SetHighscoreText(int value) => highscoreText.text = defaultText + value;

    public IEnumerator LowHeatIndicatorCoroutine()
    {
        while (true)
        {
            float timePassed = 0;
            while (timePassed < shopIndicatorBlinkingDuration)
            {
                timePassed += Time.deltaTime;
                float linearTime = timePassed / shopIndicatorBlinkingDuration;
                float currentScaleValue = Mathf.Lerp(1, shopIndicatorMaxWidth, linearTime);
                Vector3 newScale = new Vector3(currentScaleValue, currentScaleValue, currentScaleValue);
                shopButton.transform.localScale = newScale;
                yield return null;
            }
            timePassed = 0;

            while (timePassed < shopIndicatorBlinkingDuration)
            {
                timePassed += Time.deltaTime;
                float linearTime = timePassed / shopIndicatorBlinkingDuration;
                float currentScaleValue = Mathf.Lerp(shopIndicatorMaxWidth, 1, linearTime);
                Vector3 newScale = new Vector3(currentScaleValue, currentScaleValue, currentScaleValue);
                shopButton.transform.localScale = newScale;
                yield return null;
            }

            yield return null;
        }
    }


    public void ResetShopButton()
    {
        shopButton.transform.localScale = new Vector3(1, 1, 1);
    }

    //Для продолжения после окончания
    public void OnResumeGameButtonClicked()
    {
        Time.timeScale = 1;
        finalScreen.SetActive(false);
        mainUi.SetActive(true);
    }

    public void OnStopPlayingButtonClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
