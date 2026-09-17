using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private int value = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerMoney playerMoney =
            other.GetComponent<PlayerMoney>();

        if (playerMoney != null)
        {
            playerMoney.AddMoney(value);
            Destroy(transform.parent != null && transform.parent.CompareTag("Door") ? transform.parent.gameObject : gameObject);
        }
    }
}