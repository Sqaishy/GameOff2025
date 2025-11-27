using System;
using UnityEngine;

namespace SubHorror.Interaction
{
	public class Item : MonoBehaviour
	{
		public Guid ItemGuid { get; private set; }

		public Item Clone()
		{
			Item clone = Instantiate(this);
			clone.ItemGuid = ItemGuid;
			return clone;
		}

		public static bool operator ==(Item a, Item b)
		{
			return a.ItemGuid == b.ItemGuid;
		}

		public static bool operator !=(Item a, Item b)
		{
			return !(a == b);
		}

		private void OnValidate()
		{
			if (ItemGuid == Guid.Empty)
				ItemGuid = Guid.NewGuid();
		}
	}
}