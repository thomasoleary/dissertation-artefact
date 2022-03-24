## Computing Artefact ### 

This is the repository for my COMP360 Computing Artefact.

This artefact will help me answer the question of "*Can an Unknowing Participant distinguish between Multi-Agent Designed and Human Designed Interiors?*"


### Current Status of Artefact
+ The Artefact is able to create a room's layout at runtime using Agents that are in the scene.

+ Agents are able to find suitable Parent's within a scene (if it needs a parent).
+ Agents are able to arrange themselves with their Parent accordingly.
+ Once positioned and not colliding with other agents, the Agent rests.

#### Current Bugs
+ No Collision checks with floors or walls.
+ Agents that can be placed on walls do not orient themselves correctly.
+ If an agents' parent moves due to a collision, the childed agent does not move accordingly.

#### Assets Used
[Apartment Kit by Brick Project Studio](https://assetstore.unity.com/packages/3d/props/apartment-kit-124055)
