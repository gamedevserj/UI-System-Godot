# UI-System-Godot

UI system for Godot that uses Dependency Injection and MVC patterns.  

![Example](https://raw.githubusercontent.com/gamedevserj/Images-For-Repo/main/UiSystemGodot/UISystemGodot.gif)  

## Menu system
The repo has some menus created: **Main, Options, Pause, Audio/Video settings, Interface settings, Key rebinding**   

To add new menu:
1. Add your menu type to the MenuType enum
2. Create your menu view script that either inherits from MenuView (if you want to make a custom transition) or from MenuViewFade (that has simple fade as a transition)
3. If your view has interactable elements (buttons, sliders, etc.), they should have scripts attached to them that implement IFocusableControl to disable elements during menu transitions. Some of the elements already included in the repo at UISystem/Common/Elements, there are also prefabs for them in the UISystem/Common/Prefabs, so you can use those
4. Create your menu model script that implements IMenuModel interface (it is just a marker interface)
5. Create your menu controller sctipt that inherits from MenuController providing your view and model
6. In your menu controller implement the MenuType property by providing your menu type and implement SetupElements()
7. Create path to your view prefab in MenuViewsPaths
8. In MenusManager Init() create your menu controller and add it to the array that is passed to the AddControllers()
9. After that you should be able to call _menusManager.ShowMenu(...) to show your new menu

### Menu background controller
A simple script that handles menu's background, look at MainMenu and PauseMenu controllers for example.

### ℹ️: Example menus notes

#### Audio, video, and interface menus are setup to have a popup if some settings were not saved before quitting. Key rebinding menu saves binds when new key is assigned.

#### Rebinding menu example  
  
To create key binding that player can rebind you need to do the following:
1. Create your actions in InputMap
2. Create a property in InputsData and add it to the RebindableActions array
3. Add rebindable button views in the menu view and set them up in the controller using existing buttons as example
#### ⚠️ The example rebinding menu uses image names from https://kenney.nl/assets/input-prompts, but they are not included in this repository. You'll need to download them separately.

## Popup system
Some of the popups are already setup and some menus already use them. The steps to create a new type of popup are the same as creating a new menu. 
If you want to show a popup when some event occurs you can either create an event and subscribe PopupsManager to that event. Or you can add a static Instance property and call it directly, PopupsManager and MenusManager already part of UiInstaller which is a singleton, so you don't need to add it to the list of autoloads.  
#### ⚠️ If you have interactable elements in your popup they should also implement IFocusableControl to disable them during transition

## Screen fade
A simple script that controls fading, call FadeOut() with an optional action as a prarmeter that you want to happen when screen is completely black.
