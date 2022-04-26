using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PoderAstrofisico : PoderPlanetas
{

    [SerializeField] GameObject blackHole;

    List<GameObject> mis_BalckHoles = new List<GameObject>();

    bool preparado_para_instanciar = false;

    private void Awake()
    {
        EventManager.StartListening("ClickCasilla", Crear_BlackHole);
    }

    public override void InitialAction(bool sin_pasar_turno = false, int[] vector = null)
    {
        base.InitialAction(true, vector);

        List<Casilla> planetas = FiltroCasillas.CasillasDeUnJugador(faccion);

        if (vector == null)// Cuando el vector es nulo se hace de manera aleatoria 
        {
            foreach (Casilla c in planetas)
            {
                List<Casilla> posibles = new List<Casilla>();
                foreach (Casilla cc in c.adyacentes)
                {
                    if (cc) posibles.Add(cc);
                }
                posibles = FiltroCasillas.CasillasSinMeteorito(FiltroCasillas.CasillasLibres(posibles));
                int rnd = Random.Range(0, posibles.Count);
                GameObject thisPieza;
                if (PhotonNetwork.InRoom)
                {
                    thisPieza = PhotonNetwork.Instantiate(padre.opcionesIniciales[2].name, posibles[rnd].transform.position, Quaternion.identity);
                }
                else
                {
                    thisPieza = Instantiate(padre.opcionesIniciales[2], posibles[rnd].transform.position, Quaternion.identity);
                }
                thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
                thisPieza.GetComponent<Pieza>().Colocar(posibles[rnd]);
                posibles[rnd].pieza = thisPieza.GetComponent<Pieza>();
            }
        }
        else //cuando el vector no lo es
        {
            Debug.Log("ASTRO " + vector[4]);
            if(vector[4] == 1) // El heroe astrofisico debe tener handicap (dos laboratirios deben estar contiguos)
            {
                Casilla casillaPlaneta1 = null;
                Casilla casillaPlaneta2 = null;

                for(int i =0; i < planetas.Count && (casillaPlaneta1 == null && casillaPlaneta2 == null); ++i) //Primero debemos buscar los planetas contiguos
                {
                    List<Casilla> casillasAComprobar = new List<Casilla>();
                    casillasAComprobar = FiltroCasillas.CasillasAdyacentes(planetas[i], true);
                    casillasAComprobar = FiltroCasillas.CasillasAdyacentes(casillasAComprobar, true);
                    casillasAComprobar = FiltroCasillas.CasillasPlaneta(casillasAComprobar);
                    casillasAComprobar.Remove(planetas[i]);

                    if (casillasAComprobar.Count > 0)
                    {

                        casillaPlaneta1 = planetas[i];
                        casillaPlaneta2 = casillasAComprobar[0];
                    }
                }

                Casilla casillaReferencia = null; //Variable para guardar la casilla donde se va a encontrar el primer laboratorio de los contiguos
                for (int i = 0; i < planetas.Count; ++i) //Una vez encontrados intanciamos los laberintos
                {


                    List<Casilla> posibles = new List<Casilla>();

                    if(i == planetas.IndexOf(casillaPlaneta1))  //Utilizamos el index of para saber cuales tienen que ir juntos de toda la lista
                    {                                           //Estos dos if podrian hacer con el iterador i pero creo que es mas claro si utilizo la referenceia a la anterior casilla
                    
                        posibles = FiltroCasillas.CasillasAdyacentes(casillaPlaneta2, true);
                        posibles = FiltroCasillas.CasillasAdyacentes(posibles, false);

                        posibles = FiltroCasillas.Interseccion(posibles, FiltroCasillas.CasillasAdyacentes(casillaPlaneta1, true));


                    }
                    else if(i == planetas.IndexOf(casillaPlaneta2))
                    {
                        

                        posibles = FiltroCasillas.CasillasAdyacentes(casillaReferencia, true);

                        posibles = FiltroCasillas.Interseccion(posibles, FiltroCasillas.CasillasAdyacentes(casillaPlaneta2, true));
                        
                    }
                    else
                    {
                        posibles = FiltroCasillas.CasillasAdyacentes(planetas[i], true);
                    }

                    posibles = FiltroCasillas.CasillasLibres(posibles);

                    //instanciacion de la pieza
                    int rnd = Random.Range(0, posibles.Count);
                    if (i == planetas.IndexOf(casillaPlaneta1)) 
                    { 
                        
                        casillaReferencia = posibles[rnd];

                    } 

                    GameObject thisPieza;
                    if (PhotonNetwork.InRoom)
                    {
                        thisPieza = PhotonNetwork.Instantiate(padre.opcionesIniciales[2].name, posibles[rnd].transform.position, Quaternion.identity);
                    }
                    else
                    {
                        thisPieza = Instantiate(padre.opcionesIniciales[2], posibles[rnd].transform.position, Quaternion.identity);
                    }
                    thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
                    thisPieza.GetComponent<Pieza>().Colocar(posibles[rnd]);
                    posibles[rnd].pieza = thisPieza.GetComponent<Pieza>();
                }

            }
            else //El heroe no debe tener handicap lo que significa que cada laboratorio no debe estar cerca de nigun otro 
            {

                List<Casilla> laboratoriosPrevios = new List<Casilla>();

                foreach(Casilla c in planetas)
                {
                    List<Casilla> posibles = new List<Casilla>();

                    posibles = FiltroCasillas.CasillasAdyacentes(c, true);
                    if (laboratoriosPrevios.Count > 0) posibles = FiltroCasillas.RestaLista(posibles, FiltroCasillas.CasillasAdyacentes( laboratoriosPrevios, true));

                    posibles = FiltroCasillas.CasillasLibres(posibles);

                    int rnd = Random.Range(0, posibles.Count);
                    GameObject thisPieza;
                    if (PhotonNetwork.InRoom)
                    {
                        thisPieza = PhotonNetwork.Instantiate(padre.opcionesIniciales[2].name, posibles[rnd].transform.position, Quaternion.identity);
                    }
                    else
                    {
                        thisPieza = Instantiate(padre.opcionesIniciales[2], posibles[rnd].transform.position, Quaternion.identity);
                    }
                    thisPieza.GetComponent<Pieza>().Set_Pieza_Extra();
                    thisPieza.GetComponent<Pieza>().Colocar(posibles[rnd]);
                    posibles[rnd].pieza = thisPieza.GetComponent<Pieza>();
                    laboratoriosPrevios.Add(posibles[rnd]);

                }

            }
        }
        if (!sin_pasar_turno)
        {
            Debug.Log("poder astrofisico no paso paso");
            EventManager.TriggerEvent("AccionTerminadaConjunta");
        }
    }



    public override void FirstActionPersonal()
    {

        if (!gameObject.GetPhotonView().IsMine && PhotonNetwork.InRoom) return;

        StartCoroutine(CFirstActionPersonal());
    }

    IEnumerator CFirstActionPersonal()
    {

        //if (mis_BalckHoles.Count > 0) yield return StartCoroutine(Activar(mis_BalckHoles[0].GetComponent<Pieza>().casilla));

        List<Casilla> posibles_lugares = blackHole.GetComponent<Pieza>().CasillasDisponibles();
        if (posibles_lugares.Count == 0)
        {
            Debug.Log("no hay posibles huecos para el astro fisics");
            EventManager.TriggerEvent("AccionTerminadaConjunta");
        }
        else
        {
            Tablero.instance.ResetCasillasEfects();
            foreach (Casilla casilla in posibles_lugares) casilla.SetState(States.select);

            preparado_para_instanciar = true;
        }

        yield return null;
    }

    public void Crear_BlackHole()
    {
        if (!preparado_para_instanciar) return;

        Casilla c = ClickCasillas.casillaClick;

        List<Casilla> posibles_lugares = blackHole.GetComponent<Pieza>().CasillasDisponibles();

        if (posibles_lugares.Contains(c))
        {
            GameObject this_pieza;
            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                this_pieza = PhotonNetwork.Instantiate(blackHole.name, c.transform.position, Quaternion.identity);
            }
            else
            {
                this_pieza = Instantiate(blackHole, c.transform.position, Quaternion.identity);
            }
            this_pieza.GetComponent<Pieza>().Set_Pieza_Extra();
            this_pieza.GetComponent<Pieza>().Colocar(c);

            mis_BalckHoles.Add(this_pieza);

            preparado_para_instanciar = false;
            Tablero.instance.ResetCasillasEfects();

           StartCoroutine( Activar(c, true));
        }
    }

    IEnumerator Activar(Casilla origen, bool last = false)
    {

        for (int j = 0; j < origen.adyacentes.Length; ++j)
        {
            if (origen.adyacentes[j] && origen.adyacentes[j].pieza && !origen.adyacentes[j].pieza.astro)
            {
                OnlineManager.instance.Destroy_This_Pieza(origen.adyacentes[j].pieza);
            }
            //Atraer_Todo_En_Una_Direccion(origen.adyacentes[j], j);
        }
        yield return new WaitForSeconds(1.5f);

        if (last)
        {
            Debug.Log("last poder astro fisico");
            EventManager.TriggerEvent("AccionTerminadaConjunta");
        }
        
    }
    public override void SecondAction()
    {
        FirstActionPersonal();
    }

    public void Atraer_Todo_En_Una_Direccion(Casilla c, int direccion)
    {
        if (!c) return;
        if (c.pieza)
        {
            Debug.Log("Pieza a mover: " + c.pieza);
            if (c.pieza.astro) return;
            int aux_reverseDirection;
            if (direccion < 3) aux_reverseDirection = direccion + 3;
            else aux_reverseDirection = direccion - 3;

            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                if (c.pieza.gameObject.GetPhotonView().IsMine)
                {
                    c.pieza.Set_Pieza_Extra();
                    c.pieza.transform.position = c.adyacentes[aux_reverseDirection].transform.position;
                    c.pieza.Colocar(c.adyacentes[aux_reverseDirection]);
                    c.Limpiar_Pieza(c.pieza);
                }
                else
                {
                    int i = Tablero.instance.Get_Numero_Casilla(c.gameObject);
                    int j = Tablero.instance.Get_Numero_Casilla(c.adyacentes[aux_reverseDirection].gameObject);
                    base.photonView.RPC("RPC_Move_FromC_ToC2", RpcTarget.Others, i, j, true);

                }
            }
            else
            {
                c.pieza.Set_Pieza_Extra();

                c.pieza.transform.position = c.adyacentes[aux_reverseDirection].transform.position;
            }
        }
        if (c.adyacentes[direccion])
        {
            Atraer_Todo_En_Una_Direccion(c.adyacentes[direccion], direccion);
        }
    }
}
