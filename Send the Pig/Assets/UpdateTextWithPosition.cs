using System;
using UnityEngine;
using UnityEngine.UI;
using HutongGames.PlayMaker;
 
public class UpdateTextWithPosition : MonoBehaviour
{
   
    public Transform target;
    public Text text;
    public Text text_max;
    public PlayMakerFSM directFSM;
   
    public float pigHeight_value;
    public string pigHeight_text;
    public float pigHeight_maxvalue;

    private FsmBool collisionDetected;
    private FsmFloat maxRecord;
    private FsmBool chuteDeployed;

    private float lastHeight;
    private bool firstLoad = true;
   
           	
    void Update()
    {
        collisionDetected = directFSM.FsmVariables.GetFsmBool("collisionDetected");
        chuteDeployed = directFSM.FsmVariables.GetFsmBool("chuteDeployed");
        maxRecord = directFSM.FsmVariables.GetFsmFloat("maxRecord");

        if (firstLoad == true)
        {
            pigHeight_maxvalue = maxRecord.Value;
            firstLoad = false;
        }

        if (chuteDeployed.Value == false)
        {

        
        pigHeight_text = target.position.y.ToString();
        pigHeight_value = float.Parse(pigHeight_text);
        float truncated = (float)(Math.Truncate((double)pigHeight_value*100.0) / 100.0);

        //collisionDetected = directFSM.FsmVariables.GetFsmBool("collisionDetected");
        //chuteDeployed = directFSM.FsmVariables.GetFsmBool("chuteDeployed");
                
        if (truncated > pigHeight_maxvalue && collisionDetected.Value == false && truncated > 2)
        {
      
            text_max.text = truncated.ToString();
            text_max.color = Color.red; 
            pigHeight_maxvalue = truncated;            
        }

        else if(truncated < lastHeight && collisionDetected.Value == false && truncated > 2)
        {
            maxRecord.Value = lastHeight;
            text_max.color = Color.green;
            chuteDeployed = true;
            directFSM.Fsm.Event("HitMaxHeight"); 
        }

        text.text = truncated.ToString();
        lastHeight = truncated;

        //For reference later 
        //text.text = target.position.y.ToString() + "m";
        //text.text = target.position.ToString();

        }

    }
   
}
