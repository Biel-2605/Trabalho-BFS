using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionManager : MonoBehaviour
{
    [Header("Pa�ses")]
    public CountryNode[] countries; // Lista de pa�ses (cada um � um v�rtice do grafo)

    [Header("Configura��o da Infec��o")]
    public int startIndex = 0;      // Pa�s inicial da infec��o
    public float infectionDelay = 1.5f; // Delay entre ondas

    // Matriz de adjac�ncia (1 = vizinho, 0 = n�o vizinho)
    private int[,] adjMatrix = new int[10, 10]
    {
        // BR AR CH BO PE CO VE EQ PA UR
        { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0 }, // Brasil
        { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0 }, // Argentina
        { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0 }, // Chile
        { 1, 0, 1, 0, 1, 0, 0, 0, 0, 0 }, // Bol�via
        { 0, 0, 0, 1, 0, 1, 0, 0, 0, 0 }, // Peru
        { 0, 0, 0, 0, 1, 0, 1, 1, 0, 0 }, // Col�mbia
        { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 }, // Venezuela
        { 0, 0, 0, 0, 0, 1, 0, 0, 1, 0 }, // Equador
        { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 }, // Paraguai
        { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 }, // Uruguai
    };

    private bool infectionStarted = false;

    void Update()
    {
        // Pressiona espa�o para come�ar a infec��o
        if (!infectionStarted && Input.GetKeyDown(KeyCode.Space))
        {
            infectionStarted = true;
            StartCoroutine(BFSInfection());
        }
    }

    // BFS para infectar pa�ses
    IEnumerator BFSInfection()
    {
        Queue<int> queue = new Queue<int>();
        bool[] visited = new bool[countries.Length];

        // Come�a no pa�s inicial
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

                    // Delay para simular propaga��o por onda
                    yield return new WaitForSeconds(infectionDelay);
                }
            }
        }
    }
}
