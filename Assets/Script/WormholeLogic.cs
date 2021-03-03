using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WormholeLogic : MonoBehaviour
{
    [Header ("Refill and discharge rates and objects for bars")]
    [SerializeField] float maxValue = 100f;
    [SerializeField] float damagePerTick = 2f;
    [SerializeField] float fillPerTick = 2f;
    [SerializeField] float waitToFill = 2f;
    float currentStatusOfSuckerBar;
    float currentStatusOfPulserBar;
    [SerializeField] Slider suckerSlider;
    [SerializeField] Slider pulserSlider;
    [Header ("Wormhole configs")]
    [SerializeField] GameObject rightWormHoleReciever;
    [SerializeField] GameObject leftWormHoleReciever;
    [SerializeField] GameObject wormHolePulser;
    [SerializeField] float distanceForX = 2f;
    Camera cam;
    Vector2 mousePos;
    Vector2 holePosDifForX;
    Vector2 playerPos;
    Animator animator;
    bool isUsingSucker = false;
    bool isUsingPulser = false;

    void Start()
    {
        currentStatusOfPulserBar = maxValue;
        currentStatusOfSuckerBar = maxValue;
        cam = Camera.main;
        animator = GetComponent<Animator>();
        holePosDifForX = new Vector2(distanceForX, 0f);
        SetMaxPulser(maxValue);
        SetMaxSucker(maxValue);
    }  
    void Update()
    {
        playerPos = new Vector2(transform.position.x, transform.position.y);
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Reciever();
        Pulser();
        if(isUsingPulser && currentStatusOfPulserBar > 0)
        {
            PulserGoingDownPerTick();
        }
        if(!isUsingPulser && currentStatusOfPulserBar < maxValue)
        {
            PulserGoingUp();
        }
        if (FindObjectOfType<RightRecieverLogic>() || FindObjectOfType<LeftRecieverLogic>())
        {
            if (currentStatusOfSuckerBar > 0)
            {
                SuckerGoingDownPerTick();
            }
        }
        if (!isUsingSucker && currentStatusOfSuckerBar < maxValue)
        {
            if(!FindObjectOfType<RightRecieverLogic>() && !FindObjectOfType<LeftRecieverLogic>())
            {
                SuckerGoingUp();
            }       
        }       
    }
    private void Reciever()
    {
        Vector2 rightHolePos = playerPos + holePosDifForX;
        Vector2 leftHolePos = playerPos - holePosDifForX;
        if (Input.GetButtonDown("Fire2"))
            {
            if (currentStatusOfSuckerBar > 0)
            {
                 Instantiate(rightWormHoleReciever, rightHolePos, Quaternion.identity);
                 isUsingSucker = true;
            }
            }
        if (Input.GetButtonDown("Fire1"))
            {
            if (currentStatusOfSuckerBar > 0)
            {
                Instantiate(leftWormHoleReciever, leftHolePos, Quaternion.identity);
                isUsingSucker = true;
            }
        }
        if (Input.GetButtonUp("Fire2") && Input.GetButtonUp("Fire1"))
        {
            isUsingSucker = false;
        }
        if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Fire2"))
        {
            isUsingSucker = false;
        }


        //Animator Events
        if(Input.GetButton("Fire1") && Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("BothPowers");
        }
        if (Input.GetButton("Fire2") && Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("BothPowers");
        }
        if (Input.GetButtonDown("Fire2") && Input.GetButton("Fire1"))
        {
            animator.SetTrigger("BothReciever");
        }
    }
    private void Pulser()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(currentStatusOfPulserBar > 0)
            {
                Instantiate(wormHolePulser, mousePos, Quaternion.identity);
                isUsingPulser = true;
            }      
        }   
        if (Input.GetKeyUp(KeyCode.W))
        {
            isUsingPulser = false;
        }
    }

    void PulserGoingDownPerTick()
    {
        currentStatusOfPulserBar -= Time.deltaTime * damagePerTick;
        SetPulserValue(currentStatusOfPulserBar);

    }
    void SuckerGoingDownPerTick()
    {
        currentStatusOfSuckerBar -= Time.deltaTime * damagePerTick;
        SetSuckerValue(currentStatusOfSuckerBar);
    }

    void PulserGoingUp()
    {
        if(currentStatusOfPulserBar < maxValue)
        {
            SetPulserValue(currentStatusOfPulserBar);
            StartCoroutine(WaitForPulserFill());
        } else { return; }
    }
    IEnumerator WaitForPulserFill()
    {
        yield return new WaitForSeconds(waitToFill);
        currentStatusOfPulserBar += Time.deltaTime * fillPerTick;
        SetPulserValue(currentStatusOfPulserBar);
    }
    void SuckerGoingUp()
    {       
        if(currentStatusOfSuckerBar < maxValue)
        {
            SetSuckerValue(currentStatusOfSuckerBar);
            StartCoroutine(WaitForSuckerFill());

        }
    }
    IEnumerator WaitForSuckerFill()
    {
        yield return new WaitForSeconds(waitToFill);
        currentStatusOfSuckerBar += Time.deltaTime * fillPerTick;
        SetSuckerValue(currentStatusOfSuckerBar);
    }

    public void SetMaxSucker(float sucker)
    {
        suckerSlider.maxValue = sucker;
        suckerSlider.value = sucker;
    }
    public void SetMaxPulser(float pulser)
    {
        pulserSlider.maxValue = pulser;
        pulserSlider.value = pulser;
    }
    public void SetSuckerValue(float sucker)
    {
        suckerSlider.value = sucker;
    }
    public void SetPulserValue(float pulser)
    {
        pulserSlider.value = pulser;
    }
}
