using UnityEngine;

/*
 * ThrownAxe keeps the axe's held pose so it can be attached to the hand
 * after a throw. Launch physics and collision response belong here;
 * PlayerController decides when to throw and recall it.
 */

public class ThrownAxe : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Collider axeCollider;
    public float spinSpeed = 2000f;
    public TrailRenderer trailRenderer;
    
    Transform _hand;
    Vector3 _heldLocalPosition;
    Quaternion _heldLocalRotation;

    public Vector3 CatchPosition => _hand.TransformPoint(_heldLocalPosition);

    void Start()
    {
        trailRenderer.emitting = false;
    }

    public void Launch(Vector3 direction, float impulse, CharacterController thrower)
    {
        _hand = transform.parent;
        _heldLocalPosition = transform.localPosition;
        _heldLocalRotation = transform.localRotation;
        
        Physics.IgnoreCollision(axeCollider, thrower);
        transform.SetParent(null);
        transform.position += direction * 0.5f;
        
        rigidbody.isKinematic = false;
        axeCollider.enabled = true;
        trailRenderer.emitting = true;
        
        
        rigidbody.AddForce(direction * impulse, ForceMode.VelocityChange); //Impulse uses mass, VelocityChange does not
        rigidbody.AddTorque(transform.forward * (-spinSpeed * Mathf.Deg2Rad), ForceMode.VelocityChange);
        
    }

    public void AttachToHand()
    {
        transform.SetParent(_hand);
        transform.SetLocalPositionAndRotation(_heldLocalPosition, _heldLocalRotation);
        rigidbody.isKinematic = true;
        axeCollider.enabled = false;
        trailRenderer.emitting = false;
        // TODO Slice 8.3 (catch hook): stop the spin and restore the held look.
        // Check: throw and recall both spin. Two full cycles end with the original held look.
        // Next: polish, networking, and your showcase video. </> end of Slice 8
    }

    void OnCollisionEnter(Collision collision)
    {
        rigidbody.isKinematic = true;
        trailRenderer.emitting = false;
        
    }
}
