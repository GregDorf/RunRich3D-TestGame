using UnityEngine;
using UnityEngine.Splines;

[ExecuteAlways]
public class SplineObjectPosition : MonoBehaviour
{
    [SerializeField] private SplineContainer spline;

    [Range(0f, 1f)]
    [SerializeField] private float progress;

    [SerializeField] private bool followRotation = true;

    private void Update()
    {
        UpdatePosition();
    }

    private void OnValidate()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (spline == null)
            return;

        Vector3 position = spline.EvaluatePosition(progress);

        transform.position = position;

        if (followRotation)
        {
            Vector3 forward = spline.EvaluateTangent(progress);

            if (forward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(
                    forward,
                    Vector3.up
                );
            }
        }
    }
}