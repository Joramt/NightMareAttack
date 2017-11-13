using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
	public Image healImage;
    public AudioClip deathClip;
    public float flashSpeed = 1f;
    public Color flashColourDamage = new Color(1f, 0f, 0f, 0.1f);
	public Color flashColourHeal = new Color(0f, 1f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;
	bool healed;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(damaged)
        {
			damageImage.color = flashColourDamage;
			damaged = false;
        }
		else if(healed){
			healImage.color = flashColourHeal;
			healed = false;
		}
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
			healImage.color = Color.Lerp (healImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }


        
    }

	public void GetHeal(int heal){

		healed = true;
	
		if(currentHealth >= startingHealth - heal){
			currentHealth = startingHealth;
		}
		else
			currentHealth += heal;
		
		healthSlider.value = currentHealth;
	}

    public void TakeDamage (int amount)
    {
	
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

		if(currentHealth <= 0 )
			healthSlider.value = -2;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        Application.LoadLevel (Application.loadedLevel);
    }
}
