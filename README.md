# Guide

## Set up the component
### You need to add the component to your project using the Package Manager. Open the Package Manager (Windows > Package Manager), click on the + icon and select “Add package from git URL...” and enter:

### https://github.com/itcOnlineGaming/GA_25_P3_Darragh_McKernan.git?path=/Packages/ie.mypackage.prefabcreatorandloader#v1.0.7

### Note that the URL specifies the complete path to the package and a git tag. The package should now be visible in your project.

## Using the Prefab Creator and Loader
### A package that can be used to save a collection of gameobjects as 1 singular gameobject.
### The saved gameobjects are stored in playerprefs and can be loaded as needed.
### This is especially useful when a player is able to build something as it can be saved and stored so they can reuse the built object later on.

![image](https://github.com/user-attachments/assets/961ccd78-91c4-427f-bb19-30c592a93dd4)
# Setting up the package
## Saving an Object
### You can save an object either by name or tag using the function SaveObjectByInput() in the script ObjectSaveUI.cs
### You provide an inputfield so that you can use that to pick what you want to save by tag or name.
### If you provide an additional inputfield you can use that to set the name of the object that it will be saved as.

## Loading a saved object
### You MUST have a folder structure like this.
![image](https://github.com/user-attachments/assets/5f3d683c-7986-4f34-b522-1faa4c1c2960)
### To be able to rebuild gameobjects you must provide prefabs for each child object that makes up the gameobject.
### Make sure you attatch all important scripts and components to the prefab for when it is loaded.
### You can also optionally add in a button to load in one of the previously saved objects.
### If you add in a button you can add in another inputfield for the user to load an object by name.
### Additionally a dropdown menu can be used to display all of the saved objects in an easy to access menu so the user can see all of the objects and pick whatever one they want.
### A button will still be required to load objects that were chosen from the dropdown.

## The Demo
### In the demo you can see that an object can be saved either by specifying the name of the object or the tag that is on the object.
### You can also choose the name to save the object as but if you dont it will simply save as the name of the GameObject
### You can also provide an input box so that the user can load an object by name if they want.
### You can save objects you want using the functions provided and calling them using buttons or using your own scripts.
## Step by Step Demo Guide
## Saving
### There are 2 ways to save a Gameobject: save by using the name of the GameObject or the Tag
![image](https://github.com/user-attachments/assets/fa712251-c0a6-4e20-bd3b-aa453abac067)
### In the demo you can see the 2 objects labelled for you
### Obj is the name of the GameObject
### Tag is the tag of the GameObject
![image](https://github.com/user-attachments/assets/d85062bd-a250-4389-a638-9500433b8b66)
### You can also optionally give a name to the GameObject that you are saving
![image](https://github.com/user-attachments/assets/44893280-7c55-4201-8588-abab441302da)
### If you do not provide a name it will simply be named after the name of the GameObject in the scene
## Loading
### There are 2 ways to load a saved GameObject
### Loading by name
### If you provide a name in the name search input and press teh load button beside it the package will search for a gameobject saved under that name and load it into the scene if one is found.
![image](https://github.com/user-attachments/assets/6014680f-0636-41a1-b89e-b22dbf0a8b4c)
### When you save a GameObject it will automatically populate the dropdown list and you can easily load from there.
### All you have to do is click on an entry on the list and then press the load button beside it and it will load the chosen GameObject into the scene.
![image](https://github.com/user-attachments/assets/eda5f9d9-853a-4582-b5b4-7b9cbdb2ed1a)
### The final button called Clear Prefabs will allow you do delete all of the saved GameObjects and will automatically wipe the dropdown list too. 
![image](https://github.com/user-attachments/assets/4f1c9ed6-a967-462f-9b44-dc01c1f7f126)
