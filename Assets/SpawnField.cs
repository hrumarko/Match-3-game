using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnField : MonoBehaviour
{
    public GameObject[] gameObjects;
    public static GameObject[,] field;
    int fieldSize = 10;
    public GameObject par;
    int rand;
    int?[] nums = new int?[3];
    public Animator startAnimator;
    
    void Start(){
        
        FirstSpawn();
        Clean();
        
        
    }
    void Update(){
        
        FillingTheVoid();
        
        
    }
    public void LoadScenes(){
        SceneManager.LoadScene(0);
    }

    public void FirstSpawn(){
        int a = 0;
        field = new GameObject[fieldSize, fieldSize];
        for(int y = 0; y < fieldSize; y++){
            for(int x = 0; x < fieldSize; x++){
                rand = Random.Range(0, gameObjects.Length);

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
                go.transform.parent = par.transform;
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
        for(int y = 0; y < fieldSize; y++){
            for(int x = 0; x < 8; x++){
                if(field[x, y].GetComponent<ObjectsGame>().Num == field[x+1, y].GetComponent<ObjectsGame>().Num && field[x+1, y].GetComponent<ObjectsGame>().Num == field[x+2, y].GetComponent<ObjectsGame>().Num){
                        Destroy(field[x, y]);
                        
                        Destroy(field[x+1, y]);
                        Destroy(field[x+2, y]);
                    }
                }
        }
        Invoke("LoweringDown", 0.3f);
        
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
                     while( n < fieldSize -1 && field[x, n] == null){
                        n++;
                     }
                    if(field[x, n] != null){
                        field[x, n].transform.position -= new Vector3(0, (n-y), 0);
                        field[x, y] = field[x, n];
                        field[x, n] = null;
                    }
                    
                    
                }
                
            }
        }

        Invoke("FillingTheVoid", 0.3f);
        
    }

    public void FillingTheVoid(){
        int random;
            for(int y = 0; y < fieldSize; y++){
                for(int x = 0; x < fieldSize; x++){
                    if(field[x, y] == null){
                        random = Random.Range(0, gameObjects.Length);
                        GameObject go = Instantiate(gameObjects[random], new Vector2(x, y), Quaternion.identity);
                        go.GetComponent<ObjectsGame>().SetPos(x, y);
                        go.transform.parent = par.transform;
                        field[x, y] = go;
                        
                    }
                }
            }
            Invoke("CheckWinLines", 0.3f);
            
    }

    void PlayAnim(){
        startAnimator.Play("start_game");
    }
}
