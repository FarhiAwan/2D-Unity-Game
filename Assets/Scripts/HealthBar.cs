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

        // ✅ Reset health to full every time
        Health.totalHealth = 1f;

        player = GameObject.Find("Player");

        // ✅ Always start green
        barImage.color = Color.green;
        SetSize(Health.totalHealth);
    }

    public void Damage(float damage){
        Health.totalHealth -= damage;

        if(Health.totalHealth <= 0f){
            Health.totalHealth = 0f;
            Die();
        }

        // ✅ Color changes based on health amount
        if(Health.totalHealth <= 0.3f){
            barImage.color = Color.red;
        } else if(Health.totalHealth <= 0.6f){
            barImage.color = Color.yellow; // ✅ yellow when medium health
        } else {
            barImage.color = Color.green;  // ✅ green when high health
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