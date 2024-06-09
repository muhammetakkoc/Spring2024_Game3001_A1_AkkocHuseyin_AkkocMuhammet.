using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;


public enum Steering
{
    AngularSeek,
    LinearSeek
}


public class Seek : MonoBehaviour
{
    public Steering mode;
    public TMP_Text modeTxt;
    Rigidbody2D rb2;
    float speed = 10f;


    void Start()
    {

        rb2 = GetComponent<Rigidbody2D>();
        //modeTxt.text = " Angular Seek";
    }


    // Scene Change
    public void ChangeSceneStart()
    {
        SceneManager.LoadScene(0);
        //Start Scene
    }
    public void ChangeSceneRestart()
    {
        SceneManager.LoadScene(1);
        // Play Scene
    }
    public void ChangeScenePlay()
    {
        SceneManager.LoadScene(1);
        //Play Scene
    }


    void Update()
    {

        // Mode Change

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("pressed 1, Angular Seek");
            modeTxt.text = "Mode: Angular Seek";
            mode = Steering.AngularSeek;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            modeTxt.text = "Mode: Linear  Seek";
            Debug.Log("pressed 2, Linear Seek");
            mode = Steering.LinearSeek;
        }

        if (mode == Steering.AngularSeek)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0.0f;

            Vector3 currentVelocity = rb2.velocity;
            Vector3 desiredVelocity = (mouse - transform.position).normalized * speed;
            Vector3 seekForce = desiredVelocity - currentVelocity;

            rb2.AddForce(seekForce);
        }
        else if (mode == Steering.LinearSeek)
        {
            Vector3 mouseA = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseA.z = 0f;
            mouseA = new Vector3(mouseA.x, mouseA.y, 0.0f);

            transform.position = Vector3.MoveTowards(transform.position, mouseA, speed * Time.deltaTime);
        }



        //Vector3 A = transform.position;
        //Vector3 B = mouse;
        //Vector3 AB = B - A;
        //Vector3 unitAB = AB.normalized;
        //Vector3 vAB = unitAB * speed;

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Obstacle")
        {
            SceneManager.LoadScene(2);
        }
    }

}
