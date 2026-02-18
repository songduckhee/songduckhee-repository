using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time -  lastCheckTime > checkRate) //레이가 0.05초마다 카메라 정중앙을 쏴주고
        {
            lastCheckTime = Time.time;

			Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
			{
				if (hit.collider.gameObject != curInteractGameObject)
				{
					curInteractGameObject = hit.collider.gameObject;
					curInteractable = hit.collider.GetComponent<IInteractable>(); //만약 뭔가 맞았다면 그 맞은 오브젝트의 interaction클래스를 넣어줌
					SetPromptText();
				}
			}
			else
			{
				curInteractGameObject = null;
				curInteractable = null;
				promptText.gameObject.SetActive(false);
			}
		}
       
    }
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract(); //여기서 e를 누른 아이템의 데이터를 플레이어에게 넘겨줌
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
