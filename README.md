# Guide

## Set up the component
### You need to add the component to your project using the Package Manager. Open the Package Manager (Windows > Package Manager), click on the + icon and select “Add package from git URL...” and enter:

### https://github.com/itcOnlineGaming/GA_25_P3_Darragh_McKernan.git?path=/Packages/ie.mypackage.prefabcreatorandloader#v1.0.7

### Note that the URL specifies the complete path to the package and a git tag. The package should now be visible in your project.

## Using the Prefab Creator and Loader
### A package that can be used to save a collection of gameobjects as 1 singular gameobject.
### The saved gameobjects are stored in the package and can be loaded at will when they are needed.
### This is especially useful when a player is able to build something as it can be saved as stored so they can reuse the built object later on.

# Setting up the package
## Saving an Object
### You can only save a prefab while in the editor so this is only for developer use. After the objects are saved the user can load them even in a build.
### Attach the package script from the package to a gameoject in your scene (preferably a GameManager).
### Attach the gameobject containing the package script to a button so that the user can save the gameobject that you want.
### A UI element will also be included in the package that if the player types into it they can give their saved object a name.
### Make sure that each button you add calls on the functions in thescript to save and load. You MUST provide inout boxes for the users to be able to save objects.
### You MUST have a folder structure like this.
![image](https://github.com/user-attachments/assets/d15d471d-2d65-4c77-aa5d-6404f599dc30)
### Assets/Resources/SavedPrefabs
### All prefabs will be saved and loaded in the folder SavedPrefabs.
### You can add in your own prefabs here by dragging them in or you can run your game and call the save button to use your own game to genereate prefabs to use. This is especially useful if you have a system to build prefabs such as cars.

## Loading a saved object
### You can also optionally add in a button to load in one of the previously saved objects, these will be stored as a list so you can easily access all of them.
### A dropdown menu can be used to display all of the saved objects in an easy to access menu so the user can see all of the objects and pick whatever one they want.
![image](https://github.com/user-attachments/assets/961ccd78-91c4-427f-bb19-30c592a93dd4)

### In the demo you can see that an object can be saved either by specifying the name of the object or the tag that is on the object.
### You can also provide an input box so that the user can load an object by name if they want.
### You can save objects you want into the folder using the functions provided and calling them using buttons or using your own scripts.
### If you have any other prefabs you want the player to be able to use you can simply drop prefabs into the Assets/Resources/SavedPrefabs folderand the script will automatically pick them up.
