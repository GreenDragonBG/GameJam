using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public float range = 50f;
    public LayerMask interactableLayer;
    public float moveSpeed = 10f;
    public float holdDistance = 3f;

    private Camera cam;
    private Transform currentTarget;
    private Rigidbody targetRigidbody;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryLockTarget();
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            ReleaseTarget();
        }

        if (currentTarget != null && Input.GetKey(KeyCode.E))
        {
            MoveTargetInFrontOfCamera(currentTarget);
        }
    }

    void TryLockTarget()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, interactableLayer))
        {
            bool validTarget =
                hit.collider.CompareTag("Attractable");

            if (validTarget)
            {
                currentTarget = hit.transform;
                targetRigidbody = currentTarget.GetComponent<Rigidbody>();

                if (targetRigidbody != null)
                {
                    targetRigidbody.useGravity = false;
                    targetRigidbody.linearVelocity = Vector3.zero;
                }
            }
        }
    }

    void ReleaseTarget()
    {
        if (targetRigidbody != null)
        {
            targetRigidbody.useGravity = true;
        }

        currentTarget = null;
        targetRigidbody = null;
    }

    void MoveTargetInFrontOfCamera(Transform target)
    {
        if (targetRigidbody == null) return;

        Vector3 holdPosition = cam.transform.position + cam.transform.forward * holdDistance;

        // Use Rigidbody.MovePosition instead of transform.position
        Vector3 newPosition = Vector3.Lerp(target.position, holdPosition, moveSpeed * Time.deltaTime);
        targetRigidbody.MovePosition(newPosition);
    }
}
