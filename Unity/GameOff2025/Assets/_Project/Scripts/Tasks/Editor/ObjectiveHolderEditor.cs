using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace SubHorror.Tasks
{
	[CustomEditor(typeof(ObjectiveHolder))]
	public class ObjectiveHolderEditor : Editor
	{
		public override VisualElement CreateInspectorGUI()
		{
			VisualElement root = new VisualElement();

			SerializedProperty objectiveProperty = serializedObject.FindProperty("objective");
			SerializedProperty dataProperty = serializedObject.FindProperty("objectiveData");

			root.Add(new PropertyField(objectiveProperty));

			AddObjectiveData(dataProperty, root);

			return root;
		}

		private void AddObjectiveData(SerializedProperty objectiveData, VisualElement root)
		{
			if (objectiveData.boxedValue == null)
			{
				Foldout dataFoldout = new Foldout
				{
					text = "Objective Data",
				};

				root.Add(dataFoldout);

				dataFoldout.Add(new Label("No Objective Data"));
				return;
			}

			root.Add(new PropertyField(objectiveData));
		}
	}
}