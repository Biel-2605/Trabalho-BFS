using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionManager : MonoBehaviour
{
    [Header("Países")]
    public CountryNode[] countries; // Lista de países (cada um é um vértice do grafo)

    [Header("Configuração da Infecção")]
    public int startIndex = 0;      // País inicial da infecção
    public float infectionDelay = 1.5f; // Delay entre ondas

    // Matriz de adjacência (1 = vizinho, 0 = não vizinho)
    private int[,] adjMatrix = new int[10, 10]
    {
        // BR AR CH BO PE CO VE EQ PA UR
        { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0 }, // Brasil
        { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0 }, // Argentina
        { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0 }, // Chile
        { 1, 0, 1, 0, 1, 0, 0, 0, 0, 0 }, // Bolívia
        { 0, 0, 0, 1, 0, 1, 0, 0, 0, 0 }, // Peru
        { 0, 0, 0, 0, 1, 0, 1, 1, 0, 0 }, // Colômbia
        { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 }, // Venezuela
        { 0, 0, 0, 0, 0, 1, 0, 0, 1, 0 }, // Equador
        { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 }, // Paraguai
        { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 }, // Uruguai
    };

    private bool infectionStarted = false;

    void Update()
    {
        // Pressiona espaço para começar a infecção
        if (!infectionStarted && Input.GetKeyDown(KeyCode.Space))
        {
            infectionStarted = true;
            StartCoroutine(BFSInfection());
        }
    }

    // BFS para infectar países
    IEnumerator BFSInfection()
    {
        Queue<int> queue = new Queue<int>();
        bool[] visited = new bool[countries.Length];

        // Começa no país inicial
        queue.Enqueue(startIndex);
        visited[startIndex] = true;
        countries[startIndex].Infect();

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            // Checa todos os vizinhos na matriz
            for (int neighbor = 0; neighbor < countries.Length; neighbor++)
            {
                if (adjMatrix[current, neighbor] == 1 && !visited[neighbor])
                {
                    visited[neighbor] = true;
                    countries[neighbor].Infect();
                    queue.Enqueue(neighbor);

                    // Delay para simular propagação por onda
                    yield return new WaitForSeconds(infectionDelay);
                }
            }
        }
    }
}
