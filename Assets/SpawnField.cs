using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnField : MonoBehaviour
{
    public GameObject[] gameObjects;
    public static GameObject[,] field;
    int fieldSize = 10;
    
    int rand;
    int?[] nums = new int?[3];
    
    void Start(){
        FirstSpawn();
        Clean();
        
    }
    void Update(){
        
        
        if(Input.GetMouseButtonDown(1)){
            LoweringDown();
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            FillingTheVoid();
        }
        
    }

    public void FirstSpawn(){
        int a = 0;
        field = new GameObject[fieldSize, fieldSize];
        for(int y = 0; y < fieldSize; y++){
            for(int x = 0; x < fieldSize; x++){
                rand = Random.Range(0, gameObjects.Length);
                // if(a==0){
                //     nums[0] = rand;
                //     a = 1;
                // }else if(a == 1){
                //     nums[1] = rand;
                //     a = 2;
                // }else if(a == 2){
                //     nums[2] = rand;
                //     a =0;
                // }
                CheckArrayForSpawn(a, rand);
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
    void Clean(){
        
        int b = 0;
        for(int x = 0; x < fieldSize; x++){
            for(int y = 0; y < fieldSize; y++){
                int rands = field[x, y].GetComponent<ObjectsGame>().Num;
                
                CheckArrayForSpawn(b, rands);
                if(nums[0]== nums[1] && nums[1]==nums[2]){
                    while(rands == field[x,y].GetComponent<ObjectsGame>().Num){
                        rands =Random.Range(0, gameObjects.Length);
                        
                        nums[0]= rands;
                        nums[1] = 1000;
                        nums[2]= 500;
                    }
                    Destroy(field[x, y]);
                    
                    GameObject go = Instantiate(gameObjects[rands], new Vector2(x, y), Quaternion.identity);
                    go.GetComponent<ObjectsGame>().SetPos(x, y);
                    field[x, y] = go;
                }

            }
        }
    }

    

    public void CheckWinLines(){
        
        Debug.Log("ХУЙ");
        for(int y = 0; y < fieldSize; y++){
            for(int x = 0; x < 8; x++){
                if(field[x, y].GetComponent<ObjectsGame>().Num == field[x+1, y].GetComponent<ObjectsGame>().Num && field[x+1, y].GetComponent<ObjectsGame>().Num == field[x+2, y].GetComponent<ObjectsGame>().Num){
                        Destroy(field[x, y]);
                        
                        Destroy(field[x+1, y]);
                        Destroy(field[x+2, y]);
                    }
                }
        }
        
    }
    public void CheckArrayForSpawn(int a, int rand){
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
    }

    public void LoweringDown(){
        int n;
        for(int y = 0; y < fieldSize -1; y++){
            for(int x = 0; x < fieldSize; x++){
                if(field[x, y] == null){
                     n = y+1;
                     while(field[x, n] == null){
                        n++;
                     }
                    
                    field[x, n].transform.position -= new Vector3(0, n-y, 0);
                    field[x, y] = field[x, n];
                    field[x, n] = null;
                    
                }
                
            }
        }
        
    }

    public void FillingTheVoid(){
        int random;
            for(int y = 0; y < fieldSize; y++){
                for(int x = 0; x < fieldSize; x++){
                    if(field[x, y] == null){
                        random = Random.Range(0, gameObjects.Length);
                        GameObject go = Instantiate(gameObjects[random], new Vector2(x, y), Quaternion.identity);
                        go.GetComponent<ObjectsGame>().SetPos(x, y);
                        field[x, y] = go;
                        
                    }
                }
            }
    }
}
