# Guide

## Set up the component
### You need to add the component to your project using the Package Manager. Open the Package Manager (Windows > Package Manager), click on the + icon and select “Add package from git URL...” and enter:

### https://github.com/itcOnlineGaming/GA_25_P3_Darragh_McKernan

### Note that the URL specifies the complete path to the package and a git tag. The package should now be visible in your project.

## Using the Prefab Creator and Loader
### A package that can be used to save a collection of gameobjects as 1 singular gameobject.
### The saved gameobjects are stored in the package and can be loaded at will when they are needed.
### This is especially useful when a player is able to build something as it can be saved as stored so they can reuse the built object later on.

# Setting up the package
## Saving an Object
### Attach the package script (put link here) to a gameoject in your scene (preferably a GameManager).
### Attach the gameobject containing the package script to a button so that the user can save the gameobject that you want.
### A UI element will also be included in the package that if the player types into it they can give their saved object a name.
### This can be useful if the player has saved a large amount of objects so that they can easily find it by name. 

## Loading a saved object
### You can also optionally add in a button to load in one of the previously saved objects, these will be stored as a list so you can easily access all of them.
### The objects will not persist through different runs of the game but there will be a function that can be called to remove all of the objects.
![image](https://github.com/user-attachments/assets/66119f52-cfd0-4b87-a800-8a31a7dbea51)
### In the demo you can see that an object can be saved either by specifying the name of the object or the tag that is on the object.
### You must use an input field in order to allow for saving annd it can use either a tag or name of object as the component will look for both in the scene.
### When objects are saved they can be automatically applied to a drop down menu allowing users to easily access a large amount of saved objects at once.
### You can provide a text box to allow users to name their own objects and these will be the names they use to load the object making it easy for your user.
### You can allow users to load a specific object by name if you add in a text box and apply it to the fields in the script.
