using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int requiredMoney = 10;
    [SerializeField] private bool finalDoor;

    [SerializeField] private AudioClip doorSound;
    [SerializeField] private AudioClip finalDoorSound;

    private bool checkedPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (checkedPlayer)
            return;

        if (!other.CompareTag("Player"))
            return;

        PlayerMoney playerMoney =
            other.GetComponent<PlayerMoney>();

        if (playerMoney == null)
            return;

        checkedPlayer = true;

        CheckPlayer(playerMoney);
    }

    private void CheckPlayer(PlayerMoney playerMoney)
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (finalDoor)
        {
            if (audioSource != null && finalDoorSound != null)
                audioSource.PlayOneShot(finalDoorSound);

            GameStateManager.Instance.Win();
            return;
        }

        if (playerMoney.Money < requiredMoney)
        {
            if (audioSource != null && doorSound != null)
                audioSource.PlayOneShot(finalDoorSound);

            GameStateManager.Instance.Win();
            return;
        }

        // ќбычна€ дверь при достаточном количестве денег.
        if (audioSource != null && doorSound != null)
        {
            audioSource.PlayOneShot(doorSound);
            GetComponent<Animator>().SetBool("door_opened", true);
        }
    }
}