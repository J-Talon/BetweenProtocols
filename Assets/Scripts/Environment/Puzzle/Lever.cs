using System;
using Common;
using Entity;
using EventSystem;
using TMPro;
using UnityEngine;

public class Lever : MonoBehaviour, Tagged
{
    
    [SerializeField] private LogicGateController controller;
    [SerializeField] private bool switchedOn;
    [SerializeField] private Sprite initialState;
    [SerializeField] private Sprite onState = null;
    [SerializeField] private string promptString = "Interact (E)";
    [SerializeField] private bool reusable = false;
    [SerializeField] private bool trigger = false;

    
    
    private SpriteRenderer spriteRenderer;
    
    
    
    
    private GameObject interactionPrompt;
    private TextMeshPro pro;
    
    private string leverId;
    private bool inRange;
    private bool spent;
    


    public bool isOn()
    {
        return switchedOn;
    }

    public string getId()
    {
        if (leverId == null)
            leverId = ((Tagged)this).createId();
        return leverId;

    }

    void Start()
    {
        
        EventManager.sceneChangeEvent.subscribe(onSceneSwitch);
        EventManager.interactionEvent.subscribe(playerInteractEvent);
        
        leverId = getId();
        inRange = false;
        
        interactionPrompt = gameObject.transform.GetChild(0).gameObject;
        pro = interactionPrompt.GetComponent<TextMeshPro>();
        pro.text = promptString;
        
        interactionPrompt.SetActive(false);
        controller.addLever(this);
        spent = false;
        
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            return;
        
        initialState = spriteRenderer.sprite;

    }

    void interact()
    {
        if (spent && !reusable)
            return;

        spent = true;
        
        if (!reusable)
            interactionPrompt.SetActive(false);

        if (trigger)
            switchedOn = true;
        else
            switchedOn = !switchedOn;

        controller.onLeverSwitched(this);

        if (spriteRenderer == null)
            return;
        
        if (switchedOn)
        {
            if (onState != null)
                spriteRenderer.sprite = onState;
        }
        else spriteRenderer.sprite = initialState;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null)
            return;
        inRange = true;
        
        if (!spent || reusable)
            interactionPrompt.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null)
            return;
        inRange = false;
        
        interactionPrompt.SetActive(false);
    }


    
    public void playerInteractEvent(float i)
    {
        if (inRange)
            interact();
    }


    public void onSceneSwitch(string s)
    {
        EventManager.sceneChangeEvent.unsubscribe(onSceneSwitch);
        EventManager.interactionEvent.unsubscribe(playerInteractEvent);
    }
}
