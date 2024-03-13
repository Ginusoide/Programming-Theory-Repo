using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] ParticleSystem badChestParticle;
    [SerializeField] ParticleSystem goodChestParticle;
    [SerializeField] AudioClip clipGood;
    [SerializeField] AudioClip clipBad;
    [SerializeField] AudioClip clipBorder;
    [SerializeField] float volumeClipBorder = 0.15f;
    private AudioSource audioPlayer;
    private GameManager gameManager;
    private float horizontalAxis;
    private float xBound = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioPlayer = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Limita lo spostamento massimo sui bordi
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }

        // Muovi il canestro solo in orizzontale
        horizontalAxis = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalAxis);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bad"))
        {
            //gameManager.UpdateScore(-1);
            badChestParticle.Play();
            audioPlayer.PlayOneShot(clipBad);
        }
        else
        {
            //gameManager.UpdateScore(1);
            goodChestParticle.Play();
            audioPlayer.PlayOneShot(clipGood);
        }

        // Aggiorna il punteggio con Score
        gameManager.UpdateScore(other.gameObject.GetComponent<Ball>().Score);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //audioPlayer.clip = clipBorder; PlayOneShot non interrompe i suoni già in riproduzione!
        audioPlayer.PlayOneShot(clipBorder, volumeClipBorder);
    }
}
