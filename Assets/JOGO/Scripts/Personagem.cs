using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Personagem: MonoBehaviour
{   [SerializeField]
    private Rigidbody meuRb;
    Vector3 inputMovimento;

    
    private VariableJoystick joystickMovimento;
    public float velocidade, velocidadeDeAtaque;

    public float raioDaVisao, anguloDaVisao;
    Vector3 anguloDeAtaque;
    public LayerMask maskInimigo, maskObstaculo;

    [HideInInspector]
    public List<Transform> alvosVisiveis = new List<Transform>();

    public GameObject alvoDeAtaque, projetil;

    public Transform porOndeVaiSairOTiro;
    float contadorParaAtirar;
    public int numeroDeInimigos = 0;

    public CinemachineVirtualCamera cameraPrincipal;


    void Start()
    {
        StartCoroutine("AcharAlvosMasComDelay", .2f);
        cameraPrincipal = FindObjectOfType<CinemachineVirtualCamera>();
        cameraPrincipal.Follow = transform;

    }
    void Awake()
    {
        meuRb = GetComponent<Rigidbody>();
        joystickMovimento = GameObject.FindWithTag("joystickMovimento").GetComponent<VariableJoystick>();
        GameObject.Find("Botao de Tiro").GetComponent<Button>().onClick.AddListener(Atirar);
        
    }
    void Update()
    {
        Movimentar();
        
    }

    private void FixedUpdate()
    {
        meuRb.MovePosition(transform.position + (new Vector3(inputMovimento.x, 0, inputMovimento.z) * velocidade * Time.deltaTime));

    }
    void Movimentar()
    {
        inputMovimento = new Vector3(joystickMovimento.Horizontal, 0, joystickMovimento.Vertical);
        transform.LookAt(transform.position + new Vector3(inputMovimento.x, 0, inputMovimento.z));
    }

    IEnumerator AcharAlvosMasComDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            AcharAlvosVisiveis();
        }
    }

    void AcharAlvosVisiveis()
    {
        alvosVisiveis.Clear();
        Collider[] alvosNoRaioDaVisao = Physics.OverlapSphere(transform.position, raioDaVisao, maskInimigo);
        
        float DistanciaDoinimigoMaisProximo = raioDaVisao;
        GameObject inimigoMaisProximo = null;
       


        for (int i = 0; i < alvosNoRaioDaVisao.Length; i++)
        {
            GameObject gameObjectDoAlvo = alvosNoRaioDaVisao[i].gameObject;
            if (gameObjectDoAlvo.tag != "Inimigo") continue;
            Transform alvo = gameObjectDoAlvo.transform;    

            Vector3 direcaoParaAlvo = (alvo.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, direcaoParaAlvo) < anguloDaVisao / 2)
            {
                float distanciaParaAlvo = Vector3.Distance(transform.position, alvo.position);

                if (!Physics.Raycast(transform.position, direcaoParaAlvo, distanciaParaAlvo, maskObstaculo))
                {
                    
                    alvosVisiveis.Add(alvo);

                    if(distanciaParaAlvo < DistanciaDoinimigoMaisProximo)
                    {
                        
                        DistanciaDoinimigoMaisProximo = distanciaParaAlvo;
                        anguloDeAtaque = direcaoParaAlvo;
                        inimigoMaisProximo = gameObjectDoAlvo;
                        
                    }
                  
                }
            }
            
        }
        if (inimigoMaisProximo != null) {

            numeroDeInimigos++;
            alvoDeAtaque = inimigoMaisProximo;
            Debug.DrawLine(transform.position, inimigoMaisProximo.transform.position, Color.black);

        }
        else
        {
            numeroDeInimigos = 0;
            alvoDeAtaque = null;
        }
       
    }
     public void Atirar()
    {
        
        if (numeroDeInimigos > 0)
        {
            transform.LookAt(alvoDeAtaque.transform.position);

            if (Vector3.Distance(transform.position, alvoDeAtaque.transform.position) <= raioDaVisao)
            {
                GameObject ataque = Instantiate(projetil, porOndeVaiSairOTiro.transform.position, Quaternion.identity);

                ataque.GetComponent<Rigidbody>().AddForce(anguloDeAtaque * velocidade, ForceMode.Impulse);
              

            }
        }
    }

    public Vector3 DirecaoDoAngulo(float anguloEmGraus,bool anguloEGlobal)
    {
        if (!anguloEGlobal)
        {
            anguloEmGraus += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(anguloEmGraus * Mathf.Deg2Rad), 0, Mathf.Cos(anguloEmGraus * Mathf.Deg2Rad));
    }

}
