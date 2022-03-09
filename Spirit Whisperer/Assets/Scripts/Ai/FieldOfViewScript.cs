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
    public float detectionRadius = 5f;


    [Header("Detection angle radius")]
    [Range(0f,360f)]
    public float angle;

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
                Vector3 targetDetection = (player.transform.position - transform.position).normalized;
                

                if (Vector3.Angle(transform.forward,targetDetection) < angle / 2)
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
}
