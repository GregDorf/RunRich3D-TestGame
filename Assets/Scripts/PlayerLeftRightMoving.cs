using System.Collections.Generic;
using UnityEngine;

public class PlayerLeftRightMoving : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform anchor;

    [Header("Movement")]
    [SerializeField] private float swipeSensitivity = 0.02f;

    [Header("Borders")]
    [SerializeField] private float roadHalfWidth = 2f;

    [Header("Rotation")]
    [SerializeField] private float maxRotationAngle = 15f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float rotationReturnDelay = 0.15f;

    [Header("Step Sounds")]
    [SerializeField] private AudioSource stepSource;
    [SerializeField] private List<AudioClip> steps = new List<AudioClip>(3);
    [SerializeField] private float stepInterval = 0.3f;

    private float stepTimer;

    private float previousPointerX;
    private float movementDirection;
    private float lastMovementTime;
    private int prev_step;
    private Animator animator;

    private void Start()
    {
        prev_step = Random.Range(0, steps.Count);
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        ReadInput();
        RotatePlayer();
        if (GameStateManager.Instance.GameStarted && !GameStateManager.Instance.GameEnded) PlaySteps();

        animator.SetBool("gameStarted", GameStateManager.Instance.GameStarted);
        animator.SetBool("win", GameStateManager.Instance.GetWin);
        animator.SetBool("lose", GameStateManager.Instance.GetLose);
    }

    private void ReadInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    previousPointerX = touch.position.x;

                    break;

                case TouchPhase.Moved:

                    float deltaX =
                        touch.position.x - previousPointerX;

                    if (Mathf.Abs(deltaX) > 0.01f)
                    {
                        MoveFromSwipe(deltaX);

                        movementDirection = Mathf.Sign(deltaX);
                        lastMovementTime = Time.time;
                    }

                    previousPointerX = touch.position.x;

                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    break;
            }

            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            previousPointerX = Input.mousePosition.x;

            return;
        }

        if (Input.GetMouseButton(0))
        {
            float deltaX =
                Input.mousePosition.x - previousPointerX;

            if (Mathf.Abs(deltaX) > 0.01f)
            {
                MoveFromSwipe(deltaX);

                movementDirection = Mathf.Sign(deltaX);
                lastMovementTime = Time.time;
            }

            previousPointerX = Input.mousePosition.x;
        }
    }

    private void MoveFromSwipe(float deltaX)
    {
        if (anchor == null)
            return;

        // Оставляем исходное рабочее движение.
        Vector3 movement =
            anchor.right *
            deltaX *
            swipeSensitivity;

        transform.position += movement;

        ClampToRoad();
    }

    private void ClampToRoad()
    {
        if (anchor == null)
            return;

        Vector3 offset =
            transform.position - anchor.position;

        float sideOffset =
            Vector3.Dot(
                offset,
                anchor.right
            );

        if (sideOffset > roadHalfWidth)
        {
            transform.position -=
                anchor.right *
                (sideOffset - roadHalfWidth);
        }
        else if (sideOffset < -roadHalfWidth)
        {
            transform.position +=
                anchor.right *
                (-roadHalfWidth - sideOffset);
        }
    }

    private void RotatePlayer()
    {
        if (anchor == null)
            return;

        Quaternion anchorRotation =
            Quaternion.LookRotation(
                anchor.forward,
                Vector3.up
            );

        float targetAngle;

        if (Time.time - lastMovementTime < rotationReturnDelay)
        {
            targetAngle =
                movementDirection * maxRotationAngle;
        }
        else
        {
            targetAngle = 0f;
        }

        Quaternion targetRotation =
            anchorRotation *
            Quaternion.Euler(
                0f,
                targetAngle,
                0f
            );

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    public void PlaySteps()
    {
        stepTimer -= Time.deltaTime;

        if (stepTimer > 0f)
            return;

        int num_step;
        do num_step = Random.Range(0, steps.Count); while (num_step == prev_step);
        stepSource.PlayOneShot(steps[num_step]);
        prev_step = num_step;

        // Запускаем таймер следующего шага
        stepTimer = stepInterval;
    }
}