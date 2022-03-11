using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderBounds : MonoBehaviour
{
    [Header("Wander Settings")]
    public Bounds boundBox;

    public Vector3 targetPos { get; private set; }


    public void SetTargetPos(AiManager aiManager)
    {
        targetPos = GetRandomPoint(aiManager);
    }



    public Vector3 GetRandomPoint(AiManager aiManager)
    {
        float randomX = Random.Range(-boundBox.extents.x + aiManager.AINavMeshAgent.radius, boundBox.extents.x - aiManager.AINavMeshAgent.radius);
        float randomZ = Random.Range(-boundBox.extents.z + aiManager.AINavMeshAgent.radius, boundBox.extents.z - aiManager.AINavMeshAgent.radius);
        return new Vector3(randomX, aiManager.transform.position.y, randomZ);
    }
}
