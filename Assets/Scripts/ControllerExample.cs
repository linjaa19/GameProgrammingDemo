using UnityEngine;

public class ControllerExample : MonoBehaviour
{
    public GameObject hookOBJ;
    public float hookCooldown = 3.0f; // Cooldown in Sekunden

    private Vector3 lookPos;
    private Vector3 lookDir;
    private GameObject hook;
    private float lastHookTime = -Mathf.Infinity; // Zeitpunkt des letzten Schusses

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            lookPos = hit.point;
        }

        lookDir = lookPos - transform.position;
        lookDir.y = 0;
        transform.LookAt(transform.position + lookDir, Vector3.up);

        // Hook nur dann abfeuern, wenn der Cooldown abgelaufen ist
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= lastHookTime + hookCooldown)
        {
            LaunchHook();
            lastHookTime = Time.time; // Aktualisiert den Zeitpunkt des letzten Schusses
        }
    }

    void LaunchHook()
    {
        Vector3 forward = lookDir.normalized;
        forward.y = 0;
        Vector3 spawnPosition = transform.position + forward * 2f;
        spawnPosition.y = hookOBJ.GetComponent<HookScript>().fixedHeight; // Setzt die Höhe auf die fixierte Höhe der Hook

        if (hook != null)
        {
            Destroy(hook); // Löscht die alte Hook
        }

        hook = Instantiate(hookOBJ, spawnPosition, Quaternion.LookRotation(forward));
        hook.GetComponent<HookScript>().caster = transform;
    }
}
