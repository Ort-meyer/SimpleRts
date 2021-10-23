using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : BaseWeapon
{
    public GameObject m_projectilePrefab;

    // Configurable
    public float m_elevationSpeed;
    public float m_fireVelocity; // Should this be in the projectile? Have here for now
    public float m_maxFireCooldown;
    public float m_spread;

    // Around local x
    public float m_maxElevation;
    public float m_minElevation;
    // Around local y
    public float m_maxTraverse;

    // State
    private float m_currentFireCooldown;

    // Use this for initialization
    void Start()
    {

        m_currentFireCooldown = m_maxFireCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_currentFireCooldown > 0)
        {
            m_currentFireCooldown -= Time.deltaTime;
        }
        if (m_target)
        {
            Vector3 toTarget = m_target.position - transform.position;
            float targetRotationAngle = Helpers.GetAngleToHit(toTarget.magnitude, toTarget.y, m_fireVelocity); // Should be difference in y?

            Transform cannon = transform;
            Vector3 oldUp = cannon.up; // This looks silly with high traverse. Should have same default up as parent

            cannon.rotation = Quaternion.LookRotation(toTarget.ZeroY().normalized, Vector3.up);
            cannon.Rotate(new Vector3(1, 0, 0), -1 * targetRotationAngle, Space.Self);
            cannon.rotation = Quaternion.LookRotation(cannon.forward, oldUp);

            //Correct rotation with constraints
            Vector3 eulerAngles = transform.localRotation.eulerAngles;
            float x = eulerAngles.x;
            x = (x > 180) ? x - 360 : x;
            x *= -1;
            if (x > m_maxElevation)
            {
                x = m_maxElevation;
            }
            if (x < -1 * m_minElevation)
            {
                x = -1 * m_minElevation;
            }
            float y = eulerAngles.y;
            y = (y > 180) ? y - 360 : y;
            if (y > m_maxTraverse)
            {
                y = m_maxTraverse;
            }
            if (y < -1 * m_maxTraverse)
            {
                y = -1 * m_maxTraverse;
            }

            eulerAngles = new Vector3(-1 * x, y, 0);
            transform.localRotation = Quaternion.Euler(eulerAngles);

            if (m_currentFireCooldown <= 0)
            {
                M_FireWeapon();
                m_currentFireCooldown = m_maxFireCooldown;
            }
        }
    }

    private void M_FireWeapon()
    {
        CannonShell newProjectile = Instantiate(m_projectilePrefab, transform.position, transform.rotation).GetComponent<CannonShell>();

        System.Random random = new System.Random();
        float spreadx = ((float)random.NextDouble() - 0.5f) * m_spread;
        float spready = ((float)random.NextDouble() - 0.5f) * m_spread;
        newProjectile.transform.Rotate(new Vector3(spreadx, spready));
        // Spread it some
        //float spreadx = Random.Range(-m_weaponSpread, m_weaponSpread);
        //float spready = Random.Range(-m_weaponSpread, m_weaponSpread);
        //newProjectile.transform.Rotate(spreadx, spready, 0, Space.Self);
        newProjectile.GetComponent<Rigidbody>().velocity = newProjectile.transform.forward.normalized * m_fireVelocity;
        // Add firing unit to the projectile so it doesnt hit itself
        newProjectile.M_Init();
        newProjectile.m_firingUnitObject = GetComponentInParent<BaseUnit>().gameObject;
        newProjectile.M_FireAtTarget(m_fireVelocity, m_target);

        //firingUnit.M_AddRecoil(newProjectile.transform.forward * m_recoil);
        //m_readyToFire = false;
        //m_currentCooldown = m_maxCooldown;
    }
}
