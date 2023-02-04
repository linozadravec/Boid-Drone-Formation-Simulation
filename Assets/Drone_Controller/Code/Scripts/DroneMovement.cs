using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    public float minSpeed = 0.5f;
    public float maxSpeed = 1f;
    public float maxSteerForce = 1.2f;

    public bool jeKraj = false;
    public float udaljenostOdSusjeda = 1f;
    public List<GameObject> susjedi;
    Transform mojTransform;
    Transform target;
    Vector3 velocity;

    private float multiplier = 1f;

    public void Initialize(Transform target)
    {
        mojTransform = GetComponent<Transform>();
        this.target = target;

        float startSpeed = (minSpeed + maxSpeed) / 2;
        velocity = mojTransform.forward * startSpeed;
    }

    public void OnCollisionEnter(Collision sudar)
    {
        //Time.timeScale = 0; za debug
        Debug.Log("SUDAR");
    }

    public void OnTriggerEnter(Collider sudar)
    {
        susjedi.Add(sudar.gameObject);
        //Debug.Log(susjedi.Count);
    }

    public void OnTriggerExit(Collider sudar)
    {
        susjedi.Remove(sudar.gameObject);
        //Debug.Log(susjedi.Count);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!jeKraj)
        {
            Vector3 acceleration = Vector3.zero;
            if (target != null)
            {
                if (System.Math.Abs(target.position.x - mojTransform.position.x) < 0.025 &&
                    System.Math.Abs(target.position.y - mojTransform.position.y) < 0.025 &&
                    System.Math.Abs(target.position.z - mojTransform.position.z) < 0.025)
                {
                    jeKraj = true;
                }

                Vector3 offsetToTarget = (target.position - mojTransform.position);

                //fixa bug di drone nikad ne sleti nego se vrti
                if(offsetToTarget.magnitude < 0.75)
                {
                    //scaleanje s blizinom
                    multiplier = (1/(offsetToTarget.magnitude/2));
                }

                acceleration = SteerTowards(offsetToTarget) * multiplier;

                acceleration += Odvajanje() * 3;

                //transform.position += acceleration * Time.deltaTime;

                velocity += acceleration * Time.deltaTime;
                float speed = velocity.magnitude;
                Vector3 dir = velocity / speed;
                speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
                velocity = dir * speed;

                transform.position += velocity * Time.deltaTime;
            }
        }
    }

    public Vector3 Odvajanje()
    {
        Vector3 suprotniVektor = Vector3.zero;
        int brojac = 0;

        for (int i = 0; i < susjedi.Count; i++)
        {
            if (Vector3.Distance(susjedi[i].transform.position, transform.position) < udaljenostOdSusjeda)
            {
                brojac += 1;

                //cim blize to je blizina veci broj (max 2)
                //float blizina = 3 - (Vector3.Distance(susjedi[i].transform.position, transform.position) / 0.75f);

                suprotniVektor += (transform.position - susjedi[i].transform.position);
            }
        }

        if (brojac != 0)
        {
            suprotniVektor = suprotniVektor / brojac;
        }

        return suprotniVektor;
    }

    Vector3 SteerTowards(Vector3 vector)
    {
        Vector3 v = vector.normalized * maxSpeed - velocity;
        return Vector3.ClampMagnitude(v, maxSteerForce);
    }

    void Odluci()
    {
        //Initialize(target)
    }
}
