using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
public class Obstacle : MonoBehaviour
{
    private NavMeshObstacle _navMeshObstacle;

    private void Awake()
    {
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
    }

    private void Reset()
    {
        if(!gameObject.isStatic)
        {
            gameObject.isStatic = true;
        }
    }
}
