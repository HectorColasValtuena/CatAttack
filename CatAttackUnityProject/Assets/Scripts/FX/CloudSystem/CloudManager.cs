using UnityEngine;

namespace CatAttack
{
	public class CloudManager : MonoBehaviour
	{
		//private const int cloudZPos = 10;
		//public static CloudManager instance;

		public Transform centerPoint;	//transform of the point around which to limit cloud movement

		public Vector2 maxCloudDistance;	//how far from the center of the system (positive and negative) a cloud can reach
		public int cloudAmmount;			//how many clouds to create on startup
		public Vector2 minCloudSpeed;			//random range of cloud speeds
		public Vector2 maxCloudSpeed;
		public Vector2 cloudDirection;		//>=0 = clouds move rightwards, <0 clouds move leftwards

		public GameObject cloudPrefab;		//prefab of a cloud element
		public Sprite[] cloudSpriteList;	//collection of available sprites for clouds

		public void Awake ()
		{
			//CloudManager.instance = this;
			cloudDirection = new Vector2 ((cloudDirection.x >= 0) ? 1 : -1, (cloudDirection.y >= 0) ? 1 : -1 );
		}

		public void Start ()
		{
			if (centerPoint == null) { centerPoint = ParallaxCamera.main.transform; }
			InitializeClouds ();
		}

		public void InitializeClouds ()
		{
			//on start summon and initialize pre-set ammount of clouds
			for (int i = 0; i < cloudAmmount; i++)
			{
				CloudInstance newCloud = Instantiate(cloudPrefab, transform).GetComponent<CloudInstance>();

				//newCloud.transform.SetParent(transform);
				//give the cloud a center point
				newCloud.centerPoint = centerPoint;
				//initialize new cloud's position
				newCloud.transform.localPosition = new Vector3(Random.Range(-maxCloudDistance.x, maxCloudDistance.x), Random.Range(-maxCloudDistance.y, maxCloudDistance.y), 0);
				//Initialize new cloud's move speed
				newCloud.moveSpeed =  new Vector2(Random.Range(minCloudSpeed.x, maxCloudSpeed.x) * cloudDirection.x, Random.Range(minCloudSpeed.y, maxCloudSpeed.y) * cloudDirection.y);
				//Initialize new cloud's sprite 
				newCloud.cloudSprite = cloudSpriteList[Random.Range(0, cloudSpriteList.Length)];
				newCloud.cloudManager = this;
			}
		}

		//reset a cloud that reached it's movement limit.
		public void ResetCloud (CloudInstance targetCloud)
		{
			//update the cloud's center point in case it changed
			targetCloud.centerPoint = centerPoint;

			//set cloud's position within a random vertical range, but right over horizontal bordersn
			targetCloud.transform.position = centerPoint.position + new Vector3(
				maxCloudDistance.x * ((Random.Range((int) 0 , 2) * 2) - 1),	//horizontal position is the maximum limit multiplied by 1 or -1 randomly
				Random.Range(-maxCloudDistance.y, maxCloudDistance.y),	//vertical position is random within limits
				- centerPoint.position.z									//z should be 0, so nullify centerPoint's z
			);

			//randomize cloud's move speed
			targetCloud.moveSpeed = new Vector2(
				Random.Range(minCloudSpeed.x, maxCloudSpeed.x) * cloudDirection.x,
				Random.Range(minCloudSpeed.y, maxCloudSpeed.y) * cloudDirection.y
			);

			//randomize cloud's sprite 
			targetCloud.cloudSprite = cloudSpriteList[Random.Range(0, cloudSpriteList.Length)];
		}
	}
}