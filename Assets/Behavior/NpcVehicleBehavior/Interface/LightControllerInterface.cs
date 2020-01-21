using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControllerInterface : MonoBehaviour
{
    public class LightCommand
    {
        public bool brake_light = false;
        public bool fog_light = false;
        public bool head_light = false;
        public bool left_turn_indicator = false;
        public bool right_turn_indicator = false;
        public bool reverse_indicator = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Data_ = new LightCommand();
    }

    // Update is called once per frame
    void Update()
    {
        SetLight(BrakeLightRenderer,Data_.brake_light);
        SetLight(FogLightRenderer, Data_.fog_light);
        SetLight(HeadLightRenderer, Data_.head_light);
        SetLight(LeftTurnIndicatorRenderer, Data_.left_turn_indicator);
        SetLight(RightTurnIndicatorRenderer, Data_.right_turn_indicator);
        SetLight(ReverseIndicatorRenderer,Data_.reverse_indicator);
    }

    private void OnDestroy()
    {
        Sub_ = null;
    }

    private void SetLight(Renderer renderer,bool enable)
    {
        if(renderer == null)
        {
            return;
        }
        renderer.enabled = enable;
    }

    private UniCom.Subscriber<LightCommand> Sub_;
    public string Topic;
    // Renderers
    public Renderer BrakeLightRenderer;
    public Renderer FogLightRenderer;
    public Renderer HeadLightRenderer;
    public Renderer LeftTurnIndicatorRenderer;
    public Renderer RightTurnIndicatorRenderer;
    public Renderer ReverseIndicatorRenderer;
    private LightCommand Data_;
}