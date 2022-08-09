
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
   [SerializeField]   private HealthEnemy playerHealth;
   [SerializeField]   private Image TotalHealthBar;
   [SerializeField]   private Image CurrentHealthBar;







    private void Start()
    {
        TotalHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void FixedUpdate()
    {
        CurrentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}

