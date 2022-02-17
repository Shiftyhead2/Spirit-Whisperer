using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewScript : MonoBehaviour
{
    [Header("Detection Layer")]
    [SerializeField]
    LayerMask detectionLayer;

    [Header("Line Of Sight Detection")]
    [SerializeField]
    float characterEyeLevel = 1.5f;
    [SerializeField]
    LayerMask IgnoreForLineOfSight;

    [Header("Detection radius")]
    [SerializeField]
    float detectionRadius = 5f;


    [Header("Detection angle radius")]
    [SerializeField]
    float minimumDetectionRadiusAngle = -35f;
    [SerializeField]
    float maximumDetectionRadiusAngle = 35f;

    AiManager aiManager;

    private void Awake()
    {
        aiManager = GetComponent<AiManager>();
    }


    public void FindATargetViaLineOfSight()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);


        for (int i = 0; i < colliders.Length; i++)
        {

            if (colliders[i].transform.TryGetComponent(out FPSController player))
            {
                Vector3 targetDetection = transform.position - player.transform.position;
                float viewableAngle = Vector3.Angle(targetDetection, transform.forward);

                if (viewableAngle > minimumDetectionRadiusAngle && viewableAngle < maximumDetectionRadiusAngle)
                {
                    RaycastHit hit;
                    Vector3 playerStartPoint = new Vector3(player.transform.position.x, characterEyeLevel, player.transform.position.z);
                    Vector3 AIStartPoint = new Vector3(transform.position.x, characterEyeLevel, transform.position.z);

                    Debug.DrawLine(playerStartPoint, AIStartPoint, Color.red);

                    if (Physics.Linecast(playerStartPoint, AIStartPoint, out hit, IgnoreForLineOfSight))
                    {
                        return;
                    }
                    else
                    {
                        aiManager.currentTarget = player.transform;
                    }

                }
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.green;
    }
}
