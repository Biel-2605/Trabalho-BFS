using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionManager : MonoBehaviour
{
    // ======= CONFIGURAÇÃO NO INSPECTOR =======
    
    [Header("Países")]
    public CountryNode[] countries; // Lista de países (nós do grafo). Cada um deve ter o script CountryNode.

    [Header("Configuração da Infecção")]
    public int startIndex = 0; // Índice do país onde a infecção começa (ex: 0 = Brasil)
    public float infectionDelay = 1.5f; // Tempo entre cada “onda” de infecção (delay entre países)

    // ======= MATRIZ DE ADJACÊNCIA =======
    // Representa as conexões entre os países (grafo)
    // 1 = os países são vizinhos (conectados)
    // 0 = não são vizinhos

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

    // Flag para saber se a infecção já começou
    private bool infectionStarted = false;

    void Update()
    {
        // Ao apertar espaço, começa a infecção
        if (!infectionStarted && Input.GetKeyDown(KeyCode.Space))
        {
            infectionStarted = true;
            StartCoroutine(BFSInfection()); // Começa a propagação usando BFS
        }
    }

    // ======= ALGORITMO DE PROPAGAÇÃO =======
    // Usa BFS (Busca em Largura) para percorrer os países infectando por onda

    IEnumerator BFSInfection()
    {
        Queue<int> queue = new Queue<int>(); // Fila para controle de quais países serão infectados
        bool[] visited = new bool[countries.Length]; // Marca quais países já foram infectados

        // Começa a infecção no país inicial
        queue.Enqueue(startIndex);
        visited[startIndex] = true;
        countries[startIndex].Infect(); // Chama método de infecção no script do país

        // Enquanto houver países a infectar
        while (queue.Count > 0)
        {
            int current = queue.Dequeue(); // País atual a analisar

            // Percorre todos os países para verificar vizinhos
            for (int neighbor = 0; neighbor < countries.Length; neighbor++)
            {
                // Se for vizinho e ainda não tiver sido infectado
                if (adjMatrix[current, neighbor] == 1 && !visited[neighbor])
                {
                    visited[neighbor] = true; // Marca como infectado
                    countries[neighbor].Infect(); // Infecta o país
                    queue.Enqueue(neighbor); // Adiciona à fila para processar seus vizinhos

                    yield return new WaitForSeconds(infectionDelay); // Espera um tempo antes de continuar (simula onda)
                }
            }
        }
    }
}
