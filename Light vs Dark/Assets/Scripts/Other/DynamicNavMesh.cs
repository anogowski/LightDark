using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DynamicNavMesh : MonoBehaviour
{
    [SerializeField] [Range(0.5f, 10.0f)] float m_distance = 1.0f;
    [SerializeField] NavMeshSurface m_surface;

    void Awake()
    {
        UpdateSurface();
    }

    void Update()
    {
        if ((m_surface.transform.position - transform.position).magnitude >= m_distance)
        {
            UpdateSurface();
        }
    }

    void UpdateSurface()
    {
        m_surface.RemoveData();
        m_surface.transform.position = transform.position;
        m_surface.BuildNavMesh();
    }
}
