# UI-System-Godot

UI system for Godot that uses Dependency Injection and MVC patterns.  

![Example](https://raw.githubusercontent.com/gamedevserj/Images-For-Repo/refs/heads/main/UiSystemGodot/UISystemGodotAnimated.mp4)  

## Menu system
The repo has some menus created: **Main, Options, Pause, Audio/Video settings, Interface settings, Key rebinding**   

To add new menu:
1. Add your menu type to the MenuType class
2. Create your menu view script that either inherits from BaseWindowView or from SettingsMenuView (it has functionality to reset setting to default via model and reset view to default)
3. If your view has interactable elements (buttons, sliders, etc.), they should have scripts attached to them that implement IFocusableControl to disable elements during menu transitions. Some of the elements already included in the repo at UISystem/Common/Elements, there are also prefabs for them in the UISystem/Common/Prefabs, so you can use those
4. Create your menu model script that implements IMenuModel interface (it is just a marker interface) or ISettingsMenuModel (which has methods to save, discard and reset to default)
5. Create your menu controller script that inherits from MenuController providing your view and model
6. In your menu controller implement the MenuType property by providing your menu type and implement SetupElements()
7. Create path to your view prefab in MenuViewsPaths
8. Add your menus to the menus array created in the UiInstaller that passes them to the MenusManager
9. After that you should be able to call _menusManager.ShowMenu(...) to show your new menu

### Menu background controller
A simple script that handles menu's background, look at MainMenu and PauseMenu controllers for example.

### ℹ️: Example menus notes

Audio, video, and interface menus are setup to have a popup if some settings were not saved before quitting. Key rebinding menu saves binds when new key is assigned that's why it has empty Save and DiscardChanges methods. If you want to save changes only when pressing save button, you need to make some changes - look at audio/video settings for example how it can be done.

#### Rebinding menu example  
  
To create key binding that player can rebind you need to do the following:
1. Create your actions in InputMap
2. Create a property in InputsData and add it to the RebindableActions array
3. Add rebindable button views in the menu view and set them up in the controller using existing buttons as example
4. 
 ⚠️ The example rebinding menu uses image names from https://kenney.nl/assets/input-prompts, but they are not included in this repository. You'll need to download them separately.

## Popup system
Some of the popups are already setup and some menus already use them. The steps to create a new type of popup are the same as creating a new menu. 
If you want to show a popup when some event occurs you can either create an event and subscribe PopupsManager to that event. Or you can add a static Instance property and call it directly, PopupsManager and MenusManager already part of UiInstaller which is a singleton, so you don't need to add it to the list of autoloads.  

 ⚠️ If you have interactable elements in your popup they should also implement IFocusableControl to disable them during transition

## Screen fade
A simple script that controls fading, call FadeOut() with an optional action as a prarmeter that you want to happen when screen is completely black.  

## Transitions  
Transition control the way view is shown/hidden. The repo includes few transitions as example. Menu elements should implement ITweenableMenuElement to reset hover before starting transition if transition changes are modifying the same properties as hovering tween.

## Hovering  
Menu elements have settings resources that allow to customize the way element is displayed when hovered over/focused.  

Follow these steps if text/icons on your buttons jitter when you change size from center
1. Set content to be in the center and make sure the position is an integer, it is best to have position to be at 0. The easies way to do that - set anchors using the "Full rect" preset, and then move them to center manually
2. In your size hover settings set values to be even numbers. This is required since resizing from center moves the control by half of the increased size
3. ResizableControlView needs to be parented to object that is parented to BoxContainer if its global transition is not from top left, otherwise it doesn't behave correctly.

Use example prefabs in UISystem/Common/Prefabs as a guide for your elements.

## 3D 
The setup is used from the official example for UI in 3D space from here. The billboard parts were not included.
https://github.com/godotengine/godot-demo-projects/blob/master/viewport/gui_in_3d/gui_3d.gd

In order to get your menus to appear in 3D space you'll need to set it's parent to be guiPanel3D.menusParent in the controller's constructor in UIInstaller's Init method. 
The default setup makes it possible for 3D objects to visually block the UI, acting similarly to Unity's canvas render mode - "world space". If you want it to not be obscured by the world object (and be similar to Unity's canvas render mode "screen space - camera") there is an example scene ExampleViewportsFor3D.tscn with subviewports setup to show how it could be done.

Popups can also be displayed in 3D, the setup is the same.

 ⚠️ There are two errors that will be displayed when you run the project regarding the Viewport texture, but they don't affect the game as far as I can tell.
 You can read about it here https://github.com/godotengine/godot/issues/66247
