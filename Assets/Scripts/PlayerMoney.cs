using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerRank
{
    Бездомный,
    Бедный,
    Состоятельный,
    Богатый,
    Миллионер
}

public class PlayerMoney : MonoBehaviour
{
    [Header("Money")]
    [SerializeField] private int money;
    [SerializeField] private TMP_Text text;

    [Header("Audio")]
    [SerializeField] private AudioSource moneySource;
    [SerializeField] private AudioClip plusMoney;
    [SerializeField] private AudioClip minusMoney;

    [Header("Rank thresholds")]
    [SerializeField] private int poorThreshold = 20;
    [SerializeField] private int wealthyThreshold = 60;
    [SerializeField] private int richThreshold = 100;
    [SerializeField] private int millionaireThreshold = 300;

    public int Money => money;

    public PlayerRank Rank
    {
        get
        {
            if (money < poorThreshold)
                return PlayerRank.Бездомный;

            if (money < wealthyThreshold)
                return PlayerRank.Бедный;

            if (money < richThreshold)
                return PlayerRank.Состоятельный;

            if (money < millionaireThreshold)
                return PlayerRank.Богатый;

            return PlayerRank.Миллионер;
        }
    }

    public void AddMoney(int value)
    {
        money += value;

        if (value > 0) moneySource.PlayOneShot(plusMoney);
        else moneySource.PlayOneShot(minusMoney);

        Debug.Log($"Money: {money}, Rank: {Rank}");
    }

    private void Update()
    {
        text.text = money.ToString();
    }
}