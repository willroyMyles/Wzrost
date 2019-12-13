using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum InteractableType
{
    Default,
    Flag,
    Attack,
    Defense
}


public class InteractableBase : MonoBehaviour
{

    #region variables
    public InteractableType type;
    bool interactable = true;
    float distance = 1.5f;
    float currentDistance;
    public Canvas canvasPrefab;
    Canvas canvas;
    CanvasGroup group;
    Image image;
    TMPro.TextMeshProUGUI text;

    float maxDistance = 1;
    bool shouldShowCanves = false;
    private bool isPickedUp = false;
    public string name;

    internal bool IsPickedUp { get => isPickedUp; set => isPickedUp = value; }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Interactable";
        canvas = Instantiate(canvasPrefab, gameObject.transform.position, Quaternion.identity);
        canvas.transform.parent = gameObject.transform;
        if (canvas.worldCamera == null)
        {
            canvas.worldCamera = Camera.allCameras[0];
        }
        image = canvas.GetComponentInChildren<Image>();
        group = canvas.GetComponent<CanvasGroup>();
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        text.text = name;
    }

    public void setIsPickedUp(bool val)
    {

            IsPickedUp = val;
            group.alpha = 0;
   
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldShowCanves && !IsPickedUp)
        {
            //updates alpha
            group.alpha = Mathf.Lerp(.9f, .1f, currentDistance / maxDistance);

            //update position
            var screenpos = Camera.allCameras[0].WorldToScreenPoint(gameObject.transform.position);
            var point = new Vector2();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenpos, Camera.allCameras[0], out point);
            var adjustment = new Vector3(point.x, point.y-30f, 100f);

            image.transform.position = canvas.transform.TransformPoint(adjustment);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactor")
        {
            //show controls. auto pickup for now;
            maxDistance = Vector3.Distance(other.transform.position, transform.position);
            shouldShowCanves = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactor")
        {
            //show controls. auto pickup for now;
            shouldShowCanves = false;
            group.alpha = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Interactor" || other.gameObject.tag == "Enemy")
        {
            currentDistance = Vector3.Distance(gameObject.transform.position, other.gameObject.transform.position);
            if(currentDistance < this.distance)
            {
                other.gameObject.GetComponent<PlayerBase>().PickUp(gameObject);
            }

        }
    }
}
