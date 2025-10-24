using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sword : MonoBehaviour
{
    public UnityEvent<Enemy> OnEnemyTouched = new UnityEvent<Enemy>();

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
