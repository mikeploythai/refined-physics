# Refined Physics for Sonic Frontiers 🏃‍♂️

I like the vanilla physics of Sonic Frontiers, but holy shit the most annoying thing about it is when I let go of RT when power boosting, and Sonic practically loses all his prior speed. The inital goal was to create a mod that addresses that singular issue, but now I aim to create yet another physics mod that pivots the game towards momentum-based movement 🙄

I know, I know. It's an oversaturated mod niche for this game, but the thing that's bothered me about all the other physics mods is that it deviates a bit too much from the vanilla feel. Some have Sonic's base running speed at a crazy high amount, some make it a bit way too difficult for Sonic to simply walk up a hill, and some just simply don't feel 100% to my liking. I want this mod to feel a good amount like vanilla movement, but with refinement and quality of life changes.

At this _extremely_ early stage, I just wanna mess with the physics. However, I would like to mess with Sonic's combat and the control layout in the future.

## Development ⚙️

Here are the resouces I used to develop this project!

- [HedgeArcPack CLI tool by Radfordhound](https://github.com/HedgeDocs/HedgeDocs.github.io/releases) to unpack/repack the `playercommon.pac` file from the game itself. This file holds a bunch of shit related to the character mechanics.
- [010 Editor](https://www.sweetscape.com/010editor/) to edit the hex code within the `player_common.rfl` file, which you get from unpacking `playercommon.pac`. The RFL file contains hex code for Sonic's movement, combat, etc.
- [`SonicParameters.bt` RFL template by Skyth](https://github.com/blueskythlikesclouds/RflTemplates/blob/master/SonicFrontiers/Uncategorized/SonicParameters.bt) to actually modify `player_common.rfl` without having to figure out which hex codes do what and making me lose my god damn braincells
- [HedgeDocs](https://hedgedocs.com) as have some sort of guide to figure out wtf to do (I still have no idea what I'm doing lmao)
- [Tracker's Physics Tweaks](https://gamebanana.com/mods/415617) to see what parameters are modified to figure out what to actually mess with in `player_common.rfl`. Cool physics mod btw!
- [Legacy Spin Dash by Weezley](https://gamebanana.com/mods/462772) to figure out how creating HMM codes work, and to figure out how to implement charging for the vanilla spin charge. Great spin dash mod!
- [`SonicFrontiers.hmm` from the HedgeModManager repo](https://github.com/thesupersonic16/HedgeModManager/blob/rewrite/HedgeModManager/Resources/Codesv2/SonicFrontiers.hmm) to find the methods and libraries I needed to write my `spinboost.hmm` code

***

Literally shout out to [HyperBE32](https://github.com/HyperBE32), Weezley, [Radfordhound](https://github.com/Radfordhound), [Tracker](https://github.com/TrackerTD), and everyone involved with creating the resources I used. Without them, none of this would be possible.
