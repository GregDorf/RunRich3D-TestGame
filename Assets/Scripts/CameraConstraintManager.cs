using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraConstraintManager : MonoBehaviour
{
    public void SetupConstraints()
    {
        GameObject cameraObject = GameObject.FindGameObjectWithTag("Camera");

        if (cameraObject == null)
        {
            Debug.LogWarning(" амера с тегом Camera не найдена!");
            return;
        }

        LookAtConstraint[] constraints = FindObjectsOfType<LookAtConstraint>();

        foreach (LookAtConstraint constraint in constraints)
        {
            ConstraintSource source = new ConstraintSource
            {
                sourceTransform = cameraObject.transform,
                weight = 1f
            };

            constraint.SetSources(new List<ConstraintSource> { source });
            constraint.constraintActive = true;
        }

        Debug.Log("LookAtConstraint прив€заны к камере: " + cameraObject.name);
    }
}