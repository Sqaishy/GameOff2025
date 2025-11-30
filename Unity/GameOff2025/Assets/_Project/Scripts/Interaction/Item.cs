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

		protected bool Equals(Item other)
		{
			return base.Equals(other) && ItemGuid.Equals(other.ItemGuid);
		}

		public override bool Equals(object obj)
		{
			if (obj is null) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((Item)obj);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static bool operator ==(Item a, Item b)
		{
			if (a == null || b == null)
				return false;

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