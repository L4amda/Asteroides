using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jugador : MonoBehaviour
{
    public float thrustForce = 100f;
    public float rotationSpeed = 120f;

    public GameObject gun, bulletPrefab;

    private Boolean pausa = false;


    public static int SCORE = 0;

    public static int MAXSCORE;

    private Rigidbody _rigid;
    // Start is called before the first frame update
    void Start()
    {
        _rigid= GetComponent<Rigidbody>();
        GameObject po = GameObject.FindGameObjectWithTag("PAUSA");
        po.GetComponent<Text>().text = "";
        GameObject po2 = GameObject.FindGameObjectWithTag("PAUSA2");
        po2.GetComponent<Text>().text = "";
        GameObject ro = GameObject.FindGameObjectWithTag("REINICIAR");
        ro.GetComponent<Text>().text = "";
    }
    
    // Update is called once per frame
    void Update()
    {
        if(SCORE > MAXSCORE)
        {
            MAXSCORE = SCORE;

            comprobarMaxScore();
        }

        GameObject ms = GameObject.FindGameObjectWithTag("MAXSCORE");
        ms.GetComponent<Text>().text = "MAX SCORE: " + Jugador.MAXSCORE;

        float thrust = Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;

        _rigid.AddForce(thrustDirection * thrust * thrustForce);

        float rotation = Input.GetAxis("Horizontal") * Time.deltaTime;

        transform.Rotate(Vector3.forward, -rotation *  rotationSpeed);

        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);

            Bala balaScript=  bullet.GetComponent<Bala>();

            balaScript.targetVector = transform.right;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            verPausa();
        }

        if(Input.GetKeyDown(KeyCode.R) && pausa)
        {
            Time.timeScale = 1f;
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        

    }

    private void OnTriggerEnter (Collider collision)
    {
        if(collision.gameObject.tag == "Enemy"){
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
       
       
    }

    private void verPausa()
    {
        if (pausa)
        {
            GameObject go = GameObject.FindGameObjectWithTag("UI");
            go.GetComponent<Text>().text = "Puntos: " + Jugador.SCORE;
            GameObject po = GameObject.FindGameObjectWithTag("PAUSA");
            po.GetComponent<Text>().text = "";
            GameObject po2 = GameObject.FindGameObjectWithTag("PAUSA2");
            po2.GetComponent<Text>().text = "";
            GameObject ro = GameObject.FindGameObjectWithTag("REINICIAR");
            ro.GetComponent<Text>().text = "";
            Time.timeScale = 1f;
        }
        else{
            GameObject go = GameObject.FindGameObjectWithTag("UI");
            go.GetComponent<Text>().text = "";
            GameObject po = GameObject.FindGameObjectWithTag("PAUSA");
            po.GetComponent<Text>().text = "PAUSA";
            GameObject po2 = GameObject.FindGameObjectWithTag("PAUSA2");
            po2.GetComponent<Text>().text = "pulse P para reanudar el juego";
            GameObject ro = GameObject.FindGameObjectWithTag("REINICIAR");
            ro.GetComponent<Text>().text = "pulse R para reiniciar el juego";
            Time.timeScale = 0f;
          
        }
        pausa = !pausa;
    }

    private void comprobarMaxScore()
    {
        if (SCORE >= 999)
        {
            MAXSCORE = 999;
        }
    }
}
