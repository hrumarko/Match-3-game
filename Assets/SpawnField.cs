using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnField : MonoBehaviour
{
    public GameObject[] gameObjects;
    public GameObject[,] field;
    int fieldSize = 10;
    
    int rand;
    int?[] nums = new int?[3];
    
    void Start(){
        FirstSpawn();
        Clean(); 
    }

    public void FirstSpawn(){
        int a = 0;
        field = new GameObject[fieldSize, fieldSize];
        for(int y = 0; y < fieldSize; y++){
            for(int x = 0; x < fieldSize; x++){
                rand = Random.Range(0, gameObjects.Length);
                if(a==0){
                    nums[0] = rand;
                    a = 1;
                }else if(a == 1){
                    nums[1] = rand;
                    a = 2;
                }else if(a == 2){
                    nums[2] = rand;
                    a =0;
                }
                if(nums[0] == nums[1] && nums[1] == nums[2]){
                    while(rand== nums[2]){
                        rand = Random.Range(0, gameObjects.Length);
                    }
                    nums[0]= rand;
                    nums[1] = 1000;
                    nums[2]= 500;
                }
                GameObject go = Instantiate(gameObjects[rand], new Vector2(x, y), Quaternion.identity);
                go.GetComponent<ObjectsGame>().SetPos(x, y);
                field[x, y] = go;
            }
        }
    }
    public void Clean(){
        int b = 0;
        for(int x = 0; x < fieldSize; x++){
            for(int y = 0; y < fieldSize; y++){
                int rands = field[x, y].GetComponent<ObjectsGame>().Num;
                if(b == 0){
                    nums[0]= rands;
                    b=1;
                }else if(b == 1){
                    nums[1]= rands;
                    b=2;
                }else if(b == 2){
                    b=0;
                    nums[2]= rands;
                }

                if(nums[0]== nums[1] && nums[1]==nums[2]){
                    while(rands == field[x,y].GetComponent<ObjectsGame>().Num){
                        rands =Random.Range(0, gameObjects.Length);
                        
                        nums[0]= rands;
                        nums[1] = 1000;
                        nums[2]= 500;
                    }
                    Destroy(field[x, y]);
                    
                    GameObject go = Instantiate(gameObjects[rands], new Vector2(x, y), Quaternion.identity);
                    field[x, y] = go;
                }

            }
        }
    }
}
