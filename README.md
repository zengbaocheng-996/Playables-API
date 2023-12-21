Animation Clip terms

|                  |                                                              |
| ---------------- | ------------------------------------------------------------ |
| Animation Clip   | Animation data that can be used for animated characters or simple animations. It is a simple "unit" piece of motion, such as (one specific instance of) "Idle", "Walk", "Run" |
| Animation Curves | Curves can be attached to animation clips and controlled by various parameters from the game |
| Avatar Mask      | A specification for which body parts to include or exclude for a skeleton. Used in Animation Layers and in the importer |

Avatar terms

|                   |                                                              |
| ----------------- | ------------------------------------------------------------ |
| Avatar            | An interface for retargeting one skeleton to another         |
| Retargeting       | Applying animations created for one model to another         |
| Rigging           | The process of building a skeleton hierarchy of bone joints for your mesh. Performed with an external tool, such as Autodesk 3ds Max or Autodesk Maya |
| Skinning          | The process of binding bone joints to the character's mesh or 'skin'. Performed with an external tool, such as Autodesk 3ds Max or Autodesk Maya |
| Muscle definition | This allows you to have more intuitive control over the character's skeleton. When an Avatar is in place, the Animation system works in muscle space, which is more intuitive than bone space |
| T-pose            | The pose in which the character has their arms straight out to the sides, forming a "T". The required pose for the character to be in, in order to make an Avatar |
| Bind-pose         | The pose at which the character was modelled                 |
| Human template    | A pre-defined bone-mapping. Used for matching bones from FBX files to the Avatar |
| Translate DoF     | The three degrees-of-freedom associated with translation (movement in X, Y & Z) as opposed to rotation |

Animator and Animator Controller terms

|                         |                                                              |
| ----------------------- | ------------------------------------------------------------ |
| Animator Component      | Component on a model that animates that model using the Animation system. The component has a reference to an Animator Controller asset that controls the animation |
| Root Motion             | Motion of character's root, whether it's controlled by the animation itself or externally |
| Animator Controller     | The Animator Controller controls animation through Animation Layers with Animation State Machines and Animation Blend Trees, controlled by Animation Parameters. The same Animator Controller can be referenced by  multiple models with Animator components |
| Animation Window        | The window where the Animator Controller is visualized and edited |
| Animation Layer         | An Animation Layer contains an Animation State Machine that controls animations of a model or part of it. An example of this is if you have a full-body layer for walking or jumping and a higher layer for upper-body motions such as throwing an object or shooting. The higher layers take precedence for the body parts they control |
| Animation State Machine | A graph controlling the interaction of Animation States. Each state references an Animation Blend Tree or a single Animation Clip |
| Animation Blend Tree    | Used for continuous blending between similar Animation Clips based on float Animation Parameters |
| Animation Parameters    | Used to communicate between scripting and the Animator Controller. Some parameters can be set in scripting and used by the controller, while other parameters are based on Custom Curves in Animation Clips and can be sampled using the scripting API |
| Inverse Kinematics      | The ability to control the character's body parts based on various objects in the world |

