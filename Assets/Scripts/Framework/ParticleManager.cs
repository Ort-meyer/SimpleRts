using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyPhysicsFunctions
{
    public static float Gravity(float mass)
    {
        return 9.82f * mass;
    }
    public static Vector3 AirResistanceVector(Vector3 velocity, float radius)
    {
        return new Vector3();
    }
}

public class ParticleManager : Singleton<ParticleManager>
{

    private List<GameObject> m_activeParticleEffects = new List<GameObject>();
    //private List<GameObject> m_activeBulletHoles = new List<GameObject>(); // Do I ever need this?

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void M_AddParticle(GameObject newParticleEffect)
    {
        m_activeParticleEffects.Add(newParticleEffect);
    }


    float mass;
    Vector3 velocity;
    float radius;
    Vector3 position;

    private void FixedUpdate()
    {
        Vector3 totalForce = new Vector3();
        totalForce += new Vector3(0, 1, 0) * MyPhysicsFunctions.Gravity(mass);
        totalForce += MyPhysicsFunctions.AirResistanceVector(velocity, radius);

        velocity += totalForce;
        position += velocity * Time.deltaTime;
    }

}
