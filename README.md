# Refined Physics for Sonic Frontiers 🏃‍♂️

I like the vanilla physics of Sonic Frontiers, but holy shit the most annoying thing about it is when I let go of RT when power boosting, and Sonic practically loses all his prior speed. The inital goal was to create a mod that addresses that singular issue, but now I aim to create yet another physics mod that pivots the game towards momentum-based movement 🙄

I know, I know. It's an oversaturated mod niche for this game, but the thing that's bothered me about all the other physics mods is that it deviates a bit too much from the vanilla feel. Some have Sonic's base running speed at a crazy high amount, some make it a bit way too difficult for Sonic to simply walk up a hill, and some just simply don't feel 100% to my liking. I want this mod to feel a good amount like vanilla movement, but with refinement and quality of life changes.

At this _extremely_ early stage, I just wanna mess with the physics. However, I would like to mess with Sonic's combat and the control layout in the future.

## Current Changes 🔵

- Sonic's deceleration has been drastically reduced; his speed cap is practically gone, allowing the player to gain speed running downhill without boost
- Sonic's falling deceleration has been drastically reduced as well
- Grinding has a lower minimum speed, and deceleration has been reduced to let Sonic gain/lose speed when going upwards/downwards on a rail
- The boost gauge now depletes within 2 seconds or so, making the boost more of a dash ability now
- The air boost now acts similarly to Sonic Generations
- Max parry time has been reduced to 0.3s so it could be more challenging (added here for funsies)
- The lightspeed dash no longer brings Sonic to a halt after dashing through a trail
- Letting go of the boost while wall running no longer brings Sonic to an instant halt
- Decreased minimum slide speed
- Modified spin boost (vanilla spin dash) max speed and deceleration
- Made spin boost flinging better
- Increase Sonic's jump speed limit
- Spin charge actually determines the initial speed of the spin boost (10 charges = full speed)
- Spin dash speed without charging matches Sonic's prior running speed

### Future Changes ⏩

- Spin charging mid-air doesn't kill forward momentum
- Detach spin boost from boost gauge maybe???

## Issues 🔴

- Can't spin charge/spin boost when the boost gauge is depleted
  - Might consider this a feature to incentive players to use the boost and spin boost strategically???

## Recommendations ✨

<details>
  <summary>Mandatory <a href="https://github.com/thesupersonic16/HedgeModManager">HedgeModManager</a> codes</summary>
  
  ```
  // Camera
  Disable Spin Charge Camera

  // Cheats
  Always Unlocked Spin Dash
  
  // Fixes
  Literally everything in this category
  
  // Gameplay
  Allow Attacking from Stomp Bounce
  Disable Loop Kick on Slide
  Allow Spin Dash on Dash Panels
  Always Trickable Spin Dash Exit
  Disable Drop Dash
  
  // Physics
  Literally everything in this category
  ```
</details>

<details>
  <summary>Recommended in-game physics settings</summary>
  
  ```
  // Only what to change
  
  Starting Speed: 15
  Acceleration: 15
  Jump Deceleration: 25 (I'm pretty sure the "Retain Ground Velocity for Jump" HMM code overrides this, but I have it set to this anyway)
  Deceleration Rate: 15
  ```
</details>

## Development ⚙️

Here are the resouces I used to develop this project!

- [HedgeArcPack CLI tool by Radfordhound](https://github.com/HedgeDocs/HedgeDocs.github.io/releases) to unpack/repack the `playercommon.pac` file from the game itself. This file holds a bunch of shit related to the character mechanics.
- [010 Editor](https://www.sweetscape.com/010editor/) to edit the hex code within the `player_common.rfl` file, which you get from unpacking `playercommon.pac`. The RFL file contains hex code for Sonic's movement, combat, etc.
- [`SonicParameters.bt` RFL template by Skyth](https://github.com/blueskythlikesclouds/RflTemplates/blob/master/SonicFrontiers/Uncategorized/SonicParameters.bt) to actually modify `player_common.rfl` without having to figure out which hex codes do what and making me lose my god damn braincells
- [HedgeDocs](https://hedgedocs.com) as have some sort of guide to figure out wtf to do (I still have no idea what I'm doing lmao)
- [Tracker's Physics Tweaks](https://gamebanana.com/mods/415617) to see what parameters are modified to figure out what to actually mess with in `player_common.rfl`. Cool physics mod btw!
- [Legacy Spin Dash by Weezley](https://gamebanana.com/mods/462772) to figure out how creating HMM codes work, and to figure out how to implement charging for the vanilla spin charge. Great spin dash mod!
- [`SonicFrontiers.hmm` from the HedgeModManager repo](https://github.com/thesupersonic16/HedgeModManager/blob/rewrite/HedgeModManager/Resources/Codesv2/SonicFrontiers.hmm) to find the methods and libraries I needed to write my `spinboost.hmm` code

<details>
  <summary>The specific values I changed in <code>player_common.rfl</code></summary>
  
  ```
  // Using Skyth's RFL template
  
  // REFERENCE
  path > to > parameters
  - parameter: vanilla value > value I changed it to
  
  // The following parameters are found within the path: sonicParameters > forwardView
  
  modePackage > speed > decele
  - force: 60 > 5
  - force2: 60 > 5
  - damperRange: 15 > 10
  
  modePackage > fall
  - deceleForce: 20 > 5
  - overSpeedDeceleForce: 40 > 10
  
  modePackage > grind
  - maxSpeed: 30 > 60
  - deceleForce: 30 > 10
  - limitMinSpeed: 15 > 5
  
  modePackage > boost
  - consumptionRate: 6 > 100
  - recoveryRate: 30 > 15
  - recoveryByAttack: 3 > 0
  - infinityBoostTime: 180 > 60
  
  modePackage > airboost
  - startHSpeed: 25 > 40
  - startHSpeedMax: 50 > 60
  - minHSpeed: 20 > 40
  - minHSpeedMax: 40 > 60
  - brakeTime: 0.5 > 0
  - minKeepTime: 0.1 > 0
  - maxKeepTime: 0.5 > 0
  - maxTime: 2 > 0
  
  modePackage > parry
  - minRecieveTime: 0.2 > 0.15
  - maxRecieveTime: 15 > 0.3
  - justEffectTime2: 5 > 1
  
  modePackage > wallmove
  - brake: 100 > 15
  
  modePackage > sliding
  - minSpeed: 25 > 5
  
  modePackage > jumpSpeed
  - limitUpSpeed: 20 > 40
  
  lightDash
  - brake: 200 > 75
  
  spinBoost
  - initialRunTime: 0.5 > 0.2
  
  spinBoost > speedBoost
  - initialSpeed: 65 > 75
  - maxSpeed: 55 > 3
  - minTurnSpeed: 20 > 30
  
  speedBoost > decele
  - force: 40 > 10
  - force2: 40 > 10
  
  speedBoost > deceleNeutralMin
  - force: 1500 > 750
  
  speedBoost > deceleNeutralMax
  - force: 2000 > 1000
  ```
</details>

***

Literally shout out to [HyperBE32](https://github.com/HyperBE32), Weezley, [Radfordhound](https://github.com/Radfordhound), [Tracker](https://github.com/TrackerTD), and everyone involved with creating the resources I used. Without them, none of this would be possible.
