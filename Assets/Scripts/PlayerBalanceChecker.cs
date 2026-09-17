using UnityEngine;

public class PlayerBalanceChecker : MonoBehaviour
{
    private PlayerMoney playerMoney;

    private void Awake()
    {
        playerMoney = GetComponent<PlayerMoney>();
    }

    private void Update()
    {
        if (playerMoney == null)
            return;

        if (playerMoney.Money <= 0)
        {
            GameStateManager.Instance.Lose();
        }
    }
}