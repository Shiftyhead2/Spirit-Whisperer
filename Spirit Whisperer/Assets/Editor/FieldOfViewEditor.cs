using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(FieldOfViewScript))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfViewScript fov = (FieldOfViewScript)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.detectionRadius);

        Vector3 viewAngle1 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle2 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.blue;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle1 * fov.detectionRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle2 * fov.detectionRadius);
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
