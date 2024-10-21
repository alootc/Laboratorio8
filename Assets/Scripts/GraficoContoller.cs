using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraficoContoller : MonoBehaviour
{
    public Graficonodirigido nodo;
    public float speed;
    public int energy;
    public int maxEnergy = 10;
    public float descansoTiempo = 5f;

    private Rigidbody2D rb;
    private Vector2 dir;
    private bool isdescansar;
    private float tiempo;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetNewDireccion();
        energy -= nodo.GetCostToNextNode();
    }

    private void Update()
    {
        if (isdescansar)
        {
            tiempo += Time.deltaTime;
            if (tiempo >= descansoTiempo)
            {
                tiempo = 0;
                energy = Mathf.Min(maxEnergy, energy + 1);
                if (energy >= nodo.GetCostToNextNode())
                {
                    isdescansar = false;
                    SetNewDireccion();
                }
            }
            return;
        }

        if (energy <= 0) // Verifica si la energía es cero o negativa
        {
            rb.velocity = Vector2.zero; // Detén al jugador
            StartCoroutine(descansaryrecuperacion()); // Entra en modo descanso y recuperación
            return; // No continúa trasladándose
        }

        rb.velocity = dir.normalized * speed * Time.deltaTime;

        if (Vector2.Distance(nodo.transform.position, transform.position) < 0.1f)
        {
            rb.velocity = Vector2.zero;
            CheckNodo();
        }
    }

    private void CheckNodo()
    {
        Graficonodirigido node_tmp = nodo.GetNodeRandom();
        int cost = nodo.GetCostToNode(node_tmp);

        if (energy >= cost)
        {
            nodo = node_tmp;
            energy -= cost;
            SetNewDireccion();
        }
        else
        {
            StartCoroutine(descansaryrecuperacion());
        }
    }

    private void SetNewDireccion()
    {
        dir = (Vector2)nodo.transform.position - rb.position;
    }

    private IEnumerator descansaryrecuperacion()
    {
        isdescansar = true;
        rb.velocity = Vector2.zero;
        tiempo = 0;

        while (isdescansar)
        {
            yield return new WaitForSeconds(5f);
            energy++;
            if (energy >= nodo.GetCostToNextNode())
            {
                isdescansar = false;
                SetNewDireccion();
            }
        }
    }
}
