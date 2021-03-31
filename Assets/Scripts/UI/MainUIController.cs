using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    public Button prefabButtonLevel;
    public GameObject ContentScrollView;
    private SaveSerial saveSerial;

    [Header("Настройка звука")]
    public AudioListener audioListener;
    public Image soundButton;
    public Sprite soundOn, soundOff;

    private int widthCamera,
                widthButton,
                widthArrow,
                countLevels;

    void Start()
    {
        if (PlayerPrefs.GetString("Sound", "on") == "on")
        {
            audioListener.enabled = true;
            soundButton.sprite = soundOn;
        }
        else
        {
            audioListener.enabled = false;
            soundButton.sprite = soundOff;
        }

        saveSerial = GetComponent<SaveSerial>();

        widthCamera = Camera.main.pixelWidth;
        widthButton = widthCamera / 5;
        widthArrow = widthButton / 2;

        countLevels = saveSerial.levels.levels.Count;

        int countStr = countLevels / 5;

        ContentScrollView.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (countStr * widthButton) + widthArrow);

        SpavnButton();


    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    private void SpavnButton()
    {
        int countStr = countLevels / 5,
            numberlevel = 0,
            str;

        for (int i = 0; i < countStr; i++)
        {
            if (i % 2 == 0) str = 1;
            else str = -1;

            for (int x = -2; x <= 2; x++)
            {
                var button = Instantiate(prefabButtonLevel, Vector3.zero, Quaternion.identity);
                button.transform.SetParent(ContentScrollView.transform);
                button.name = (numberlevel + 1).ToString();

                Text text = button.GetComponentInChildren<Text>();
                text.text = (numberlevel + 1).ToString();
                text.resizeTextMaxSize = widthArrow;

                Image imageButton = button.transform.Find("Image").GetComponent<Image>();
                imageButton.rectTransform.sizeDelta = new Vector2(widthArrow, widthArrow);


                if (x == 2 && button.name != countLevels.ToString())
                {
                    var rotationImage = imageButton.rectTransform.localRotation;
                    rotationImage.z = 1;
                    imageButton.rectTransform.localRotation = rotationImage;

                    imageButton.rectTransform.anchoredPosition = new Vector3(0, (widthArrow / 2) + widthArrow, 0);

                }
                else if (button.name == countLevels.ToString())
                {
                    imageButton.gameObject.SetActive(false);
                }
                else if (str == 1)
                {
                    imageButton.rectTransform.anchoredPosition = new Vector3((widthArrow / 2) + widthArrow, 0, 0);
                }
                else
                {
                    imageButton.rectTransform.localScale = new Vector2(-imageButton.rectTransform.localScale.x, imageButton.rectTransform.localScale.y);
                    imageButton.rectTransform.anchoredPosition = new Vector3(-((widthArrow / 2) + widthArrow), 0, 0);
                }

                button.GetComponent<RectTransform>().sizeDelta = new Vector2(widthButton, widthButton);
                button.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
                button.transform.localPosition = new Vector2(x * str * widthButton, (i * widthButton) + widthArrow);

                
                if (PlayerPrefs.GetInt(button.name) > 0 || PlayerPrefs.GetInt(numberlevel.ToString()) > 0)
                {
                    Image imageLock = button.transform.Find("ImageLock").GetComponent<Image>();
                    imageLock.gameObject.SetActive(false);
                }
                if (numberlevel == 0)
                {
                    Image imageLock = button.transform.Find("ImageLock").GetComponent<Image>();
                    imageLock.gameObject.SetActive(false);
                }

                numberlevel++;
            }
        }
    }

    public void ChangingSound()
    {
        if (PlayerPrefs.GetString("Sound") == "on")
        {
            audioListener.enabled = false;
            PlayerPrefs.SetString("Sound", "off");
            soundButton.sprite = soundOff;
        }
        else
        {
            audioListener.enabled = true;
            PlayerPrefs.SetString("Sound", "on");
            soundButton.sprite = soundOn;
        }
    }
}
