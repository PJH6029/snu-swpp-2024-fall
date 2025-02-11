// using UnityEngine;
// using UnityEditor;

// [CustomEditor(typeof(DialogueNode), true)]
// public class DialogueNodeEditor : Editor
// {
//     // Serialized properties
//     SerializedProperty nodeIDProp;
//     SerializedProperty dialogueTextProp;
//     SerializedProperty optionsProp;

//     // PlainNextDialogueNode properties
//     SerializedProperty nextNodeProp;

//     // RewardDialogueNode properties
//     SerializedProperty rewardedItemProp;

//     // PenaltyDialogueNode properties
//     SerializedProperty penaltyedItemProp;

//     // MarkNPCAsMetNode properties
//     SerializedProperty NPCnameProp;

//     // ConditionDialogueNode properties
//     SerializedProperty conditionMetNextNodeProp;
//     SerializedProperty conditionNotMetNextNodeProp;
//     SerializedProperty conditionsProp;

//     // OptionsDialogueNode properties
//     SerializedProperty textNodePairsProp;

//     // Quest DialogueNode properties
//     SerializedProperty questProp;

//     // QuestOfferDialogueNode properties
//     SerializedProperty questAcceptedNodeProp;
//     SerializedProperty questRejectedNodeProp;

//     // ConditionOptionDialogueNode properties
//     // SerializedProperty conditionalTextNodePairsProp; 

//     private void OnEnable()
//     {
//         nodeIDProp = serializedObject.FindProperty("nodeID");
//         dialogueTextProp = serializedObject.FindProperty("dialogueText");
//         // optionsProp = serializedObject.FindProperty("options");

//         // PlainNextDialogueNode properties
//         nextNodeProp = serializedObject.FindProperty("nextNode");

//         // RewardDialogueNode properties
//         rewardedItemProp = serializedObject.FindProperty("rewardedItem");

//         // PenaltyDialogueNode properties
//         penaltyedItemProp = serializedObject.FindProperty("penaltyItem");

//         // MarkNPCAsMetNode properties
//         NPCnameProp = serializedObject.FindProperty("NPCName");

//         // ConditionDialogueNode properties
//         conditionMetNextNodeProp = serializedObject.FindProperty("conditionMetNextNode");
//         conditionNotMetNextNodeProp = serializedObject.FindProperty("conditionNotMetNextNode");
//         conditionsProp = serializedObject.FindProperty("conditions");

//         // OptionsDialogueNode properties
//         textNodePairsProp = serializedObject.FindProperty("textNodePairs");

//         // QuestDialogueNode properties
//         questProp = serializedObject.FindProperty("associatedQuest");

//         // QuestOfferDialogueNode properties
//         questAcceptedNodeProp = serializedObject.FindProperty("questAcceptedNode");
//         questRejectedNodeProp = serializedObject.FindProperty("questRejectedNode");

//         // ConditionOptionDialogueNode properties 
//         // conditionalTextNodePairsProp = serializedObject.FindProperty("conditionalTextNodePairs");
//     }

//     public override void OnInspectorGUI()
//     {
//         // Update the serialized object
//         serializedObject.Update();

//         // Draw the common fields
//         EditorGUILayout.PropertyField(nodeIDProp);
//         EditorGUILayout.PropertyField(dialogueTextProp);
//         // EditorGUILayout.PropertyField(optionsProp, true);

//         // Determine if the current target
//         if (target is PlainNextDialogueNode)
//         {
//             EditorGUILayout.PropertyField(nextNodeProp);
//             EditorGUILayout.Space();
//             EditorGUILayout.HelpBox("Options are managed automatically for PlainNextDialogueNode.", MessageType.Info);
//         }
//         else if (target is ConditionsDialogueNode)
//         {
//             EditorGUILayout.PropertyField(conditionMetNextNodeProp);
//             EditorGUILayout.PropertyField(conditionNotMetNextNodeProp);
//             EditorGUILayout.PropertyField(conditionsProp, true);
//         }
//         else if (target is OptionsDialogueNode)
//         {
//             EditorGUILayout.PropertyField(textNodePairsProp, true);
//         }
//         // else if (target is ConditionOptionDialogueNode) 
//         // {
//         //     EditorGUILayout.PropertyField(conditionalTextNodePairsProp, true);
//         //     EditorGUILayout.HelpBox("Each option will only appear if its conditions are met.", MessageType.Info);
//         // }
//         else if (target is LeafDialogueNode)
//         {
//             EditorGUILayout.Space();
//             EditorGUILayout.HelpBox("LeafDialogueNode has no options.", MessageType.Info);
//         }
//         else if (target is QuestDialogueNode)
//         {
//             EditorGUILayout.PropertyField(questProp);

//             if (target is QuestOfferDialogueNode)
//             {
//                 EditorGUILayout.PropertyField(questAcceptedNodeProp);
//                 EditorGUILayout.PropertyField(questRejectedNodeProp);
//             }
//             else if (target is QuestRewardDialogueNode)
//             {
//                 EditorGUILayout.PropertyField(nextNodeProp);
//             }
//         }
//         else if (target is RewardDialogueNode)
//         {
//             EditorGUILayout.PropertyField(nextNodeProp);
//             EditorGUILayout.PropertyField(rewardedItemProp);
//             EditorGUILayout.Space();
//             EditorGUILayout.HelpBox("Options are managed automatically for RewardDialogueNode.", MessageType.Info);
//         }
//         else if (target is PenaltyDialogueNode)
//         {
//             EditorGUILayout.PropertyField(nextNodeProp);
//             EditorGUILayout.PropertyField(penaltyedItemProp);
//             EditorGUILayout.Space();
//             EditorGUILayout.HelpBox("Options are managed automatically for PenaltyDialogueNode.", MessageType.Info);
//         }
//         else if (target is MarkNPCAsMetNode)
//         {
//             EditorGUILayout.PropertyField(nextNodeProp);
//             EditorGUILayout.PropertyField(NPCnameProp);
//         }
//         else
//         {
//             // default DialogueNode
//             EditorGUILayout.PropertyField(optionsProp, true);
//         }

//         // Apply changes to the serialized object
//         serializedObject.ApplyModifiedProperties();
//     }
// }
