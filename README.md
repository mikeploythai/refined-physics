# Refined Physics for Sonic Frontiers

I like the vanilla physics of Sonic Frontiers, but the most annoying thing about it is when I let go of RT when power boosting, and Sonic practically loses all his prior speed. The inital goal was to create a mod that addresses that singular issue, but here I am.. with yet another physics mod.

I know, I know. It's an oversaturated mod niche for this game, but this mod is different in the way that I try my best to give you full control over what to change about the physics while keeping it simple and easy to understand.

Every tweak in this mod can be enabled or disabled in the configuration menu, allowing you to truly refine your Frontiers experience.

## Main Tweaks

> These are the most important tweaks, and I recommend you keep all of these on for a smoother experience in the open zone. Cyber Space support is coming soon!

<details>
  <summary><b>Momentum</b></summary>

  Allows Sonic to gain, lose, and retain speed naturally.

  Sonic's air drag has been reduced when he's falling uncurled, allowing him to keep his forward momentum in the air better.
  
  Sonic's jump speed limit has been increased, allowing incline jumps to be easier to perform.
</details>

<details>
  <summary><b>Chargable Spin Dash & Rolling</b></summary>

  Enables spin charging to determine the spin dash speed.

  Due to the charge requirement to dash, holding the spin dash button without charging makes the ability act like a roll in the classic games.
  
  Charging in the air no longer kills Sonic's prior momentum. This makes it act like a normal drop dash, and makes it the recommended way to perform it.
  
  If momentum is enabled, the spin dash will be momentum-based and the max speed will depend on if power boost is active. Else, it'll act like the vanilla spin dash, but with charging taken into account.
</details>

<details>
  <summary><b>Balanced Boost Gauge Drain</b></summary>

 Re-balances the boost to account for Sonic's ability to retain speed very well.
  
  The boost lasts much shorter, making it act more like a dash ability. The spin dash, however, lasts much longer.
  
  Infinite boost now lasts for 2 minutes instead of 3. It's recommended to activate infinite boost when facing SQUID.
</details>

<details>
  <summary><b>Soft Ring Dash Brake</b></summary>

  Prevents Sonic from coming to a halt after ring dashing.
</details>

<details>
  <summary><b>Rail Momentum</b></summary>

  Decreases rail deceleration, allowing Sonic to gain, lose, and retain speed naturally while grinding.

  Sonic still has a minimum grinding speed, but it has been reduced.
</details>

## Optional Tweaks

> These are more quality-of-life oriented, and are off by default. I recommend enabling all of them, but they don't take too much away from the experience if not.

<details>
  <summary><b>Faster Base Running Speed</b></summary>

  Makes running without boost less painful.
</details>

<details>
  <summary><b>Harder Parry</b></summary>

  The parry now lasts 0.3s instead of 15s, incentivizing players to time their parries well.

  The length is subject to change; I haven't tested this in boss fights yet.
</details>

<details>
  <summary><b>Decreased Minimum Sliding Speed</b></summary>

  Decreases the minimum speed for sliding.
  
  I don't like the random speed-up from sliding, so this fixes it.
</details>

<details>
  <summary><b>Soft Wall Running Brake</b></summary>

  Prevents Sonic from coming to an instant stop when letting go of the boost button while wall running.
</details>

## WIP

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
