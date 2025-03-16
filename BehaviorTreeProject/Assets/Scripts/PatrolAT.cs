using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class PatrolAT : ActionTask<NavMeshAgent> /*remember I can use <> to directly interact with navmesh and other stuff to save space writing all the extra stuff*/ {

        public BBParameter<Vector3> pointA; // Note I got an error doing the same thing but using public transforms as an array idk why
        public BBParameter<Vector3> pointB; // Travel between these 3 points
        public BBParameter<Vector3> pointC;
        public BBParameter<Transform> targetObject; // I had lots of issues trying to end the action so I just put this here as well
        public BBParameter<float> detectionRadius;
        public float stoppingDistance = 0f;

        private Vector3[] patrolPoints;
        private static int currentPoint = 0; // another error trying to get patrol to patrol where it left off

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {

            patrolPoints = new Vector3[] { pointA.value, pointB.value, pointC.value };
            SetNextDestination();
            //EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {

            if (targetObject.value != null) // Ending action when object is nearby
            {
                float distanceToTarget = Vector3.Distance(agent.transform.position, targetObject.value.position);
                if (distanceToTarget <= detectionRadius.value)
                {
                    Debug.Log("aaa");
                    EndAction(true);
                    return;
                }
            }

            if (agent.remainingDistance <= stoppingDistance && !agent.pathPending)
            {
                SetNextDestination();
            }
        }

        private void SetNextDestination()
        {
            agent.SetDestination(patrolPoints[currentPoint]); // Move to points
            currentPoint = (currentPoint + 1) % patrolPoints.Length; // Loop through points reset back to point 0 when complete
        }

        //Called when the task is disabled.
        protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}