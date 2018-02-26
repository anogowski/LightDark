using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] string EnemyTag;
    [SerializeField] EnergyType m_type;
    [SerializeField] int m_dmg;
    [SerializeField] float m_lifespan;
    [SerializeField] float m_speed;
    public void SetBullet(EnergyType type, int dmg, float lifespan)
    {
        m_type = type;
        m_dmg = dmg;
        m_lifespan = lifespan;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = transform.forward * Time.deltaTime *m_speed;
        Vector3 pos = transform.position + vel;
        transform.position = pos;
        m_lifespan -= Time.deltaTime;
        if (m_lifespan <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        Debug.Log(tag);
        if (tag == EnemyTag)
        {
            Enemy e = collision.gameObject.GetComponent<Enemy>();
            e.Hit(m_type, m_dmg);
            Destroy(this.gameObject);
        }
    }
}
