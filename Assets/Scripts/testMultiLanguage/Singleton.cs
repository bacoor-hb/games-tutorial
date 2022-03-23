//using UnityEngine;

//public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
//{
//    public bool isDontDestroy;

//    private static T instance;
//    public static T Instance
//    {
//        get
//        {
//            if (instance == null)
//            {
//                instance = (T)FindObjectOfType(typeof(T));
//                if (instance == null)
//                {
//                    Debug.LogError(typeof(T) + "is nothing");
//                }
//            }
//            return instance;
//        }
//    }

//    protected virtual void Awake()
//    {
//        if (CheckInstance() && isDontDestroy)
//        {
//            DontDestroyOnLoad(this.gameObject);
//        }
//    }

//    protected bool CheckInstance()
//    {
//        if (this == Instance)
//        {
//            return true;
//        }
//        Destroy(this);
//        return false;
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{

	#region Fields

	/// <summary>
	/// The instance.
	/// </summary>
	private static T instance;

	#endregion

	#region Properties

	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<T>();
				if (instance == null)
				{
					GameObject obj = new GameObject();
					obj.name = typeof(T).Name;
					instance = obj.AddComponent<T>();
				}
			}
			return instance;
		}
	}

	#endregion

	#region Methods

	/// <summary>
	/// Use this for initialization.
	/// </summary>
	protected virtual void Awake()
	{
		if (instance == null)
		{
			instance = this as T;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	#endregion

}
