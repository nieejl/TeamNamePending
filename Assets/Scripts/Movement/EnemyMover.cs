using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMover : MonoBehaviour
{
    public enum FollowBehaviour
    {
        FollowCashier,
        FollowPlayer,
    }

    private NavMeshAgent _navMeshAgent;
    [SerializeField]
    private FollowBehaviour _currentFollowBehaviour;
    private Transform _currentTarget;
    private Vector3 _previousPosition;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetFollowBehaviour(_currentFollowBehaviour);
    }

    private void Update()
    {
        if(_previousPosition != _currentTarget.position)
        {
            _navMeshAgent.SetDestination(_currentTarget.position);
            _previousPosition = _currentTarget.position;
        }
    }

    public void SetFollowBehaviour(FollowBehaviour followBehaviour)
    {
        _currentFollowBehaviour = followBehaviour;
        switch(_currentFollowBehaviour)
        {
            case FollowBehaviour.FollowPlayer:
                _currentTarget = MovementController.Instance.PlayerTrasform;
                break;

            case FollowBehaviour.FollowCashier:
                _currentTarget = MovementController.Instance.CashierTrasform;
                break;
        }
        _navMeshAgent.SetDestination(_currentTarget.position);
        _previousPosition = _currentTarget.position;
    }
}
