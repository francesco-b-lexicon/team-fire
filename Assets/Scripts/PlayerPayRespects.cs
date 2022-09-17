using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerPayRespects : MonoBehaviour
{
    public UIDocument ui;
    ProgressBar respectsBar;
    bool canPayRespects = false;
    public float currentRespectsPaid = 0f;
    public float totalRespectsToPay = 100f;
    public float respectsIncreaseUnit = 5f;
    public float respectsIncreaseRate = .1f;
    float lastRespectsPaid;

    public GameObject frankWithScarf;
    public GameObject frankNoScarf;

    // Start is called before the first frame update
    void Start()
    {
        respectsBar = ui.rootVisualElement.Query<ProgressBar>("Respects-Bar");
    }

    // Update is called once per frame
    void Update()
    {
        respectsBar.value = currentRespectsPaid;
        if (Time.time >= lastRespectsPaid + respectsIncreaseRate)
        {
            if (canPayRespects && Input.GetKey(KeyCode.F))
            {
                currentRespectsPaid = Mathf.Min(currentRespectsPaid + respectsIncreaseUnit, totalRespectsToPay);

                if (currentRespectsPaid == totalRespectsToPay)
                {
                    var frankPosition = frankNoScarf.transform.position;
                    Destroy(frankWithScarf);
                    Instantiate(frankNoScarf, frankPosition, Quaternion.identity);
                    canPayRespects = false;
                }
            }
            else
            {
                currentRespectsPaid = Mathf.Max(0, currentRespectsPaid -= respectsIncreaseUnit);
            }
            lastRespectsPaid = Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "FrankWithScarf")
        {
            canPayRespects = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name == "FrankWithScarf")
        {
            canPayRespects = false;
        }
    }
}
