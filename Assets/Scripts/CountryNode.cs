using UnityEngine;

public class CountryNode : MonoBehaviour
{
    [Header("Configura��o do Pa�s")]
    public string countryName;       // Nome do pa�s
    public bool infected = false;    // Estado de infec��o
    private SpriteRenderer sr;       // Para alterar cor no mapa

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    // Marca o pa�s como infectado
    public void Infect()
    {
        infected = true;
        UpdateColor();
    }

    // Atualiza cor do pa�s (infectado ou n�o)
    private void UpdateColor()
    {
        if (sr != null)
        {
            sr.color = infected ? Color.red : Color.white;
            Debug.Log(countryName + " Foi infectado!");
        }
    }
}