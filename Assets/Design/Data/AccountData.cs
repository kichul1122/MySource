using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KC
{
	[System.Serializable]
	public class AccountData : BaseData
	{
		#region Private Variables
		[SerializeField]
		private string id;
		[SerializeField]
		private string password;
		[SerializeField]
		private string createTime;
		[SerializeField]
		private string recentAccessTime;
		#endregion

		#region Properties
		public string ID { get { return id; } set { id = value; } }
		public string Password { get { return password; } set { password = value; } }
		public string CreateTime { get { return createTime; } set { createTime = value; } }
		public string RecentAccessTime { get { return recentAccessTime; } set { recentAccessTime = value; } }
		#endregion
	}
}

