using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleStatusInterface : MonoBehaviour
{
    public class VehicleStatus
    {
        public double forward_velocity;
        public double angular_velocity;
        public Vector3 position;
    }

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody_ = GetComponent<Rigidbody>();
        //Rigidbody_ = transform.parent.gameObject.GetComponent<Rigidbody>();
        Data_ = new VehicleStatus();
        Pub_ = new UniCom.Publisher<VehicleStatus>(VehicleStatusTopic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Data_.position = transform.position;
        if(SimulatorManager.Instance == null)
        {
            return;
        }
        Vector3 velocity = transform.InverseTransformDirection(Rigidbody_.velocity);
        Data_.forward_velocity = velocity.z;
        Vector3 angular_velocity = Rigidbody_.angularVelocity;
        Data_.angular_velocity = angular_velocity.z;
        Pub_.Publish(Data_);
    }

    private void OnDestroy()
    {
        Pub_ = null;
    }

    [SerializeField] private string VehicleStatusTopic;
    private UniCom.Publisher<VehicleStatus> Pub_;
    private VehicleStatus Data_;
    private Rigidbody Rigidbody_;
}
