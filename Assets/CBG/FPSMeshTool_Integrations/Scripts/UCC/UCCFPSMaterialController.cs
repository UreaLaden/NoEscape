#if FIRST_PERSON_CONTROLLER && THIRD_PERSON_CONTROLLER
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Opsive.Shared.Events;
using Opsive.UltimateCharacterController.Character;
using Opsive.UltimateCharacterController.FirstPersonController.Camera;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CBG {

    public class UCCFPSMaterialController : FPSMaterialController {
        private UltimateCharacterLocomotion m_Locomotion;
        private UltimateCharacterLocomotion Locomotion {
            get {
                if (m_Locomotion == null) {
                    m_Locomotion = GetComponent<UltimateCharacterLocomotion>();
                }
                return m_Locomotion;
            }
        }

        private MaterialSwapper[] materialSwappers;
        
#if UNITY_EDITOR

        protected override void FindInvisibleMaterial() {
            var perspectiveMonitor = GetComponent<Opsive.UltimateCharacterController.ThirdPersonController.Character.PerspectiveMonitor>();
            if (perspectiveMonitor) {
                invisibleMaterial = perspectiveMonitor.InvisibleMaterial;
            } else {
                base.FindInvisibleMaterial();
            }
        }

#endif

        private void OnRespawn() {
            UpdatePerspective();
        }

        private void UpdatePerspective() {
            OnPerspectiveChanged(Locomotion.FirstPersonPerspective);
        }
        
        private void OnEnable() {
            EventHandler.RegisterEvent<ILookSource>(gameObject, "OnCharacterAttachLookSource", OnAttachLookSource);
            EventHandler.RegisterEvent<bool>(gameObject, "OnCharacterChangePerspectives", OnPerspectiveChanged);
            EventHandler.RegisterEvent(gameObject, "OnRespawn", OnRespawn);
            UpdatePerspective();
        }

        private void OnDisable() {
            EventHandler.UnregisterEvent<ILookSource>(gameObject, "OnCharacterAttachLookSource", OnAttachLookSource);
            EventHandler.UnregisterEvent<bool>(gameObject, "OnCharacterChangePerspectives", OnPerspectiveChanged);
            EventHandler.UnregisterEvent(gameObject, "OnRespawn", OnRespawn);
            UnregisterWithMaterialSwappers();
        }

        void OnAttachLookSource(ILookSource lookSource) {
            UnregisterWithMaterialSwappers();
            if (lookSource != null) {
                RegisterWithMaterialSwappers(lookSource.GameObject);
            }
        }
        
        void RegisterWithMaterialSwappers(GameObject rootObject) {
            materialSwappers = rootObject.GetComponentsInChildren<MaterialSwapper>();
            foreach (var swapper in materialSwappers) {
                swapper.OnEnableFirstPersonMaterials += SetFirstPerson;
                swapper.OnEnableThirdPersonMaterials += SetThirdPerson;
            }
        }

        void UnregisterWithMaterialSwappers() {
            if (materialSwappers == null) return;
            foreach (var swapper in materialSwappers) {
                swapper.OnEnableFirstPersonMaterials -= SetFirstPerson;
                swapper.OnEnableThirdPersonMaterials -= SetThirdPerson;
            }
            materialSwappers = null;
        }

    }
}
#endif