using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private Mesh mesh; //cached a mesh
    private float startingAngle;
    private float fov; //field of view angle
    private Vector3 origin;
    
    float viewDistance = 10f; //how far we can see
    private void Start()
    {
        mesh = new Mesh(); // we created new mesh
        GetComponent<MeshFilter>().mesh = mesh; // we assigned new mesh to our mesh component
        Vector3 origin = Vector3.zero; // we assigned our vertex origin
        fov = 90f;
    }

    private void Update()
    {
        
        int rayCount = 50; // how many rays we will spawn (if we spawn more than two rays we will have smoother fov)
        float angle = startingAngle; // for current angle

        float angleIncrease = fov / rayCount; //how much to increase our angle 

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];  // we will have 0 point ray and 2 other rays for the raycast and 0 vertice point in object. total 4 vertices
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = new int[rayCount * 3];  //we need 3 triangle points to make a triangle and we spawned 2 rays and we want to make them triangle for field of view

            vertices[0] = origin; //we assigned origin of the triangles.

            int vertexIndex = 1; // because we assigned 0 of the vertices we start from 1.
            int triangleIndex = 0; // we assigned array index of triangle as zero
            for (int i = 0; i <= rayCount; i++) //for each loop for our rays
            {
                Vector3 vertex = origin + GetVectorFromAngle(angle) * viewDistance; // this is how big our triangles are
                RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask); // simply it s origin, direction and distance. we are raycasting for blocking sight
                if (raycastHit2D.collider == null)
                {
                    // No hit
                    vertex = origin + GetVectorFromAngle(angle) * viewDistance; //if not hit protect its form
                }
                else
                {
                    //Hit object
                    Debug.Log("I'm hitting"); 
                    vertex = raycastHit2D.point; //change its shape for raycast
                }
                vertices[vertexIndex] = vertex;

                if (i > 0) //we only run this if we have previous vertex  and first ray doesn't have previous vertex so we run this in other rays
                {
                    triangles[triangleIndex + 0] = 0; //triangle from the origin
                    triangles[triangleIndex + 1] = vertexIndex - 1; // goes to the previous vertex 
                    triangles[triangleIndex + 2] = vertexIndex; // goes to the other vertex

                    triangleIndex += 3; // we increased our triangle index by 3. we have at least 2 rays
                }

                vertexIndex++; //we added another vertex our vertex index
                angle -= angleIncrease; // in unity angle increase goes counter clockwise so we extracted to go clockwise
            }   
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
    }
    

    public void SetOrigin(Vector3 origin) //setting up new origin for raycast in mesh
    {
        this.origin = origin;
    }

    public void SetLookDirection(Vector3 lookDirection) //setting up new direction for raycast
    {
        startingAngle = GetAngleFromVector(lookDirection) - fov / 2f;
    }
    public static Vector3 GetVectorFromAngle(float angle) // this is a general method for getting vector from angle! it is all math!
    {
        //Angle = 0 -> 360
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public static float GetAngleFromVector(Vector3 dir) //this is a general method for getting angle from a vector!
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

}
