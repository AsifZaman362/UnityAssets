using UnityEngine;
using System.Collections;

public class ParallaxEffect : MonoBehaviour {

    [SerializeField]
    private GameObject[] layers;

    [SerializeField]
    private float[] layermove_limiters;

	private float lastCamPos;

	void Start () {
		lastCamPos = Camera.main.transform.position.x;
	}
	

	void LateUpdate () {

        StartCoroutine(move());
        
		//float move_amnt_layer1 = move_amount / layer1move_limiter;
		//float move_amnt_layer2 = move_amount / layer2move_limiter;
		//Vector3 targetPos1 = layer1.transform.position;
		//Vector3 targetPos2 = layer2.transform.position;
		//targetPos1.x += move_amnt_layer1;
		//targetPos2.x += move_amnt_layer2;
		//layer1.transform.position = targetPos1;
		//layer2.transform.position = targetPos2;

		lastCamPos = Camera.main.transform.position.x;

	}

    IEnumerator move()
    {
        yield return null;
        float move_amount = (Camera.main.transform.position.x - lastCamPos);
        for (int i = 0; i < layers.Length; i++)
        {
            Vector3 targetPos = layers[i].transform.position;
            float offset_move = move_amount / layermove_limiters[i];
            targetPos.x += offset_move;
            layers[i].transform.position = targetPos;
        }
    }
}
