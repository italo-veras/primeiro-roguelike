using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GeradorDeLevel2 : MonoBehaviour
{
    public GameObject player;
    public Transform transPlayer;
    public Transform fim;
    public float distancia, distanciaParaCoisa;

    Scene essaCena;

    public static GeradorDeLevel2 instace;

    
    private void Awake()
    {
        instace = this;
    }

    public int esseLevel = 0;
    private void Start()
    {
        SceneManager.LoadScene(esseLevel + 1, LoadSceneMode.Additive);
        
    }
    private void FixedUpdate()
    {
        fim = GameObject.Find("Fim").GetComponent<Transform>();
        transPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        distancia = Vector3.Distance(transPlayer.position, fim.position);

        

    }
    void CompletouLevel()
    {
        if (distancia < distanciaParaCoisa)
        {
            Debug.Log("alksdhkals");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
        }
        
    }

    /*private void Start()
    {
        GerarLevel();

        fim = GameObject.Find("Fim").GetComponent<Transform>();
        transPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    private void FixedUpdate()
    {
        distancia = Vector3.Distance(transPlayer.position, fim.position);

        if (distancia < distanciaParaCoisa)
        {
            Debug.Log("alksdhkals");
            GerenciarCena();
        }

    }

    void GerarLevel()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        SceneManager.LoadScene(1);
    }
    void GerenciarCena()
    {

        SceneManager.LoadScene(essaCena.buildIndex + Random.Range(1, 2));
    }*/
}
