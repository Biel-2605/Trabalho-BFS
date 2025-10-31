using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionManager : MonoBehaviour
{
    [Header("Países")]
    public CountryNode[] countries; // Lista de países no mapa (definida no Inspector)

    [Header("Configuração da Infecção")]
    [Tooltip("Índice do país inicial (defina no Inspector antes de dar Play)")]
    public int startIndex = -1; // Índice do país que começa infectado (-1 = nenhum)
    public float infectionDelay = 1.5f; // Tempo entre infecções de cada país

    // Matriz de adjacência (1 = país vizinho, 0 = não vizinho)
    private int[,] adjMatrix = new int[10, 10]
    {
        // BR AR CH BO PE CO VE EQ PA UR
        { 0, 1, 0, 1, 1, 1, 1, 0, 1, 1 }, // Brasil
        { 1, 0, 1, 1, 0, 0, 0, 0, 1, 1 }, // Argentina
        { 0, 1, 0, 1, 1, 0, 0, 0, 0, 0 }, // Chile
        { 1, 1, 1, 0, 1, 0, 0, 0, 1, 0 }, // Bolívia
        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 0 }, // Peru
        { 1, 0, 0, 0, 1, 0, 1, 1, 0, 0 }, // Colômbia
        { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0 }, // Venezuela
        { 0, 0, 0, 0, 1, 1, 0, 0, 0, 0 }, // Equador
        { 1, 1, 0, 1, 0, 0, 0, 0, 0, 0 }, // Paraguai
        { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 }, // Uruguai
    };

    private bool infectionStarted = false; // Flag para impedir múltiplas execuções

    void Update()
    {
        // Quando apertar Espaço, inicia a infecção
        if (!infectionStarted && Input.GetKeyDown(KeyCode.Space))
        {
            // Verifica se o índice inicial é válido
            if (startIndex < 0 || startIndex >= countries.Length)
            {
                return; // Se inválido, não faz nada
            }

            infectionStarted = true;           // Marca que a infecção começou
            StartCoroutine(InfectSequentially()); // Inicia a corrotina BFS
        }
    }

    // Corrotina que infecta os países **país por país** usando BFS
    IEnumerator InfectSequentially()
    {
        Queue<int> queue = new Queue<int>();         // Fila BFS
        bool[] visited = new bool[countries.Length]; // Marca países já visitados

        queue.Enqueue(startIndex);    // Enfileira país inicial
        visited[startIndex] = true;   // Marca como visitado

        while (queue.Count > 0)
        {
            int current = queue.Dequeue(); // Retira o país atual da fila

            countries[current].Infect();   // Infecta o país atual
            yield return new WaitForSeconds(infectionDelay); // Espera para ver a infecção

            // Enfileira vizinhos diretos não visitados
            for (int neighbor = 0; neighbor < countries.Length; neighbor++)
            {
                if (adjMatrix[current, neighbor] == 1 && !visited[neighbor])
                {
                    visited[neighbor] = true; // Marca vizinho como visitado
                    queue.Enqueue(neighbor);  // Adiciona vizinho à fila
                }
            }
        }
    }
}
