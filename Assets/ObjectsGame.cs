using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsGame : MonoBehaviour
{
    int x;
    int y;
    public string Color;
    public int Num;
    public void SetPos(int x, int y){
        this.x = x;
        this.y = y;
    }
    public bool isFirst = false;
    public bool isSecond = false;


    public void OnMouseDown()
    {
        if(!Field.isFirstEquip){
            isFirst = true;
            Field.isFirstEquip = true;
            Field.positions[0]= this.transform.position;
            Field.diamonds[0]= this.gameObject;
        }else if(Field.isFirstEquip){
            isSecond = true;
            Field.positions[1]= this.transform.position;
            Field.diamonds[1]= this.gameObject;
            Swap();
            
        }
        
        
        
    }
    void Swap(){
        Field.diamonds[0].transform.position = Field.positions[1];
        Field.diamonds[1].transform.position = Field.positions[0];
        Field.isFirstEquip = false;
    }

    
}
