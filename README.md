# gameEnginesProject
Game Engines Project for Semester 1

Project of Jamie Mccarthy | C17405366 | DT508 | 3rd Year

In this project i will be aiming to create an entity that will travel around a set point or object 
that can be placed in the world, so as to make it easy for the project to be used as a downloadable
assest, similar to those in the Unity Asset Store. The creature itsef I aim to have generated
somewhat randomly within parameters that will be set by the user using the Unity Editor, as such it
will need to be user friendly.

This random geneartion is donevia inputting maximum values for the patrol area, linking up a "Waypoint"
prefab and then specifuying how big and fast the "Stalker" and "Follower" enetitys should be.

As for the terrin present in the scene as it stands currently, it is intended to resemblt the sea bed, 
and as such it is made using Mathf.PerlinNoise as it allows for a seemingly random hightmap to be applied
to the terrain giving it an unpredictable style. This can also be made to fit with my self imposed need 
for the project to be user friendly by having each element of the generation tied to a serialised private 
variable, as shown in the Demo video included below. This allowes for any users of this code to change the 
appearance esialy and quickly, while the use of private variable can prevent any overlap with their pre 
existing code in the project.

I am most proud of the fact that the "Stalker" entity and all code realted to it and the spowning if its 
orbiters throug harrays being created from a singular scrpt using only on prefab overall, as this is somehting 
I do not usually manage to pull off in my projets, as I often rely on numerous scripts with shared vaiables to 
accomplish complex tasks such as this.

My main inspiration for this idea is some of the creatures in Infinite Forms, however I would like to
create something that is not too similar to them, and not too disimilar either, in fitting with the
brief

Demo Video (2.02 mim)

[![YouTube](https://img.youtube.com/vi/oLFesGdNe80/0.jpg)](https://www.youtube.com/watch?v=oLFesGdNe80)

I have yet to source many tutorials for this project work, instead working based on my exosting unity 
experience. Any tutorials I use will be pasted below.

Perlin noise tutorial to refresh basics

[![YouTube](https://img.youtube.com/vi/gdSFs0PeBNQ/0.jpg)](https://www.youtube.com/watch?v=gdSFs0PeBNQ)
