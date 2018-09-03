# Scriptable Object

[Unity Scriptable Object](https://unity3d.com/kr/how-to/architect-with-scriptable-objects)

*Create Instances in-memory with
ScriptableObject.CreateInstance<MySO>();

*Bine them to .asset files with
AssetDatabase.CreateAsset() or AssetDatabase.AddObjectToAsset()

[CreateAssetMenu]

*SO Callbacks
OnEnable()
OnDisable()
OnDestroy()

*SO Lifecycle
Same as any other asset
Resources.UnloadUnusedAssets()

*Extendable Enums
Empth SOs bound to asset files
Check equality, use as dictionary keys
Natural progression to data objects
Supports null

*Dual Serialisation
SO are supported by JsonUtility
Mix in-built Sos with deserialised JSON SOs

-Load built-in level from an AssetBundle
level = lvlsBundle.LoadAsset<LevelLayout>("lvl1.asset");

-Load level from JSON
level = CreateInstance<LevelLayout>();
var json = File.ReadAllText("customlevel.json");
JsonUtility.FromJsonOverwrite(json, level);

class MySingleton : ScriptableObject
{
	private static MySingleton inst;

	public static MySingleton Instance
	{
		get
		{
			if(!inst)
			{
				inst = Resources.FindObjectOfType<MySingleton>();
			}

			if(!inst)
			{
				inst = CreateInstance<MySingleton>();
			}

			return inst;
		}
	}
}

*Delegate objects
SO can contain methods, not only data
Pass in scene objects to work on
Strategy pattern

