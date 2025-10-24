using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sword : MonoBehaviour
{
    public UnityEvent<Enemy> OnEnemyTouched = new UnityEvent<Enemy>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if (enemy != null)
                OnEnemyTouched.Invoke(enemy);
        }
    }
}
