using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    public string[] tagsToCheck;
    public float speed, returnSpeed;
    public float range, stopRange;
    public float fixedHeight = 1.0f;

    [SerializeField] private AudioClip throwSoundClip;
    [SerializeField] private AudioClip backSoundClip;
    private AudioSource audioSource;

    [HideInInspector]
    public Transform caster;
    private LineRenderer line;
    private bool isReturning; // Zustand, ob die Hook zurückkehrt
    private Vector3 targetPosition;
    private Transform collidedWith; // Das getroffene Objekt, das mitgezogen wird


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        line = transform.Find("Line").GetComponent<LineRenderer>();
        SetMaxRangeTarget();
    }

    private void Update()
    {
        if (caster)
        {
            line.SetPosition(0, caster.position);
            line.SetPosition(1, transform.position);

            if (isReturning)
            {
                ReturnToCaster();
            }
            else
            {
                MoveTowardsTarget();
            }

            if (collidedWith)
            {
                collidedWith.position = transform.position; // Hält das getroffene Objekt an der Hook-Position
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetMaxRangeTarget()
    {

        // Berechnet die Zielposition basierend auf der maximalen Reichweite
        Vector3 direction = (caster.forward).normalized;
        targetPosition = caster.position + direction * range;
        targetPosition.y = fixedHeight; // Fixiert die Höhe der Zielposition

        audioSource.clip = throwSoundClip;
        audioSource.Play();
    }

    private void MoveTowardsTarget()
    {
        
        // Bewegt die Hook in Richtung der festgelegten maximalen Reichweite
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Prüft, ob die Hook die maximale Reichweite erreicht hat
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            StartReturn(); // Beginne die Rückkehr zur caster Position
        }
    }

    private void ReturnToCaster()
    {

        audioSource.clip = backSoundClip;
        audioSource.Play();
        // Richtet die Hook auf den Caster aus
        Vector3 returnDirection = (caster.position - transform.position).normalized;
        transform.Translate(returnDirection * returnSpeed * Time.deltaTime, Space.World);

        // Zerstört die Hook, wenn sie nahe genug beim Caster ist
        if (Vector3.Distance(transform.position, caster.position) < stopRange)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Überprüft, ob das getroffene Objekt den Tag "item" hat
        if (!isReturning && other.gameObject.tag == "Enemy")
        {
            collidedWith = other.transform; // Setzt das getroffene Objekt zum Mitziehen
            StartReturn(); // Startet sofort den Rückkehrmodus
        }
    }


    private void StartReturn()
    {
        isReturning = true; // Setzt den Rückkehrmodus
    }
}
