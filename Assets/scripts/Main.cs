using UnityEngine;
using UnityEngine.UI;

public class FruitCombineGame : MonoBehaviour
{
    public GameObject[] vegetables;
    
    

    private GameObject currentvegetable;
    private GameObject nextvegetable;
    

    private float moveSpeed = 5f;
    private float borderX = 15f;
    private bool canMove = true;

    void Start() //pradeda zaidima
    {
        InitializeGame();
    }

    void Update() // normaliai vegetable turetu likti virsuj ir cia yra kad negaletum uz ribu ismest (working progress)
    {
        if (canMove)
        {
            MoveFruit();

        
        }
    }

    void InitializeGame() // pradeda zaidima atspawnindamas vegetable
    {
        Spawnvegetable();
        
       
       
    }

    void Spawnvegetable() // neveikia bet labai arti
    {
        currentvegetable = Instantiate(vegetables[Random.Range(0, 5)], new Vector2(0f, 8f), Quaternion.identity);
    }




    void MoveFruit()
    {
        float ximput = Input.GetAxis("Horizontal");
        Vector2 currentpos = currentvegetable.transform.position;
        currentpos.x += ximput * moveSpeed * Time.deltaTime;
        currentpos.x = Mathf.Clamp(currentpos.x, -borderX, borderX);
        currentvegetable.transform.position = currentpos;
    }

    
    
}