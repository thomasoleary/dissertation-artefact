using System.Linq;
using System;
using UnityEngine;

public class testray : MonoBehaviour
{

    public Sides[] sidesArr;

    [HideInInspector] public Vector3[] vectorAxis = new Vector3[6];

    void Start()
    {
        InitialiseDirections();
    }

    void Update()
    {
        EvenMoreRay();
    }

    public void EvenMoreRay()
    {
        for (int i = 0; i < sidesArr.Count(); i++)
        {
            Debug.DrawRay(transform.position, (transform.rotation * GetAxis(sidesArr[i].axis)) * sidesArr[i].dist, Color.red);
            /* RaycastHit groundHit;
            if (Physics.Raycast(transform.position, (transform.rotation * GetAxis(sidesArr[i].axis)), out groundHit, sidesArr[i].dist, sidesArr[i].layers))
            {
                Debug.DrawRay(transform.position, (transform.rotation * GetAxis(sidesArr[i].axis)) * sidesArr[i].dist, Color.green);

                Debug.Log(gameObject.name + " " + sidesArr[i].axis.ToString() + " is hitting: " + groundHit.collider.name);
            } */
        }
    }

    [Serializable]
    public struct Sides
    {
        public Axisrotate axis;
        [Range(0.0f, 10.0f)]
        public float dist;
        //public LayerMask layers;
    }

    public enum Axisrotate
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        FORWARD,
        BACK
    }
    public Vector3 GetAxis(Axisrotate axis)
    {
        return vectorAxis[(int)axis];
    }

    public void InitialiseDirections()
    {
        vectorAxis[0] = Vector3.up;
        vectorAxis[1] = Vector3.down;
        vectorAxis[2] = Vector3.left;
        vectorAxis[3] = Vector3.right;
        vectorAxis[4] = Vector3.forward;
        vectorAxis[5] = Vector3.back;
    }
    
    /* public float distance = 1.0f;
    public float[] distances = new float[] {1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f};
    Vector3[] directions = new Vector3[] {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back}; */

    /* public void BoxRay()
    {
        foreach(var dir in directions)
        {
            Debug.DrawRay(transform.position, (transform.rotation * dir) * distance, Color.red);
            
            RaycastHit groundHit;
            if (Physics.Raycast(transform.position, (transform.rotation * dir), out groundHit, distance))
            {
                Debug.DrawRay(transform.position, (transform.rotation * dir) * distance, Color.green);
                
            }
        }
    }

    public void MoreRay()
    {
        for (int i = 0; i < directions.Count(); i++)
        {
            Debug.DrawRay(transform.position, (transform.rotation * directions[i]) * sidesArr[i].dist, Color.red);
            RaycastHit groundHit;
            if (Physics.Raycast(transform.position, (transform.rotation * directions[i]), out groundHit, sidesArr[i].dist, sidesArr[i].layers))
            {
                Debug.DrawRay(transform.position, (transform.rotation * directions[i]) * sidesArr[i].dist, Color.green);

                Debug.Log(sidesArr[i].axis.ToString() + " is hitting: " + groundHit.collider.name);
            }

        }
    }

    public void Ray()
    {
        for (int i = 0; i < directions.Count(); i++)
        {
            Debug.DrawRay(transform.position, (transform.rotation * directions[i]) * distances[i], Color.red);

            RaycastHit groundHit;
            if (Physics.Raycast(transform.position, (transform.rotation * directions[i]), out groundHit, distances[i]))
            {
                Debug.DrawRay(transform.position, (transform.rotation * directions[i]) * distances[i], Color.green);
            }
        }
    }
 */
    
    /* private List<float> distances = new List<float>();
    public int DIRECTIONS = 6;
    public float DoRay()
    {
        //float[] distances;

        List<float> distances = new List<float>();

        foreach(var dir in GetDirections(DIRECTIONS))
        {
            Debug.DrawRay(transform.position, dir, Color.blue);

            RaycastHit groundHit;
            if (Physics.Raycast(transform.position, dir, out groundHit, distance))
            {
                distances.Add(groundHit.distance);
            }
        }

        return distances.Any() ? distances.Min() : distance;
    }

    public Vector3[] GetDirections(int numDirections)
    {
        var points = new Vector3[numDirections];
        float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        float off = 2.0f / numDirections;

        foreach(int dir in Enumerable.Range(0, numDirections))
        {
            float y = dir * off - 1 + (off / 2);
            float r = Mathf.Sqrt(1 - y * y);
            float phi = dir * inc;
            float x = (float)(Mathf.Cos(phi) * r);
            float z = (float)(Mathf.Sin(phi) * r);
            points[dir] = new Vector3(x, y, z);
        }

        return points;
    } */
}
