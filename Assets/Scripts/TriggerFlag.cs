using UnityEngine;

public class TriggerFlag : MonoBehaviour
{
    public bool PlayerEntered { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerEntered = true;
        GetComponent<Animator>().SetBool("player_entered", true);
        GetComponent<AudioSource>().Play();
    }
}