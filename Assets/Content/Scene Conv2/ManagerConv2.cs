using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerConv2 : MonoBehaviour 
{
//    const int particlesPerConnection = 128;

    public List<Connection> connections = new List<Connection>();

    ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000];
    public ParticleSystem lines;

    public void Start()
    {
        lines.Emit(1000);
    }

    public void AddConnection(GameObject a, GameObject b)
    {
        var add = new Connection(a, b);
        if (!connections.Contains(add))
        {
            connections.Add(add);
        }
    }

    public void Update()
    {
        var size = 0;
        lines.GetParticles(particles);

        for (int i = 0; i < connections.Count; i++)
        {
            var a = connections[i].a.transform.position;
            var b = connections[i].b.transform.position;
            var d = b - a;
            var dNorm = d.normalized;
            var dPerp = new Vector3(-dNorm.y, dNorm.x);
            var mag = d.magnitude;
            a += dNorm * 0.3f;
            b -= dNorm * 0.3f;
            var particlesPerConnection = Mathf.RoundToInt(mag * 12);
            for (int c = 0; c < particlesPerConnection; c++) 
            {
                var index = size + c;
                particles[index].lifetime = 1;
                var l = c / (float)(particlesPerConnection - 1);
                particles[index].position = Vector3.Lerp(a, b, l)
                    + dPerp * Mathf.Sin(mag * -1f + mag * l * 5.25f + Time.time * 9) * 0.125f;
            }
            size += particlesPerConnection;
        }

        lines.SetParticles(particles, size);
    }

    public static ManagerConv2 inst
    {
        get
        {
            return GameObject.FindGameObjectWithTag("manager").GetComponent<ManagerConv2>();
        }
    }
}

[System.Serializable]
public struct Connection
{
    public GameObject a;
    public GameObject b;

    public Connection(GameObject a, GameObject b)
    {
        if (a.GetInstanceID() < b.GetInstanceID())
        {
            this.a = a;
            this.b = b;
        }
        else
        {
            this.b = a;
            this.a = b;
        }
    }
}
