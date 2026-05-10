using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    private RectTransform bar;
    private Image barImage;
    private GameObject player;

    void Start()
    {
        bar = GetComponent<RectTransform>();
        barImage = GetComponent<Image>();

<<<<<<< HEAD
=======
        // Reset health to full every time
>>>>>>> b4d9f2f13777b13e3ce5ecc18ee5fb1d9dfba1bb
        Health.totalHealth = 1f;

        player = GameObject.Find("Player");

<<<<<<< HEAD
=======
        //  Always start green
>>>>>>> b4d9f2f13777b13e3ce5ecc18ee5fb1d9dfba1bb
        barImage.color = Color.green;
        SetSize(Health.totalHealth);
    }

    public void Damage(float damage){
        Health.totalHealth -= damage;

        if(Health.totalHealth <= 0f){
            Health.totalHealth = 0f;
            Die();
        }

<<<<<<< HEAD
        if(Health.totalHealth <= 0.3f){
            barImage.color = Color.red;
        } else if(Health.totalHealth <= 0.6f){
            barImage.color = Color.yellow; 
        } else {
            barImage.color = Color.green;  
=======
        // Color changes based on health amount
        if(Health.totalHealth <= 0.3f){
            barImage.color = Color.red;
        } else if(Health.totalHealth <= 0.6f){
            barImage.color = Color.yellow; //  yellow when medium health
        } else {
            barImage.color = Color.green;  // green when high health
>>>>>>> b4d9f2f13777b13e3ce5ecc18ee5fb1d9dfba1bb
        }

        SetSize(Health.totalHealth);
    }

    private void Die(){
        Debug.Log("Player Died!");
        Destroy(player);
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetSize(float size){
        bar.localScale = new Vector3(size, 1f, 1f);
    }
}
