using UnityEngine;

public class CountryNode : MonoBehaviour
{
    [Header("Configuração do País")]
    public string countryName;       // Nome do país
    public bool infected = false;    // Estado de infecção
    private SpriteRenderer sr;       // Para alterar cor no mapa

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    // Marca o país como infectado
    public void Infect()
    {
        infected = true;
        UpdateColor();
    }

    // Atualiza cor do país (infectado ou não)
    private void UpdateColor()
    {
        if (sr != null)
        {
            sr.color = infected ? Color.red : Color.white;
            Debug.Log(countryName + " Foi infectado!");
        }
    }
}