// Main purpose of this document is to get C# to show up in the languages section lol

Code "Running Tweaks" by "cali_burrito"
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
  #lib "INI"

  using System.IO;

  static float runningSpeed = 15f;
  static float groundDecele = 4f;
  static float airDecele = 10f;
  static bool isLoaded = false;
  static string directory;
//
{
  if (!isLoaded) {
    directory = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Refined Physics");
    isLoaded = true;
  }

  var config = INI.Read(Path.Combine(directory, "config.ini"));
  bool isFasterRunningEnabled = bool.Parse(config["optionalTweaks"]["fasterRunning"]);
  bool isMomentumEnabled = bool.Parse(config["mainTweaks"]["momentum"]);

  if (isFasterRunningEnabled) {
    runningSpeed = 20f;

    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.normal2.max, runningSpeed);
  }

  if (isMomentumEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.decele.force, groundDecele);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.decele.force2, groundDecele);

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.fall.deceleForce, airDecele);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.fall.overSpeedDeceleForce, airDecele * 2f);

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.boost.initial, runningSpeed * 2f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.boost.min, runningSpeed * 2f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.boost2.initial, runningSpeed * 2f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.speed.boost2.max, runningSpeed * 2f);

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.jumpSpeed.limitUpSpeed, 30f);
  }
}

Code "Chargable Spin Dash" by "cali_burrito"
//
  #include "ReflectionHelpers" noemit

  #lib "Sonic"
  #lib "SonicParameters"
  #lib "BlackboardItem"
  #lib "MathHelpers"
  #lib "INI"

  using System.IO;

  static float chargeCount = 0f;
  static float minDashSpeed = 35f;
  static float maxDashSpeed = 65f;
  static float addedDashSpeed = 30f / 7f;
  static bool isLoaded = false;
  static string directory;
//
{
  if (!isLoaded) {
    directory = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Refined Physics");
    isLoaded = true;
  }

  var config = INI.Read(Path.Combine(directory, "config.ini"));
  bool isChargableSpinDashEnabled = bool.Parse(config["mainTweaks"]["chargableSpinDash"]);
  bool isMomentumEnabled = bool.Parse(config["mainTweaks"]["momentum"]);

  if (isChargableSpinDashEnabled) {
    float dashVelocity = Sonic.Kinematics.GetHorizontalMagnitude();
    float maxSpeed = !isMomentumEnabled ? Sonic.Kinematics.GetHorizontalMagnitude() : 3f;

    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    if (isMomentumEnabled) {
      maxDashSpeed = BlackboardItem.GetRingCount() != SonicParams.pData->forwardView.modePackage.common.capacityRings ? 55f : 75f;
      addedDashSpeed = BlackboardItem.GetRingCount() != SonicParams.pData->forwardView.modePackage.common.capacityRings ? 20f / 7f : 40f / 7f;
    }

    if (Sonic.IsGrounded() && Sonic.State.GetCurrentStateID() == Sonic.StateID.StateSpinBoostCharge) {
      if (Sonic.Input.IsPressed(Sonic.PlayerActionType.PlayerSonicboom))
        chargeCount += 1f;
      
      dashVelocity = MathHelpers.Clamp((minDashSpeed - addedDashSpeed) + (chargeCount * addedDashSpeed), minDashSpeed, maxDashSpeed);
    } else if (!Sonic.IsGrounded()) {
      Sonic.Kinematics kinematics = Sonic.Kinematics.Get();
      if (kinematics == null) return;

      if (Sonic.State.GetCurrentStateID() == Sonic.StateID.StateSpinBoostCharge) {
        *kinematics.Velocity += Sonic.Kinematics.GetForward();
        chargeCount += 1f;
        dashVelocity = MathHelpers.Clamp(chargeCount / 1.15f, minDashSpeed, maxDashSpeed);
      }
    } else if (Sonic.State.GetPreviousStateID() != Sonic.StateID.StateSpinBoostCharge && Sonic.State.GetCurrentStateID() != Sonic.StateID.StateSpinBoost) {
      chargeCount = 0f;
      dashVelocity = Sonic.Kinematics.GetHorizontalMagnitude();
      if (!isMomentumEnabled) maxSpeed = Sonic.Kinematics.GetHorizontalMagnitude();
    }

    RFL_SET_PARAM(SonicParams, forwardView.spinBoost.initialRunTime, 0.2f);
    RFL_SET_PARAM(SonicParams, forwardView.spinBoost.speedBoost.initialSpeed, dashVelocity);
    RFL_SET_PARAM(SonicParams, forwardView.spinBoost.speedBoost.maxSpeed, maxSpeed);
    RFL_SET_PARAM(SonicParams, forwardView.spinBoost.speedBoost.minTurnSpeed, 30f);
    RFL_SET_PARAM(SonicParams, forwardView.spinBoost.speedBoost.decele.force, 5f);
    RFL_SET_PARAM(SonicParams, forwardView.spinBoost.speedBoost.decele.force2, 5f);
    RFL_SET_PARAM(SonicParams, forwardView.spinBoost.deceleNeutralMin.force, 750f);
    RFL_SET_PARAM(SonicParams, forwardView.spinBoost.deceleNeutralMax.force, 1000f);
    RFL_SET_PARAM(SonicParams, forwardView.spinBoost.gravitySize, 70f);
  }
}

Code "Balanced Boost Gauge Drain" by "cali_burrito"
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
  #lib "INI"

  using System.IO;

  static bool isLoaded = false;
  static string directory;
//
{
  if (!isLoaded) {
    directory = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Refined Physics");
    isLoaded = true;
  }

  var config = INI.Read(Path.Combine(directory, "config.ini"));
  bool isBalancedDrainEnabled = bool.Parse(config["mainTweaks"]["balancedDrain"]);

  if (isBalancedDrainEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.boost.consumptionRate, Sonic.State.GetCurrentStateID() != Sonic.StateID.StateSpinBoost ? 100f : 20f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.boost.recoveryRate, 20f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.boost.infinityBoostTime, 120f);
  }
}

Code "Cyber Space Air Boost" by "cali_burrito"
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
  #lib "INI"

  using System.IO;

  static bool isLoaded = false;
  static string directory;
//
{
  if (!isLoaded) {
    directory = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Refined Physics");
    isLoaded = true;
  }

  var config = INI.Read(Path.Combine(directory, "config.ini"));
  bool isCSAirBoostEnabled = bool.Parse(config["mainTweaks"]["csAirBoost"]);

  if (isCSAirBoostEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.startHSpeed, 40f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.startHSpeedMax, 80f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.minHSpeed, 30f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.minHSpeedMax, 60f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.minKeepTime, 0.05f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.maxKeepTime, 0.1f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.maxTime, 0f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.gravityRate, 0.5f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.airboost.additionalTransitTime, 0.3f);
  }
}

Code "Rail Momentum" by "cali_burrito"
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
  #lib "INI"

  using System.IO;

  static bool isLoaded = false;
  static string directory;
//
{
  if (!isLoaded) {
    directory = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Refined Physics");
    isLoaded = true;
  }

  var config = INI.Read(Path.Combine(directory, "config.ini"));
  var isRailMomentumEnabled = bool.Parse(config["mainTweaks"]["railMomentum"]);

  if (isRailMomentumEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.grind.deceleForce, 5f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.grind.limitSpeedMin, 10f);
  }
}

Code "Soft Ring Dash Brake" by "cali_burrito"
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
  #lib "INI"

  using System.IO;

  static bool isLoaded = false;
  static string directory;
//
{
  if (!isLoaded) {
    directory = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Refined Physics");
    isLoaded = true;
  }

  var config = INI.Read(Path.Combine(directory, "config.ini"));
  var isSoftRingDashEnabled = bool.Parse(config["mainTweaks"]["softRingDash"]);

  if (isSoftRingDashEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.lightDash.brake, 75f);
  }
}

Code "Harder Parry" by "cali_burrito"
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
  #lib "INI"

  using System.IO;

  static bool isLoaded = false;
  static string directory;
//
{
  if (!isLoaded) {
    directory = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Refined Physics");
    isLoaded = true;
  }

  var config = INI.Read(Path.Combine(directory, "config.ini"));
  var isHarderParryEnabled = bool.Parse(config["optionalTweaks"]["harderParry"]);

  if (isHarderParryEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.parry.minRecieveTime, 0.15f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.parry.maxRecieveTime, 0.3f);
    RFL_SET_PARAM(SonicParams, forwardView.modePackage.parry.justEffectTime2, 1f);
  }
}

Code "Decreased Minimum Slide Speed" by "cali_burrito"
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
  #lib "INI"

  using System.IO;

  static bool isLoaded = false;
  static string directory;
//
{
  if (!isLoaded) {
    directory = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Refined Physics");
    isLoaded = true;
  }

  var config = INI.Read(Path.Combine(directory, "config.ini"));
  var isMinSlideEnabled = bool.Parse(config["optionalTweaks"]["minSlide"]);

  if (isMinSlideEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.sliding.minSpeed, 5f);
  }
}

Code "Soft Wall Running Brake" by "cali_burrito"
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
  #lib "INI"

  using System.IO;

  static bool isLoaded = false;
  static string directory;
//
{
  if (!isLoaded) {
    directory = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Refined Physics");
    isLoaded = true;
  }

  var config = INI.Read(Path.Combine(directory, "config.ini"));
  var isSoftWallBrakeEnabled = bool.Parse(config["optionalTweaks"]["softWallBrake"]);

  if (isSoftWallBrakeEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    RFL_SET_PARAM(SonicParams, forwardView.modePackage.wallmove.brake, 25f);
  }
}

Code "Grand Slam On Demand" by "cali_burrito"
//
  #include "ReflectionHelpers" noemit

  #lib "SonicParameters"
  #lib "INI"

  using System.IO;

  static int comboCounter = 0;
  static bool isLoaded = false;
  static string directory;
//
{
  if (!isLoaded) {
    directory = Path.Combine(Directory.GetCurrentDirectory(), "Mods", "Refined Physics");
    isLoaded = true;
  }

  var config = INI.Read(Path.Combine(directory, "config.ini"));
  var isSlamDemandEnabled = bool.Parse(config["experimentalTweaks"]["slamDemand"]);

  if (isSlamDemandEnabled) {
    var SonicParams = Reflection.GetDataInfo<SonicParameters.Root>("player_common");
    if (SonicParams.pData == null) return;

    if (Sonic.State.GetCurrentStateID() == Sonic.StateID.StateComboStep)
      comboCounter += 1;

    if (comboCounter == 4 && Sonic.Input.IsDown(Sonic.PlayerActionType.PlayerCyloop) && Sonic.Input.IsDown(Sonic.PlayerActionType.PlayerStomping))
      Sonic.State.SetState(Sonic.StateID.StateSmash);

    if (comboCounter > 4 || Sonic.State.GetPreviousStateID() == Sonic.StateID.StateSmash)
      comboCounter = 0;
  }
}