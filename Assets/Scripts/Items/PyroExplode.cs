using UnityEngine;
using System.Collections;


public class PyroExplode : MonoBehaviour {

	EnemyHealth enemyHeatlh;
	Transform mineTransform;
	int i = 0;
	
	public void setTransform(Transform mineTransform){
		this.mineTransform = mineTransform;

	}

	public void Update(){

	}
	
	public void Generate(){
		
		Destroy(Instantiate ( Resources.Load ("Pyro1"), mineTransform.position, mineTransform.rotation), 5);
		GameMaster.currentMines--;
		MineManager.nbMine = GameMaster.currentMines;

	}

	
}