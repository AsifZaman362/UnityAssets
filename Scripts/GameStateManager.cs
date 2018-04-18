using UnityEngine;


public class GameStateManager : MonoBehaviour{


    ///<summary>
    /// Game state is represented as : 
    /// 0 : Paused, 1 : Running
    /// </summary>
    private int state;
    private float timeScale_;

    void Start(){
        state = 1;
        timeScale_ = Time.timeScale;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            if(state == 0){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    public void Pause(){
        if(state == 1){
            timeScale_ = Time.timeScale;
            Time.timeScale = 0;
            state = 0;
        }
    }

    public void Resume(){
        if(state == 0){
            Time.timeScale = timeScale_;
            state = 1;
        }
    }
}