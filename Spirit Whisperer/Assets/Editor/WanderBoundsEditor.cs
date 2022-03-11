using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(WanderBounds))]
public class WanderBoundsEditor : Editor
{
    private void OnSceneGUI()
    {
        WanderBounds wanderState = (WanderBounds)target;
        Handles.color = Color.yellow;
        Handles.DrawWireCube(wanderState.boundBox.center, wanderState.boundBox.size);
        Handles.color = Color.red;
        Handles.DrawWireDisc(wanderState.targetPos, Vector3.up, 0.2f);
    }
}
