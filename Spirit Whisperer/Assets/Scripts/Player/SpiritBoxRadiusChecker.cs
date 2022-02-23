using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritBoxRadiusChecker : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask ghostLayerMask;
    [SerializeField] private float checkInterval;
    [SerializeField] private float repeatInterval;
    bool ghostInRange = false;


    private void Start()
    {
        ghostInRange = false;
        InvokeRepeating(nameof(IsGhostInRadius),checkInterval,repeatInterval);
    }



    private void IsGhostInRadius()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, ghostLayerMask);

        if(colliders.Length <= 0)
        {
            GameActions.onInsideRadiusOfGhost?.Invoke(false);

            if (ghostInRange)
            {
                ghostInRange = false;
            }

            return;
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].TryGetComponent(out AiManager ghost))
            {
                if (!ghostInRange)
                {
                    ghostInRange = true;
                    GameActions.onInsideRadiusOfGhost?.Invoke(true);
                }
                else
                {
                    return;
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (!ghostInRange)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }


        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
