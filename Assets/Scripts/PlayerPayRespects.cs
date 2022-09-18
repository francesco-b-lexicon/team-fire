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
    public GameObject scarf;

    // Start is called before the first frame update
    void Start()
    {
        ui ??= FindObjectOfType<UIDocument>();
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
                    var frankPosition = frankWithScarf.transform.position;
                    Instantiate(frankNoScarf, frankPosition, Quaternion.identity);
                    Destroy(frankWithScarf);
                    canPayRespects = false;

                    var _scarf = Instantiate(scarf);
                    _scarf.transform.parent = gameObject.transform;
                    _scarf.transform.localPosition = new Vector3(-0.53f, 0.48f, 0.02f);
                    _scarf.transform.localScale = new Vector3(1, 1, 1);
                    _scarf.transform.localRotation = Quaternion.Euler(0, 0, -28.87f);
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
            respectsBar.RemoveFromClassList("hidden");
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name == "FrankWithScarf")
        {
            canPayRespects = false;
            StartCoroutine(HideRespectsBar());
        }
    }

    private IEnumerator HideRespectsBar()
    {
        yield return new WaitForSeconds(1.5f);

        respectsBar.AddToClassList("hidden");
    }
}
