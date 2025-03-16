using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class StopStareAT : ActionTask<NavMeshAgent> {

        public BBParameter<Transform> targetObject; // The object AI should look at
        public float rotationSpeed = 5f; // Speed of rotation

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {

            agent.isStopped = true; // Stop the AI from moving
            agent.velocity = Vector3.zero; // Clear velocity to prevent sliding
            //EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {

            if (Vector3.Distance(agent.transform.position, targetObject.value.position) > 5f)
            {
                EndAction(true);
                //return;
            }

            // Get direction to the target object
            Vector3 direction = targetObject.value.position - agent.transform.position;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            agent.transform.rotation = Quaternion.Lerp(agent.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);


        }

        //Called when the task is disabled.
        protected override void OnStop() {

        }

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}