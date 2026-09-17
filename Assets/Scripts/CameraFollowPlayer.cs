using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform anchor;

    [Header("Position")]
    [SerializeField] private float height = 3f;
    [SerializeField] private float distance = 4f;

    [Header("Follow")]
    [SerializeField] private float followSpeed = 10f;

    [Header("Rotation")]
    [SerializeField] private float cameraTilt = 20f;
    [SerializeField] private float rotationSpeed = 10f;

    private void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        if (anchor == null) anchor = GameObject.FindGameObjectWithTag("Anchor").transform;
    }

    private void LateUpdate()
    {
        if (player == null || anchor == null)
            return;

        FollowPlayer();
        RotateWithAnchor();
    }

    private void FollowPlayer()
    {
        // Положение игрока относительно Anchor
        Vector3 playerOffset = player.position - anchor.position;

        // Убираем продольное смещение игрока.
        // Оставляем только боковое относительно Anchor.
        float sideOffset = Vector3.Dot(playerOffset, anchor.right);

        // Камера находится позади Anchor и смещается
        // влево/вправо вместе с игроком.
        Vector3 targetPosition =
            anchor.position
            + anchor.right * sideOffset
            - anchor.forward * distance
            + Vector3.up * height;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );
    }

    private void RotateWithAnchor()
    {
        Quaternion targetRotation =
            Quaternion.LookRotation(
                anchor.forward,
                Vector3.up
            ) *
            Quaternion.Euler(cameraTilt, 0f, 0f);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}