using UnityEngine;
using TMPro;
using ButchersGames;

public class LevelNumberUI : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;

    private void Start()
    {
        UpdateLevelText();
    }

    public void UpdateLevelText()
    {
        levelText.text = "Уровень " + LevelManager.CurrentLevel;
    }
}