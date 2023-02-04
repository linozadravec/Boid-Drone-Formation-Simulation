using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {

    BoidSettings settings;

    private List<Boid> susjedi;

    //sirina sfere koja se casta za racunanje novog directiona pri kretanju prema sudaru
    public float sphereRadius = 1;


    public bool jeKraj = false;
    public float minSpeed = 0.5f;
    public float maxSpeed = 1f;
    // State
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public Vector3 forward;
    Vector3 velocity;

    // To update:
    Vector3 acceleration;
    [HideInInspector]
    public Vector3 avgFlockHeading;
    [HideInInspector]
    public Vector3 avgAvoidanceHeading;
    [HideInInspector]
    public Vector3 centreOfFlockmates;
    [HideInInspector]
    public int numPerceivedFlockmates;

    // Cached
    Material material;
    Transform cachedTransform;
    Transform target;

    void Awake () {
        material = transform.GetComponentInChildren<MeshRenderer> ().material;
        cachedTransform = transform;
    }



    public void Initialize (BoidSettings settings, Transform target) {
        this.target = target;
        this.settings = settings;

        position = cachedTransform.position;
        forward = cachedTransform.forward;

        float startSpeed = (minSpeed + maxSpeed) / 2;
        velocity = transform.forward * startSpeed;
    }

    public void UpdateBoid () {
        Vector3 acceleration = Vector3.zero;

        if (target != null)
        {
            if (System.Math.Abs(target.position.x - position.x) < 0.025 &&
                System.Math.Abs(target.position.y - position.y) < 0.025 &&
                System.Math.Abs(target.position.z - position.z) < 0.025)
            {
                jeKraj = true;
            }
            Vector3 offsetToTarget = (target.position - position);
            acceleration = SteerTowards(offsetToTarget) * settings.targetWeight;
        }

        if (!jeKraj)
        {
            if (IsHeadingForCollision())
            {
                Vector3 collisionAvoidDir = ObstacleRays();
                Vector3 collisionAvoidForce = SteerTowards(collisionAvoidDir) * settings.avoidCollisionWeight;
                acceleration += collisionAvoidForce;
            }

            velocity += acceleration * Time.deltaTime;
            float speed = velocity.magnitude;
            Vector3 dir = velocity / speed;
            speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
            velocity = dir * speed;

            cachedTransform.position += velocity * Time.deltaTime;
            //cachedTransform.forward = dir;
            position = cachedTransform.position;
            forward = dir;
        }
    }

    bool IsHeadingForCollision () {
        RaycastHit hit;
        if (Physics.SphereCast (position, sphereRadius, forward, out hit, settings.collisionAvoidDst, settings.obstacleMask)) {
            return true;
        } else { }
        return false;
    }

    Vector3 ObstacleRays () {
        Vector3[] rayDirections = BoidHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++) {
            Vector3 dir = cachedTransform.TransformDirection (rayDirections[i]);
            Ray ray = new Ray (position, dir);
            if (!Physics.SphereCast (ray, sphereRadius, settings.collisionAvoidDst, settings.obstacleMask)) {
                return dir;
            }
        }

        return forward;
    }

    Vector3 SteerTowards (Vector3 vector) {
        Vector3 v = vector.normalized * settings.maxSpeed - velocity;
        return Vector3.ClampMagnitude (v, settings.maxSteerForce);
    }

}