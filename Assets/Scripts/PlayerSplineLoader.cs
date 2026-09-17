using UnityEngine;
using UnityEngine.Splines;

public class PlayerSplineLoader : MonoBehaviour
{
    private SplineAnimate splineAnimate;

    private void Start()
    {
        splineAnimate = GetComponent<SplineAnimate>();
        FindSpline();
    }

    public void FindSpline()
    {
        GameObject splineObject = GameObject.FindGameObjectWithTag("Spline");

        if (splineObject == null)
        {
            Debug.LogWarning("Объект с тегом PlayerSpline не найден!");
            return;
        }

        SplineContainer spline = splineObject.GetComponent<SplineContainer>();

        if (spline == null)
        {
            Debug.LogWarning("На объекте с тегом PlayerSpline нет SplineContainer!");
            return;
        }

        splineAnimate.Container = spline;
        splineAnimate.Restart(true);
    }
}