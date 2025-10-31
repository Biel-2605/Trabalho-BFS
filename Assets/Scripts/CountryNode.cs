using UnityEngine;

public class CountryNode : MonoBehaviour
{
    [Header("Configuração do País")]
    public string countryName;       // Nome do país (aparece no Inspector)
    public bool infected = false;    // Indica se o país está infectado

    [Header("Cores")]
    public Color healthyColor = Color.green;  // Cor quando saudável
    public Color infectedColor = Color.red;   // Cor quando infectado

    private SpriteRenderer sr; // Componente visual do país

    void Awake()
    {
        // Tenta pegar o SpriteRenderer do próprio objeto ou filhos
        sr = GetComponentInChildren<SpriteRenderer>();

        // Atualiza a cor inicial de acordo com o estado (infectado ou saudável)
        UpdateColor();
    }

    // Método que marca o país como infectado
    public void Infect()
    {
        if (infected) return; // Se já estiver infectado, não faz nada
        infected = true;      // Marca como infectado
        UpdateColor();        // Atualiza a cor para vermelha
    }

    // Atualiza a cor do país com base no estado de infecção
    private void UpdateColor()
    {
        if (sr != null)
            sr.color = infected ? infectedColor : healthyColor;
    }
}
