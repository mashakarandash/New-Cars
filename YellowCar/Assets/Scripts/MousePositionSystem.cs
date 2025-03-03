using UnityEngine;
using UnityEngine.AI;

public class MousePositionSystem : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private NavMeshAgent _agent;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(GetNewRay(), out RaycastHit hit))
            {
                _agent.SetDestination(hit.point);
            }
        }
    }

    private Ray GetNewRay()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        return ray;
    }
}
