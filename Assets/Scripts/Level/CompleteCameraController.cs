using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CompleteCameraController : MonoBehaviour {
    private struct PointInSpace {
        public Vector3 Position ;
        public float Time ;
    }
    
    [SerializeField]
    [Tooltip("The transform to follow")]
    private Transform target;
    
    [SerializeField]
    [Tooltip("The offset between the target and the camera")]
    private Vector3 offset;
    
    [Tooltip("The delay before the camera starts to follow the target")]
    [SerializeField]
    private float delay = 0.5f;
    
    [SerializeField]
    [Tooltip("The speed used in the lerp function when the camera follows the target")]
    private float speed = 5;

    private Rigidbody2D rb;
    
    ///<summary>
    /// Contains the positions of the target for the last X seconds
    ///</summary>
    private Queue<PointInSpace> pointsInSpace = new Queue<PointInSpace>();



    void Start() {
        offset = target.position - this.transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void LateUpdate () {
        // Add the current target position to the list of positions
        pointsInSpace.Enqueue( new PointInSpace() { Position = target.position, Time = Time.time } ) ;
        
        // Move the camera to the position of the target X seconds ago 
        while( pointsInSpace.Count > 0 && pointsInSpace.Peek().Time <= Time.time - delay + Mathf.Epsilon ) {
            int direction = target.rotation.y == 0 ? 50 : 35;
            Vector3 position = Vector3.Lerp( rb.position , pointsInSpace.Dequeue().Position + offset + Vector3.up*15 + Vector3.right * direction , Time.deltaTime * speed);
            rb.MovePosition(position);
        }
    }
}