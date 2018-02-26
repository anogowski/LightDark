using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float m_hp;
    [SerializeField] EnergyType m_type;
    Image HpBar;
    GameObject player;
    Player playerScript;
    NavMeshAgent m_navMeshAgent = null;
    Vector3 playerPos;

    float m_maxHP;

    void Start()
    {
        m_maxHP = m_hp;
        playerScript = player.GetComponent<Player>();
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        playerPos = player.transform.position;
        HpBar = gameObject.GetComponentInChildren<Image>();
    }

    public void SetPlayer(GameObject p)
    {
        player = p;
    }

    public void SetEnemy(EnergyType type, int hp)
    {
        m_type = type;
        m_hp = hp;
        m_maxHP = hp;
    }

    public void SetStats(int hp)
    {
        m_hp = hp;
        m_maxHP = hp;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        playerPos.y = 1;
        m_navMeshAgent.destination = playerPos;
    }

    public void Hit(EnergyType type, int val)
    {
        if (m_type == type)
        {
            m_hp += val;
            if (m_hp > m_maxHP)
            {
                m_hp = m_maxHP;
            }
        }
        else
        {
            m_hp -= val;
            if (m_hp <= 0)
            {
                GameManager.instance.addScore((int)m_maxHP);
                Destroy(this.gameObject);
            }
        }
        HpBar.fillAmount = m_hp / m_maxHP;
    }

    void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == player.tag)
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.Hit(m_hp);
            Destroy(this.gameObject);
        }
    }
}
