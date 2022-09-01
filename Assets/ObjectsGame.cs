using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsGame : MonoBehaviour
{
    public int x;
    public int y;
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

        int x1 = Mathf.RoundToInt(Field.diamonds[0].transform.position.x);
        int y1 = Mathf.RoundToInt(Field.diamonds[0].transform.position.y);
        int x2 = Mathf.RoundToInt(Field.diamonds[1].transform.position.x);
        int y2 = Mathf.RoundToInt(Field.diamonds[1].transform.position.y);

        Field.diamonds[0].GetComponent<ObjectsGame>().SetPosAfterSwap(x1, y1);
        SpawnField.field[x1,y1] = Field.diamonds[0];

        Field.diamonds[1].GetComponent<ObjectsGame>().SetPosAfterSwap(x2, y2);
        SpawnField.field[x2,y2] = Field.diamonds[1];
        Field.isFirstEquip = false;
        
    }
    public void SetPosAfterSwap(int x, int y){
        SetPos(x, y);
    }

    
}
