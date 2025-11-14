using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace SubHorror.Tasks
{
	[CustomEditor(typeof(ObjectiveInteractable))]
	public class ObjectiveInteractableEditor : Editor
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
			Foldout dataFoldout = new Foldout
			{
				text = "Objective Data",
			};

			root.Add(dataFoldout);

			if (objectiveData.boxedValue == null)
			{
				dataFoldout.Add(new Label("No Objective Data"));
				return;
			}

			foreach (FieldInfo field in objectiveData.boxedValue.GetType().GetFields()) 
				dataFoldout.Add(new PropertyField(objectiveData.FindPropertyRelative(field.Name)));
		}
	}
}