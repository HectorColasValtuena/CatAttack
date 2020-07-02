using UnityEngine;

namespace CatAttack
{
	public class CloudManager : MonoBehaviour
	{
		//private const int cloudZPos = 10;
		public static CloudManager instance;

		public Vector2 maxCloudDistance;	//how far from the center of the system (positive and negative) a cloud can reach
		public int cloudAmmount;			//how many clouds to create on startup
		public float minCloudSpeed;			//random range of cloud speeds
		public float maxCloudSpeed;
		public float cloudDirection;		//>=0 = clouds move rightwards, <0 clouds move leftwards

		public GameObject cloudPrefab;		//prefab of a cloud element
		public Sprite[] cloudSpriteList;	//collection of available sprites for clouds

		public void Awake ()
		{
			CloudManager.instance = this;
			cloudDirection = (cloudDirection >= 0) ? 1 : -1;
		}

		public void Start ()
		{
			InitializeClouds ();
		}

		public void InitializeClouds ()
		{
			//on start summon and initialize pre-set ammount of clouds
			for (int i = 0; i < cloudAmmount; i++)
			{
				CloudInstance newCloud = Instantiate(cloudPrefab).GetComponent<CloudInstance>();

				newCloud.transform.SetParent(transform);
				//initialize new cloud's position
				newCloud.transform.localPosition = new Vector3(Random.Range(-maxCloudDistance.x, maxCloudDistance.x), Random.Range(-maxCloudDistance.y, maxCloudDistance.y), 0);
				//Initialize new cloud's move speed
				newCloud.moveSpeed = Random.Range(minCloudSpeed, maxCloudSpeed) * cloudDirection;
				//Initialize new cloud's sprite 
				newCloud.cloudSprite = cloudSpriteList[Random.Range(0, cloudSpriteList.Length)];
			}
		}

		//reset a cloud that reached it's movement limit.
		public void ResetCloud (CloudInstance targetCloud)
		{
			//initialize cloud's position
			targetCloud.transform.localPosition = new Vector3(maxCloudDistance.x * cloudDirection * -1, Random.Range(-maxCloudDistance.y, maxCloudDistance.y), 0);
			//Initialize cloud's move speed
			targetCloud.moveSpeed = Random.Range(minCloudSpeed, maxCloudSpeed) * cloudDirection;
			//Initialize cloud's sprite 
			targetCloud.cloudSprite = cloudSpriteList[Random.Range(0, cloudSpriteList.Length)];
		}
	}
}