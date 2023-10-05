## Refined Physics v1.0.1

- Added `MathHelpers` library (I forgot lol)
- Rebalanced spin dash gravity tweak

## Refined Physics v1.0

- First official release!
- Refactored codebase to support the Final Horizon update
- Added tweaks for Cyber Space
- New config categories due to refactor
- Removed differences for air boost and spin dash if power boost is active, might re-introduce later??

## Refined Physics v1.0-beta1.3.3

- NOTICE: Final update until update 3. I've been hearing lots of things about this update; I decided it's best I wait it out. In the mean time, I'll be working on a combat mod! Stay tuned.
- Lowered deceleration force for spin dash
- Lowered deceleration force for rail grinding
- Re-balanced air boost; air boost speed is faster if power boost is on

## Refined Physics v1.0-beta1.3.2

- Reverted spin charge requirement from 8 to 5 for full speed.
- (EXPERIMENTAL) Activating grand slam in combat should be more reliable. Sonic can now perform the action after a single homing attack, melee attack, or quick cyloop. Turn on the code in the experimental section to try it out!

## Refined Physics v1.0-beta1.3.1

- Fixed default configuration; optional tweaks were mistakenly on by default.

## Refined Physics v1.0-beta1.3

- Updates will only come from the server following this update. When updates arrive, please press the refresh icon next to the mod name in HMM to recieve the prompt to download it!
- Completely overhauled the configuration UI. There are now 3 sections: main tweaks, optional tweaks, and experimental tweaks. Main tweaks refer to the most important tweaks that impact gameplay heavily, optional tweaks are more quality-of-life related, but recommended to turn on, and experimental tweaks are things that I DO NOT recommend activating; they're there for me to remember what I wrote lol.
- Cleaned up the code
- Decreased Ground Deceleration, Decreased Falling Deceleration, and Easier Incline Jumps have been merged into one code called Momentum. This was done for the sake of simplifying the UX and making things easier to understand for the player.
- Added the option to have a faster base running speed. If Momentum is enabled, the boost will be faster too!
- (EXPERIMENTAL) Added the option for activating Grand Slam through a combo. Perform a homing attack, 3 combo attacks, and press Y and B at the same time. This is HEAVILY experimental, and my first foray into combat-related tweaking. It also doesn't work sometimes due to how Y and B are mapped to Quick Cyloop and Stomp respectively.
- Replaced Generations-Styled air boost with the Cyber Space air boost. Might be a controversial change, but I really like Cyber Space's air boost; it's the only good thing about Cyber Space.
- Enabling the Momentum code is now required if you want the Chargable Spin Dash to be momentum-based. Otherwise, the Chargable Spin Dash will work just like my other mod of the same name.
- Adjusted ground deceleration value to let Sonic retain running momentum better
- The Momentum code enables smoother running animation transitions

## Refined Physics v1.0-beta1.2

- Removed air deceleration when spin charging in the air, making it the definitive drop dash for this mod
- It now takes 8 spin charges to dash away at full speed instead of 5

## Refined Physics v1.0-beta1.1

- Decreased boost gauge consumption during spin dash from 50 to 25
- Re-balanced initial spin boost speed

## Refined Physics v1.0-beta

- Added configuration UI
- Public beta release 🎉

## Refined Physics v1.0-alpha.4

- General refactoring of codes
- Split ground and air deceleration codes into 2
- Added code for more challenging parry
- Rebalanced air boost
- Increased wall-running brake force
- Spin boost speed increases if power boost is active
- Added code for faster boost consumption

## Refined Physics v1.0-alpha.3

- Reworked mod to use HMM codes rather than modifying playercommon.pac
  - This means it's compatible with other physics/combat mods _in theory_, as my values would overwrite the other mods if placed above them (I think idrk)
- Set up update server properly this time lol

## Refined Physics v1.0-alpha.2

- Made boost gauge recovery rate faster
- Rebalanced air boost
- Decreased light speed dash brake resistance
- Decreased minimum slide speed
- Modified spin boost max speed and deceleration
- Made spin boost flinging better

## Refined Physics v1.0-alpha

- Sonic's deceleration has been drastically reduced; his speed cap is practically gone, allowing the player to gain speed running downhill without boost
- Sonic's falling deceleration has been drastically reduced as well
- Grinding has a lower minimum speed, and deceleration has been reduced to let Sonic gain/lose speed when going upwards/downwards on a rail
- The boost gauge now depletes within 2 seconds or so, making the boost more of a dash ability now
- The air boost now acts similarly to Sonic Generations
- Max parry time has been reduced to 0.3s so it could be more challenging
- The lightspeed dash no longer brings Sonic to a halt after dashing through a trail
- Letting go of the boost while wall running no longer brings Sonic to an instant halt
