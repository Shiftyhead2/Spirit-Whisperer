using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpiritBoxRadiusChecker))]
public class SpiritBoxRangeEditor : Editor
{
    private void OnSceneGUI()
    {
        SpiritBoxRadiusChecker spiritBox = (SpiritBoxRadiusChecker)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(spiritBox.transform.position, Vector3.up, Vector3.forward, 360, spiritBox.detectionDistanceMax);

        if (spiritBox.ghostInRange)
        {
            Handles.color = Color.green;
            
        }
        else
        {
            Handles.color = Color.red;
        }


        if (spiritBox.ghost != null)
        {
            Handles.DrawLine(spiritBox.transform.position, spiritBox.ghost.transform.position);
        }
    }
}
