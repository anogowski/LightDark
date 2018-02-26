using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] GameObject LightBullet;
    [SerializeField] GameObject DarkBullet;
    [SerializeField] Image HpBar;
    [Header("Stats")]
    [SerializeField] float hp = 5;
    [SerializeField] int dmg = 1;
    [SerializeField] int spd = 4;
    int bounds = 24;
    float m_maxHP;
    // Use this for initialization
    void Start()
    {
        m_maxHP = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Fire(EnergyType.LIGHT);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Fire(EnergyType.DARK);
        }
        if(Input.GetKey("w"))
        {
            transform.position = transform.position + (transform.forward * spd * Time.deltaTime);
        }
        else if (Input.GetKey("s"))
        {
            transform.position = transform.position + (-transform.forward * spd * Time.deltaTime);
        }
        //TODO: Rotation
        if (Input.GetKey("a"))
        {
            transform.position = transform.position + (-transform.right * spd * Time.deltaTime);
        }
        else if (Input.GetKey("d"))
        {
            transform.position = transform.position + (transform.right * spd * Time.deltaTime);
        }

        Vector3 pos = transform.position;

        if(pos.x < -bounds)
        {
            pos.x = -bounds;
        }
        else if (pos.x > bounds)
        {
            pos.x = bounds;
        }

        if (pos.z < -bounds)
        {
            pos.z = -bounds;
        }
        else if (pos.z > bounds)
        {
            pos.z = bounds;
        }

        transform.position = pos;
    }

    void Fire(EnergyType type)
    {
        Vector3 front = transform.position + transform.forward;
        switch (type)
        {
            case EnergyType.LIGHT:
                GameObject lb = Instantiate(LightBullet, front, Quaternion.identity);
                break;
            case EnergyType.DARK:
                GameObject db = Instantiate(DarkBullet, front, Quaternion.identity);
                break;
            default:
                break;
        }
    }

    public void Hit(float val)
    {
        hp -= val;
        HpBar.fillAmount = hp/ m_maxHP;
        if(hp <= 0)
        {
            Destroy(this.gameObject,0.1f);

            GameManager.instance.GameOver();
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
