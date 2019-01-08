using UnityEngine.EventSystems;

using UnityEngine;

public class TowerManager : Singleton<TowerManager> {

	public TowerButton towerBtnPressed{get; set;}
	private SpriteRenderer spriteRenderer;


	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
			if(hit.collider.tag == "Build Site"){
				hit.collider.tag = "BuildSiteFull";
				placeTower(hit);
			}


		}
		if(spriteRenderer.enabled){
			followMouse();
		}	
	}

	public void placeTower(RaycastHit2D hit) {
		if(!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null) {
			GameObject newTower= Instantiate(towerBtnPressed.TowerObject);
			newTower.transform.position = hit.transform.position;
			disableDragSprite();
		}
	}
	public void selectedTower(TowerButton towerSelected) {
		towerBtnPressed = towerSelected;
		enableDragSprite(towerBtnPressed.DragSprite);

	}
	public void followMouse(){
		transform.position =Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = new Vector2(transform.position.x, transform.position.y);
	}

	public void enableDragSprite(Sprite sprite) {
		spriteRenderer.enabled = true;
		spriteRenderer.sprite = sprite;
	}

	public void disableDragSprite() {
		spriteRenderer.enabled = false;
	}
}
