# Refined Physics for Sonic Frontiers 🏃‍♂️

I like the vanilla physics of Sonic Frontiers, but the most annoying thing about it is when I let go of RT when power boosting, and Sonic practically loses all his prior speed. The inital goal was to create a mod that addresses that singular issue, but here I am.. with yet another physics mod.

I know, I know. It's an oversaturated mod niche for this game, but the thing that's bothered me about all the other physics mods is that it deviates a bit too much from the vanilla feel. Some have Sonic's base running speed at a crazy high amount, some make it a bit way too difficult for Sonic to simply walk up a hill, and some just simply don't feel 100% to my liking. I want to modify the movement as minimally as possible, and focus on refining what's already good (with some extras sprinkled in).

## Changes 🔵

> Players can opt-in or out of any changes I made through the configuration menu!

<details>
  <summary><b>Decreased Ground Deceleration</b></summary>

  Decreases Sonic's deceleration on ground, allowing him to gain speed naturally without boosting and to prevent instant momentum loss after boosting/power boosting.
</details>

<details>
  <summary><b>Decreased Falling Deceleration</b></summary>

  Decreases Sonic's deceleration in air, allowing him to retain his forward momentum while falling uncurled.
</details>

<details>
  <summary><b>Momentum Rails</b></summary>

  Makes rails more momentum-based, allowing Sonic to gain/decrease speed when going up/down rails.

  Additionally, the minimum rail grinding speed has been decreased, and maximum rail speed is increased.
  
  This does NOT remove Sonic's minimum rail speed.
</details>

<details>
  <summary><b>Generations-Styled Air Boost</b></summary>

  Makes the air boost act similar to the one in Sonic Generations.

  Keep `Decreased Falling Deceleration` enabled for a better air boosting experience.
</details>

<details>
  <summary><b>Brake Easing for Wall Running</b></summary>

  Makes Sonic ease into a stop when letting go of the boost button while wall running.
</details>

<details>
  <summary><b>Decreased Minimum Slide Speed</b></summary>

  Decreases the minimum sliding speed.
</details>

<details>
  <summary><b>Softer Light Speed Dash Brake</b></summary>

  Prevents Sonic from coming to an instant stop after light speed dashing.
</details>

<details>
  <summary><b>Easier Incline Jumps</b></summary>

  Makes incline jumps easier.
</details>

<details>
  <summary><b>Better Spin Boost</b></summary>

  Inspired by Weezley's [Legacy Spin Dash mod](https://gamebanana.com/mods/462772)!

  Spin boost is now momentum-based.

  Spin boosting without charging retains Sonic's prior forward momentum, making it act like a normal roll.
  
  Spin charging on the ground determines how fast the spin boost is after releasing the charge. 5 charges = max speed.
  
  In the air, the longer the player holds the spin charge button, the faster the resulting spin boost will be once Sonic hits the ground.
  
  Flinging off a platform while spin boosting is now much smoother (I think).

  Spin charging mid-air doesn't bring Sonic to an awkward halt! This make it the definitive way to perform a drop dash with this mod.
</details>

<details>
  <summary><b>Faster Boost Consumption</b></summary>

  Since Sonic can retain his momentum better in Refined Physics, there's no need for him to boost for a long time.

  Additionally, I wanted to explore how boost and spin dash can co-exist without one losing purpose due the other.
  
  IMO: the boost should be a method to gain speed quickly, with the trade-off that Sonic remains vulnerable and it takes more energy. The spin dash should be a method to both gain speed and attack enemies that takes less energy, with the trade-off that the player must charge it.
  
  Credits to Proto's [Cyberspace Overhaul mod](https://gamebanana.com/mods/430615) for inspiring me to explore this boost-as-dash idea many, many months ago. LexStorm's awesome [High Speed Style mod](https://gamebanana.com/mods/464066) explores the idea as well!
</details>

<details>
  <summary><b>Harder Parry</b></summary>

  Not really physics-related, but I added it anyway lmao

  Makes the parry last 0.3s instead of 15s.
</details>

## WIP 🔴

- Cyberspace 3D physics
- Cyberspace 2D physics
- Water physics
- Momentum-based free falling

## Mandatory Codes/Settings ⚙️

<details>
  <summary><b>HedgeModManager Codes</b></summary>

  ```
  // Animation
  Disable Running Fall

  // Camera
  Disable Spin Charge Camera
  Disable Umbrella Camera Lock-On

  // Cheats
  Always Unlocked Spin Dash

  // Fixes
  Literally everything in this category

  // Gameplay
  Allow Attacking from Stomp Bounce
  Allow Spin Dash on Dash Panels
  Always Trickable Spin Dash Exit
  Disable Drop Dash
  Disable Loop Kick on Slide
  Disable Spin Slash on Drop Dash

  // Physics
  Disable Deceleration Collision
  Reduced Homing Delay
  Retain Ground Velocity for Jump
  Tighter Jump Rotation
  ```
</details>

<details>
  <summary><b>In-Game Settings</b></summary>

  ```
  Starting Speed: 15
  Jump Deceleration: 30
  Set the deceleration rate: 15
  ```
</details>

## Development 💻

Here are the resouces I used to develop this project!

- [HedgeArcPack CLI tool by Radfordhound](https://github.com/HedgeDocs/HedgeDocs.github.io/releases) to unpack/repack the `playercommon.pac` file from the game itself. This file holds a bunch of shit related to the character mechanics.
- [010 Editor](https://www.sweetscape.com/010editor/) to edit the hex code within the `player_common.rfl` file, which you get from unpacking `playercommon.pac`. The RFL file contains hex code for Sonic's movement, combat, etc.
- [`SonicParameters.bt` RFL template by Skyth](https://github.com/blueskythlikesclouds/RflTemplates/blob/master/SonicFrontiers/Uncategorized/SonicParameters.bt) to actually modify `player_common.rfl` without having to figure out which hex codes do what and making me lose my god damn braincells
- [HedgeDocs](https://hedgedocs.com) as have some sort of guide to figure out wtf to do (I still have no idea what I'm doing lmao)
- [Tracker's Physics Tweaks](https://gamebanana.com/mods/415617) to see what parameters are modified to figure out what to actually mess with in `player_common.rfl`. Cool physics mod btw!
- [Legacy Spin Dash by Weezley](https://gamebanana.com/mods/462772) to figure out how creating HMM codes work, and to figure out how to implement charging for the vanilla spin charge. Great spin dash mod!
- [`SonicFrontiers.hmm` from the HedgeModManager repo](https://github.com/thesupersonic16/HedgeModManager/blob/rewrite/HedgeModManager/Resources/Codesv2/SonicFrontiers.hmm) to find the methods and libraries I needed to write my `spinboost.hmm` code
- [Sonic Unleashed Sweepkick by Loco](https://gamebanana.com/mods/461751) to learn how to integrate configuration in a HMM code mod.

***

Literally shout out to [HyperBE32](https://github.com/HyperBE32), Weezley, [Radfordhound](https://github.com/Radfordhound), [Tracker](https://github.com/TrackerTD), and everyone involved with creating the resources I used. Without them, none of this would be possible.
