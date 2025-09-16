using UnityEngine;

public class HitTarget : MonoBehaviour
{
    [Header("Target Settings")]
    public float fallForce = 5f;          // Extra force applied on hit
    public Vector3 forceDirection = Vector3.back; // Direction of the push

    private Rigidbody _rb;
    private bool _isHit = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        // Make sure the target starts as kinematic
        if (_rb != null)
        {
            _rb.isKinematic = true;
            _rb.useGravity = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if hit by arrow
        if (!_isHit && collision.gameObject.GetComponent<Arrow>() != null)
        {
            Debug.Log("Target hit by arrow!");
            ActivatePhysics();
        }
    }

    private void ActivatePhysics()
    {
        _isHit = true;

        if (_rb != null)
        {
            // Enable physics
            _rb.isKinematic = false;
            _rb.useGravity = true;

            // Apply a little push to make it fall more realistically
            _rb.AddForce(forceDirection.normalized * fallForce, ForceMode.Impulse);
        }
    }
}
