using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] GameObject m_gameObject = null;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray.origin, ray.direction, out raycastHit))
            {
                Instantiate(m_gameObject, raycastHit.point + (Vector3.up * 3.0f), Quaternion.identity);
            }
        }
    }
}
