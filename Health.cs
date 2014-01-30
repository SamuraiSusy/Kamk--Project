using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    private float hp = 16.0f;

    public void TakeDamage(float amount)
    {
        hp -= amount;

        if(hp <= 0)
        {
            Debug.Log("Out of hit points = death");
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, Screen.width / 4, Screen.height / 10), hp.ToString() + " HP");
    }
}
