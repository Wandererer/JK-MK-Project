using UnityEngine;
using System.Collections;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

// Orient the object to match that of the Myo armband.
// Compensate for initial yaw (orientation about the gravity vector) and roll (orientation about the wearer's arm) by allowing the user to set a reference orientation.
//
// Making the fingers spread pose or pressing the 'r' key resets the reference orientation.
public class JointOrientation : MonoBehaviour
{
	// Myo game object to connect with.
	// This object must have a ThalmicMyo script attached. 
	//thalmicmyo script를 얻어오기위한 게임 오브잭트
	public GameObject myo = null;
	
	// A rotation that compensates for the Myo armband's orientation parallel to the ground, i.e. yaw.  
	// Once set, the direction the Myo armband is facing becomes "forward" within the program. 프로그램 안에서는 설정되면  앞쪽을 향하고 있음
	// Set by making the fingers spread pose or pressing "r".  보자기를 하면  다시 세팅이가능
	private Quaternion _antiYaw = Quaternion.identity;
	
	// A reference angle representing how the armband is rotated about the wearer's arm, i.e. roll. 
	// Set by making the fingers spread pose or pressing "r".
	private float _referenceRoll = 0.0f; //myo의 휘는 걸 잡아옴
	
	// The pose from the last update. This is used to determine if the pose has changed
	// so that actions are only performed upon making them rather than every frame during
	// which they are active.
	private Pose _lastPose = Pose.Unknown;  //항상 움직임 바꾼걸 받아오지 않기 위해
	
	// Update is called once per frame.
	void Update ()
	{
		// Access the ThalmicMyo component attached to the Myo object.
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> (); //myo object에 접속
		
		// Update references when the pose becomes fingers spread or the r key is pressed. 
		//referece는 보자기나 r키가 눌렸을경우 true로 바뀜
		bool updateReference = false;
		if (thalmicMyo.pose != _lastPose) {
			_lastPose = thalmicMyo.pose;
			
			if (thalmicMyo.pose == Pose.FingersSpread) {
				updateReference = true;
				
				ExtendUnlockAndNotifyUserAction(thalmicMyo);
			}
		}
		if (Input.GetKeyDown ("r")) {
			updateReference = true;
		}

		//Debug.Log (myo.transform.forward+ "forward");
		
		// Update references. This anchors the joint on-screen such that it faces forward away
		// from the viewer when the Myo armband is oriented the way it is when these references are taken.
		//upadatereferece가 true였을 경우 화면에서 앞쪽을 바라보도록 초기화시킴
		if (updateReference) {
			// _antiYaw represents a rotation of the Myo armband about the Y axis (up) which aligns the forward
			// vector of the rotation with Z = 1 when the wearer's arm is pointing in the reference direction.
			//antiyaw는 rotation의 좌우를 앞쪽으로 정렬하고 
			//vector는 착용자의 바라보는쪽이  1이 되도록한다
			_antiYaw = Quaternion.FromToRotation (
				new Vector3 (myo.transform.forward.x, 0, myo.transform.forward.z),
				new Vector3 (0, 0, 1)
				); //myo의  x z로부터  0 0 1이 되도록 정렬한다

			Debug.Log (_antiYaw +"  ");
			
			// _referenceRoll represents how many degrees the Myo armband is rotated clockwise;; rotateroll은 시계방향으로 얼마나 회전을 하였나 나타낸다
			// about its forward axis (when looking down the wearer's arm towards their hand) from the reference zero  roll direction.  // 밑을 향하고 있을때가 reference zero임
			//This direction is calculated and explained below. When this reference is taken, the joint will be rotated about its forward axis such that it faces upwards when
			// the roll value matches the reference.
			Vector3 referenceZeroRoll = computeZeroRollVector (myo.transform.forward);
			//Debug.Log(referenceZeroRoll+"  referencezero");
			_referenceRoll = rollFromZero (referenceZeroRoll, myo.transform.forward, myo.transform.up);
		}

			// Current zero roll vector and roll value.
			Vector3 zeroRoll = computeZeroRollVector (myo.transform.forward); //zeroRoll이 외적값을 모두 계산하고 결과는 마이요 위쪽을 향하는 외적값이 넘어옴
		float roll = rollFromZero (zeroRoll, myo.transform.forward, myo.transform.up);
		//                                       ratation  z 앞쪽           ratation y 위쪽
		
		
		// The relative roll is simply how much the current roll has changed relative to the reference roll. //relativeRoll 은 각도가 얼마나 바뀌었는지만 파악
		// adjustAngle simply keeps the resultant value within -180 to 180 degrees.
       // Debug.Log(roll + "  roll");
        Debug.Log(_referenceRoll + "_referenceRoll"); //위치새로고침 했을당시의 각도값을 가져와서 
		float relativeRoll = normalizeAngle (roll - _referenceRoll); //현재 각도가 얼마나 바뀌었는지 확인


       // Debug.Log(relativeRoll + " relativeRoll");
	
        // antiRoll represents a rotation about the myo Armband's forward axis adjusting for reference roll.
		Quaternion antiRoll = Quaternion.AngleAxis (relativeRoll, myo.transform.forward); //forward 방향에서  relativeroll만큼의 각을 쿼터니언값으로 만들어줌

        Debug.Log(antiRoll + " antiRoll");
		
		// Here the anti-roll and yaw rotations are applied to the myo Armband's forward direction to yield
		// the orientation of the joint.
		transform.rotation = _antiYaw * antiRoll * Quaternion.LookRotation (myo.transform.forward); 
		//change this object rotation  --jk
		
		// The above calculations were done assuming the Myo armbands's +x direction, in its own coordinate system,
		// was facing toward the wearer's elbow. If the Myo armband is worn with its +x direction facing the other way,
		// the rotation needs to be updated to compensate.
		if (thalmicMyo.xDirection == Thalmic.Myo.XDirection.TowardWrist) {
			// Mirror the rotation around the XZ plane in Unity's coordinate system (XY plane in Myo's coordinate
			// system). This makes the rotation reflect the arm's orientation, rather than that of the Myo armband.
			transform.rotation = new Quaternion(transform.localRotation.x,
			                                    -transform.localRotation.y,
			                                    transform.localRotation.z,
			                                    -transform.localRotation.w);
		}
	}
	
	// Compute the angle of rotation clockwise about the forward axis relative to the provided zero roll direction. //
	// As the armband is rotated about the forward axis this value will change, regardless of which way the
	// forward vector of the Myo is pointing. The returned value will be between -180 and 180 degrees. myo가 어디로 회전하는지에 따라 값이 바뀜
	float rollFromZero (Vector3 zeroRoll, Vector3 forward, Vector3 up)
	{
		// The cosine of the angle between the up vector and the zero roll vector. 위쪽 백터와 제로 백터의 cosine
		//Since both are orthogonal to the forward vector, 앞쪽 백터(아래방향) 이랑  제로 벡터 둘다 forward랑 직각을 이룸
		//this tells us how far the Myo has been turned around the forward axis relative to the zero roll vector, //이게  얼마나  myo가 
		// but we need to determine separately whether the Myo has been rolled clockwise or counterclockwise.
		float cosine = Vector3.Dot (up, zeroRoll); //내적값 구함 
		//zerorolll이 구한 값은 이 환경에서의 처음시작시 위쪽의 값을 받아온다. 그값이랑 아예 위쪽을 내적한다
		//길이값

		//Debug.Log (cosine + "cosine");

		
		// To determine the sign of the roll, we take the cross product of the up vector and the zero
		// roll vector. This cross product will either be the same or opposite direction as the forward
		// vector depending on whether up is clockwise or counter-clockwise from zero roll.
		// Thus the sign of the dot product of forward and it yields the sign of our roll value.
		Vector3 cp = Vector3.Cross (up, zeroRoll);

       // Debug.Log(cp + "cp");

		float directionCosine = Vector3.Dot (forward, cp);
		float sign = directionCosine < 0.0f ? 1.0f : -1.0f; //왼쪽 방향인지 오른쪽인지 왼쪽이 1 오른쪽이 -1

       // Debug.Log(sign + " sign");
       // Debug.Log(sign * Mathf.Rad2Deg * Mathf.Acos (cosine) + " ??");
      //  Debug.Log(Mathf.Acos(cosine) + " Mathf.Acos (cosine) "); //코사인 역함수

		// Return the angle of roll (in degrees) from the cosine and the sign. 
		//코사인과 위치값의 리턴 하고방향을 정해줌
		return sign * Mathf.Rad2Deg * Mathf.Acos (cosine);
	}
	
	// Compute a vector that points perpendicular to the forward direction, forward 방향의 직각을 구함
	// minimizing angular distance from world up (positive Y axis).  // 위쪽을 바라보는 각을 최소 화함
	// This represents the direction of no rotation about its forward axis. // forwaRD각도는 회전이 없음
	Vector3 computeZeroRollVector (Vector3 forward)//두 백터를 외적을 해도 결국 마이오의 위방향의 외적값을 구하게됨 처음 시작시 위방향을 계산해서 바꿔줌
	{
		//자기 원래 위치의 백터를 위로 향할수있게

		Vector3 antigravity = Vector3.up;
		//Debug.Log (antigravity + " anti");
		Vector3 m = Vector3.Cross (myo.transform.forward, antigravity);//외적값 구함  myo.transform.forward ( 회전 좌우)랑 위로 향하는 것이랑 외적값을 구함 처음 이값 -1 , 0 , 0
		Vector3 roll = Vector3.Cross (m, myo.transform.forward); //외적값 구함  //처음 이값 다시 위 방향가리킴
		
		//Debug.Log(myo.transform.forward +" forward");
		//Debug.Log(m + " zeroroll m");
		// Debug.Log(roll + " zeroroll roll");
		//Debug.Log(roll.normalized + "  roll.normalized");
		
		return roll.normalized;
	}
	
	// Adjust the provided angle to be within a -180 to 180.
	float normalizeAngle (float angle)
	{
		if (angle > 180.0f) {
			return angle - 360.0f;
		}
		if (angle < -180.0f) {
			return angle + 360.0f;
		}
		return angle;
	}
	
	// Extend the unlock if ThalmcHub's locking policy is standard, and notifies the given myo that a user action was
	// recognized.
	void ExtendUnlockAndNotifyUserAction (ThalmicMyo myo)
	{
		ThalmicHub hub = ThalmicHub.instance;
		
		if (hub.lockingPolicy == LockingPolicy.Standard) {
			myo.Unlock (UnlockType.Timed);
		}
		
		myo.NotifyUserAction ();
	}
}
