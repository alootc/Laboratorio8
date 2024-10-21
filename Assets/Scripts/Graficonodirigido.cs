using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graficonodirigido : MonoBehaviour
{
    public List<Conexion> conexiones; 

    public Graficonodirigido GetNodeRandom()
    {
        return conexiones[Random.Range(0, conexiones.Count)].nodo;
    }

    public int GetCostToNextNode()
    {
        return conexiones[0].cost; 
    }

    public int GetCostToNode(Graficonodirigido nextNode)
    {
        for (int i = 0; i < conexiones.Count; i++)
        {
            if (conexiones[i].nodo == nextNode)
            {
                return conexiones[i].cost;
            }
        }
        return int.MaxValue; 
    }
}

[System.Serializable]
public class Conexion
{
    public Graficonodirigido nodo; 
    public int cost; 
}
