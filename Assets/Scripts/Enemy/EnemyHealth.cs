using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
	public int healthSpawningPercentage;
	public int bulletSpawningPercentage;
	public string enemyType;
	
    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    public bool isDead;
    bool isSinking;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
        currentHealth = startingHealth;
		healthSpawningPercentage = 15;
		bulletSpawningPercentage = 5;

		if( enemyType.Equals("Hellephant") ){
			healthSpawningPercentage *= 3;
			bulletSpawningPercentage *= 2;
		}
    }


    void Update ()
    {

		if(isSinking)
		{
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);

        }

    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }

    }

	public void TakeDamage(int amount){

		if(isDead)
			return;
		
		enemyAudio.Play ();
		
		currentHealth -= amount;
		
		if(currentHealth <= 0)
		{
			Death ();
		}
	}
	
	
	
	public void Death ()
	{
		isDead = true;
		
		
		capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent <NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;

        Destroy (gameObject, 2f);

		int random = Random.Range (0, 100);
		
		if(random <= healthSpawningPercentage )
			Instantiate (Resources.Load ("HealthIcon"), this.transform.position, this.transform.rotation);
		else if(random >= 100 - bulletSpawningPercentage)
			Instantiate (Resources.Load ("Bullet"), this.transform.position, this.transform.rotation);
    }
}
