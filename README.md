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
### The objects will persist through different runs of the game but there will be a function that can be called to either remove all of the objects
### or to just remove a specified object.
