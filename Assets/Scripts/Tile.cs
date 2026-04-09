using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Tile : MonoBehaviour
{
    public TileState state {get; private set;}
    public TileCell cell {get; private set;}
    public int number {get; private set;}
    public bool locked {get; set;}
    public Image background;
    public TextMeshProUGUI numberText;  
    
    private void Awake(){
        background = GetComponent<Image>();
        numberText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetState(TileState state, int number )
    {
        this.state = state; 
        this.number = number;
        background.color = state.backgroundColor;
        numberText.color = state.textColor;
        numberText.text = number.ToString();
    }

    public void Spawn(  TileCell cell){
        if (this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell =cell;
        this.cell.tile = this;
         transform.position = cell.transform.position;
    }
        public void MoveTo(  TileCell cell){
        if (this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell =cell;
        this.cell.tile = this;
        StartCoroutine(Animate(cell.transform.position,false));
    }
    private IEnumerator Animate(Vector3 end, bool merging)
    {
        float elapsed = 0;
        float duration = 0.1f;
        Vector3 start = transform.position;
        while(elapsed < duration){
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, elapsed / duration);
            yield return null;
        }
        transform.position = end;
        if(merging){
            Destroy(gameObject);
        }
    }

    public void Merge(TileCell cell)
    {
        if(this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell = cell;
        cell.tile.locked = true;
        StartCoroutine(Animate(cell.transform.position, true));
        
    }
}
