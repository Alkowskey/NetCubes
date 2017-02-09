using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawn_cubes : NetworkBehaviour {


    [SerializeField]
    private Camera Cam;

    [SerializeField]
    private LayerMask Mask;

    public GameObject[] cubeArr1;
    public GameObject[] cubeArr2;
    private int control = 0, control1 = 1;
    public float timeLeft = 5;
    private bool puste = false;
    public GameObject[] obstacles;
    public GameObject Cube_control;
    public GameObject Cube_control1;
    int to_samo, to_samo1 = 0;
    bool prawda1 = false;
    bool prawda2 = false;
    private int x, z;
    int number;
    int number1;
    // Use this for initialization
    void Start () {

        obstacles[8] = Instantiate(cubeArr1[0], new Vector3(-8, 2, -5.5f), Quaternion.identity);
        obstacles[7] = Instantiate(cubeArr2[0], new Vector3(-8, 2, 5.5f), Quaternion.identity);

        if (Cam == null)
        {
            Debug.LogError("Cam not found!");
            this.enabled = false;
        }
        losuj();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
                Destroy(obstacles[8]);
                Destroy(obstacles[7]);
                 Shoot();
        }
        if (puste == true)
        {
            obstacles[8] = Instantiate(cubeArr1[0], new Vector3(-8, 2, -5.5f), Quaternion.identity);
            obstacles[7] = Instantiate(cubeArr2[0], new Vector3(-8, 2, -5.5f), Quaternion.identity);
            puste = false;
        }
        if(number==x)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                Debug.Log("Wygral gracz nr 1");
                timeLeft = 5;
                Set_zero();
            }
        }
        if (number1 == z)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                Debug.Log("Wygral gracz nr 2");
                timeLeft = 5;
                Set_zero();
            }

        }

    }

    void Spawn_cube(int x, int z)
    {
        if (z != x)
        {
            obstacles[x] = Instantiate(cubeArr1[x], new Vector3(-8, 2, -5.5f), Quaternion.identity);
            obstacles[z] = Instantiate(cubeArr2[z], new Vector3(-8, 2, 5.5f), Quaternion.identity);
        }
        else
            Shoot();
    }
    //losuje kolor.
    void losuj()
    {
        while(true)
        {
            number = Random.Range(1, 5);
            number1 = Random.Range(1, 5);
            if (number != to_samo)
            {
                to_samo = number;
                Cube_control = Instantiate(cubeArr1[number], new Vector3(-8, 2, -8.5f), Quaternion.identity);
                prawda1 = true;
                
            }
            if (number1 != to_samo1)
            {
                to_samo1 = number1;
                Cube_control1 = Instantiate(cubeArr2[number1], new Vector3(-8, 2, 8.5f), Quaternion.identity);
                prawda2 = true;
            }
            if (prawda1 == true && prawda2 == true)
            {
                prawda2 = false;
                prawda1 = false;
                break;
            }
        }
    }
    //czysci gre
    void Set_zero()
    {
        Destroy(Cube_control);
        Destroy(Cube_control1);
        Destroy(obstacles[8]);
        Destroy(obstacles[7]);
        obstacles[8] = Instantiate(cubeArr1[0], new Vector3(-8, 2, -5.5f), Quaternion.identity);
        obstacles[7] = Instantiate(cubeArr2[0], new Vector3(-8, 2, 5.5f), Quaternion.identity);
        control = 0;
        control1 = 1;
        timeLeft = 5;
        Destroy(obstacles[x]);
        Destroy(obstacles[z]);
        losuj();
    }
    //======
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, 100, Mask))
        {
            Debug.Log("we hit: "+ hit.collider.name);
            Destroy(obstacles[x]);
            Destroy(obstacles[z]);
            x = Random.Range(1, 6);
            z = Random.Range(1, 6);
            timeLeft = 5;
            Spawn_cube(x, z);
            if (hit.collider.CompareTag("Klocek1"))
            {
                Debug.Log("Klocek1");
            }
            else
            {
                Debug.Log("Klocek2");

            }

        }
    }
}
