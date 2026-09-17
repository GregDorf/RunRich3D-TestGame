using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoneyBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMoney playerMoney;
    [SerializeField] private RectTransform fill;
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private Image fillImage;

    [Header("Rank Colors")]
    [SerializeField] private Color homelessColor = Color.gray;
    [SerializeField] private Color poorColor = Color.yellow;
    [SerializeField] private Color wealthyColor = Color.green;
    [SerializeField] private Color richColor = Color.blue;
    [SerializeField] private Color millionaireColor = Color.magenta;

    [Header("Rank Thresholds")]
    [SerializeField] private int poorThreshold = 20;
    [SerializeField] private int wealthyThreshold = 60;
    [SerializeField] private int richThreshold = 100;
    [SerializeField] private int millionaireThreshold = 300;
    [SerializeField] private int millionaireMaxMoney = 500;

    private float fullWidth;

    private void Start()
    {
        fullWidth = fill.rect.width;

        UpdateBar();
    }

    private void Update()
    {
        UpdateBar();
    }

    private void UpdateBar()
    {
        if (playerMoney == null)
            return;

        int money = playerMoney.Money;

        switch (playerMoney.Rank)
        {
            case PlayerRank.Бездомный:
                SetBar(
                    money,
                    0,
                    poorThreshold,
                    "Бездомный",
                    homelessColor
                );
                break;

            case PlayerRank.Бедный:
                SetBar(
                    money,
                    poorThreshold,
                    wealthyThreshold,
                    "Бедный",
                    poorColor
                );
                break;

            case PlayerRank.Состоятельный:
                SetBar(
                    money,
                    wealthyThreshold,
                    richThreshold,
                    "Состоятельный",
                    wealthyColor
                );
                break;

            case PlayerRank.Богатый:
                SetBar(
                    money,
                    richThreshold,
                    millionaireThreshold,
                    "Богатый",
                    richColor
                );
                break;

            case PlayerRank.Миллионер:
                SetBar(
                    money,
                    millionaireThreshold,
                    millionaireMaxMoney,
                    "Миллионер",
                    millionaireColor
                );
                break;
        }
    }

    private void SetBar(
        int money,
        int minMoney,
        int maxMoney,
        string rankName,
        Color color)
    {
        float progress = Mathf.InverseLerp(
            minMoney,
            maxMoney,
            money
        );

        Vector2 size = fill.sizeDelta;
        size.x = fullWidth * progress;

        fill.sizeDelta = size;

        rankText.text = rankName;
        rankText.outlineColor = color;
        fillImage.color = color;
    }
}