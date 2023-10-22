using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ThrowableObject : MonoBehaviour
{
    public GameObject target;
    public TextMesh scoreText;

    bool isThrown = false;
    int roundedScore = 0;

    float score = 0;

    void Start()
    {
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();
        if (interactable)
        {
            interactable.onSelectEntered.AddListener(OnSelect);
            interactable.onSelectExited.AddListener(OnThrown);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target") && isThrown)
        {
            score += roundedScore;
            scoreText.text = "Score: " + score.ToString();
            isThrown = false;
        }
    }

    public void OnSelect(XRBaseInteractor interactor)
    {
        isThrown = false;
    }

    public void OnThrown(XRBaseInteractor interactor)
    {
        isThrown = true;
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        float score = Mathf.Min(distanceToTarget*10, 100);
        roundedScore = Mathf.RoundToInt(score);
    }
}
