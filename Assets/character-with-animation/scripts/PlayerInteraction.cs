// using UnityEngine;
// using System.Collections.Generic;

// public class PlayerInteraction : MonoBehaviour
// {
//     [Header("Interaction Settings")]
//     [SerializeField] private float interactionRange = 3f;
//     [SerializeField] private LayerMask interactionMask;
//     [SerializeField] private Transform interactionPoint;
    
//     [Header("Environmental Tools")]
//     [SerializeField] private float cleaningRadius = 2f;
//     [SerializeField] private float treeGrowthTime = 3f;
    
//     private Camera mainCamera;
//     private bool isPerformingAction;
//     private Dictionary<string, int> collectedResources = new Dictionary<string, int>();
    
//     private void Start()
//     {
//         mainCamera = Camera.main;
//     }
    
//     private void Update()
//     {
//         HandleInteraction();
//     }
    
//     private void HandleInteraction()
//     {
//         if (isPerformingAction) return;
        
//         Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
//         RaycastHit hit;
        
//         if (Physics.Raycast(ray, out hit, interactionRange, interactionMask))
//         {
//             // Show interaction prompt
//             if (Input.GetKeyDown(KeyCode.E))
//             {
//                 InteractWithObject(hit.collider.gameObject);
//             }
//         }
//     }
    
//     private void InteractWithObject(GameObject obj)
//     {
//         switch (obj.tag)
//         {
//             case "PollutedArea":
//                 StartCleaningArea(obj);
//                 break;
//             case "PlantableGround":
//                 StartPlantingTree(obj);
//                 break;
//             case "CollectibleResource":
//                 CollectResource(obj);
//                 break;
//             case "Animal":
//                 HelpAnimal(obj);
//                 break;
//         }
//     }
    
//     private void StartCleaningArea(GameObject pollutedArea)
//     {
//         isPerformingAction = true;
//         // Animation and particle effects would be triggered here
        
//         // After cleaning animation
//         Invoke(nameof(FinishCleaning), 2f);
//     }
    
//     private void StartPlantingTree(GameObject ground)
//     {
//         if (collectedResources.ContainsKey("Seeds") && collectedResources["Seeds"] > 0)
//         {
//             isPerformingAction = true;
//             // Tree planting animation and instantiation would occur here
//             collectedResources["Seeds"]--;
            
//             Invoke(nameof(FinishPlanting), treeGrowthTime);
//         }
//     }
    
//     private void CollectResource(GameObject resource)
//     {
//         string resourceType = resource.GetComponent<ResourceType>().type;
//         if (!collectedResources.ContainsKey(resourceType))
//         {
//             collectedResources[resourceType] = 0;
//         }
//         collectedResources[resourceType]++;
//         Destroy(resource);
//     }
    
//     private void HelpAnimal(GameObject animal)
//     {
//         // Animal helping mechanics would go here
//         // Could involve healing, feeding, or freeing from pollution
//     }
    
//     private void FinishCleaning()
//     {
//         isPerformingAction = false;
//         // Update environment state
//     }
    
//     private void FinishPlanting()
//     {
//         isPerformingAction = false;
//         // Complete tree growth
//     }
// }