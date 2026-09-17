using UnityEngine;

public class PlayerFormRanking : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerMoney playerMoney;

    [Header("Player Forms")]
    [SerializeField] private GameObject homelessForm;
    [SerializeField] private GameObject poorForm;
    [SerializeField] private GameObject wealthyForm;
    [SerializeField] private GameObject richForm;
    [SerializeField] private GameObject millionaireForm;

    private void Start()
    {
        UpdateForm();
    }

    private void Update()
    {
        UpdateForm();
    }

    private void UpdateForm()
    {
        if (playerMoney == null)
            return;

        DisableAllForms();

        switch (playerMoney.Rank)
        {
            case PlayerRank.Бездомный:
                homelessForm.SetActive(true);
                break;

            case PlayerRank.Бедный:
                poorForm.SetActive(true);
                break;

            case PlayerRank.Состоятельный:
                wealthyForm.SetActive(true);
                break;

            case PlayerRank.Богатый:
                richForm.SetActive(true);
                break;

            case PlayerRank.Миллионер:
                millionaireForm.SetActive(true);
                break;
        }
    }

    private void DisableAllForms()
    {
        homelessForm.SetActive(false);
        poorForm.SetActive(false);
        wealthyForm.SetActive(false);
        richForm.SetActive(false);
        millionaireForm.SetActive(false);
    }
}