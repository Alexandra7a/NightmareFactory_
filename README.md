# NightmareFactory in a nutshell
Hi there, this is a project I've worked on in Unity using C#, a 2D puzzle style game. It was made from scratch, including the graphics, which were created in Photoshop. It follows the adventure of a little girl lost in a dream world, actually a nightmare world, where all her fears come true. She is fighting to go forward and pass all the obstacles and regain the light which symbolizes the morning. The game's style is black and white, the characters being designed in form of a shadow.
  

![image](https://github.com/Alexandra7a/NightmareFactory_/assets/63046754/62260d84-5b4d-4c66-861e-19a7ce4d8864)
# Why Unity? 
I often got fascinated by the game industry and asked myself: ‘How are these games created?’. I searched and discovered one of many game engines out there, Unity. At first it looked complicated, but once I dived in, the structure became clear and my performance increased.

# Technical details and Implementation
This section covers different aspects of the creation process of the game: the front-end part consisting of animations, and back-end depicting the way some aspects of the project work.
## Animations
Unity facilitates this aspect by having a built-in animation system. Fist of all, the charater needs to have some 'bones' to be able to move. The motion is taken frame by frame adjusting the body parts of the character. 
<img src="https://github.com/Alexandra7a/NightmareFactory_/assets/63046754/b006e39b-5582-4ec2-841a-4cbf472a2866">

## Scripts
Some samples regarding the scrips used in the project.
### Handleing buttons
When the New Button is clicked the game opens with a new scene(the second scene of the game)
```c#
    void NewGame() { SceneManager.LoadScene(2); //loads the second scene }
 ```
The continue button has two separate actions: unresponsive when the game is freshly started and responsive in case the user came back to play again.
```c#
    void Continue()
    { 
      if (first_scene_player.firstOpening == 3) // a variable responsible to check the NewGame button clicked
      { SceneManager.LoadScene(save.IndexScene());// save.Index returns the player's current scene } 
      else
      {
        // the buttion is not responsive because the game was never started
        b2_color = yourButton2.GetComponent<Button>().colors; 
        b2_color.normalColor = Color.gray; 
        yourButton2.GetComponent<Button>().colors = b2_color; }
    }
```


# Some scenes from the game
<div>
  <img src="https://github.com/Alexandra7a/NightmareFactory_/assets/63046754/9db7ed7f-e3ee-4657-9459-4fa05d253a7d" width="30%" height="10%"/>
  <img src="https://github.com/Alexandra7a/NightmareFactory_/assets/63046754/1f64a3fd-f9d3-4272-b6fe-df503980b2c0" width="30%" height="10%"/>
   <img src="https://github.com/Alexandra7a/NightmareFactory_/assets/63046754/39893f9d-1698-4c2b-86f1-635535cba0e9" width="30%" height="10%"/>
  <img src="https://github.com/Alexandra7a/NightmareFactory_/assets/63046754/4d992a1c-7464-4f8c-9c02-9f6ec42d9f58" width="30%" height="10%"/>
  <img src="https://github.com/Alexandra7a/NightmareFactory_/assets/63046754/9620ea0a-3e62-46c6-9bb1-166b4bb57fff" width="30%" height="10%"/>
  <img src="https://github.com/Alexandra7a/NightmareFactory_/assets/63046754/450e9285-b9a0-425a-9b29-b818a469e733" width="30%" height="10%"/>
</div>
